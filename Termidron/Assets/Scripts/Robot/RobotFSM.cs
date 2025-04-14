using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controlador IA del robot utilizando una máquina de estados finitos (FSM)
/// </summary>
public class RobotFSM : MonoBehaviour
{
    /// <summary>
    /// Estados del robot.
    /// </summary>
    public enum RobotState { Patrol, Alert, Attack, Hit }
    public RobotState currentState;

    [Header("References")]
    /// <summary>
    /// Referencia al transform del jugador
    /// </summary>
    public Transform player;
    /// <summary>
    /// Waypoints que el robot sigue en modo patrulla
    /// </summary>
    public List<Transform> waypoints;
    public GameManager gameManager;

    [Header("Speeds & Ranges")]
    public float patrolSpeed = 3f;
     /// <summary>
    /// Velocidad de movimiento en estado de alerta
    /// </summary>
    public float alertSpeed = 4.5f;
    /// <summary>
    /// Distancia máxima a la que el robot puede atacar al jugador
    /// </summary>
    public float attackRange = 10f;
    /// <summary>
    /// Rango en el que el robot puede detectar al jugador
    /// </summary>
    public float alertRange = 15f;
     /// <summary>
    /// Rango en el que el robot puede oir ruidos
    /// </summary>
    public float hearingRange = 20f;
    public float waypointThreshold = 1f;

    [Header("Hit Reaction")]
    /// <summary>
    /// Duracion en segundos del estado de golpe
    /// </summary>
    public float hitDuration = 6f;   
    /// <summary>
    /// Si el robot esta actualmente en estado de golpeado
    /// </summary>    
    private bool isHit = false;          

    private int currentWaypointIndex = 0;
    private bool isReturning = false;
    private DroneShooting shootingScript;
    /// <summary>
    /// Inicializa la FSM y obtiene el componente de disparo
    /// </summary>
    private void Start()
    {
        currentState = RobotState.Patrol;
        shootingScript = GetComponent<DroneShooting>();
    }
    /// <summary>
    /// Lógica de la FSM del robot. Se ejecuta cada frame.
    /// </summary>
    private void Update()
    {
        if (!gameManager.IsGameInProgress()) return;

        if (isHit)
        {
            return;
        }

        switch (currentState)
        {
            case RobotState.Patrol:
                Patrol();
                break;
            case RobotState.Alert:
                Alert();
                break;
            case RobotState.Attack:
                Attack();
                break;
            case RobotState.Hit:
                break;
        }
    }
    /// <summary>
    /// Logica del estado de patrullaje. El robot se mueve entre waypoints y verifica si el jugador esta cerca
    /// </summary>
    private void Patrol()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        MoveTowards(targetWaypoint.position, patrolSpeed);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < waypointThreshold)
        {
            if (!isReturning)
            {
                if (currentWaypointIndex < waypoints.Count - 1) currentWaypointIndex++;
                else isReturning = true;
            }
            else
            {
                if (currentWaypointIndex > 0) currentWaypointIndex--;
                else isReturning = false;
            }
        }

        CheckForPlayer();
    }
    /// <summary>
    /// Logica del estado de alerta. El robot se mueve hacia el jugador y cambia de estado si se acerca lo suficiente o si lo pierde de vista
    /// </summary>
    private void Alert()
    {
        MoveTowards(player.position, alertSpeed);

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange)
            currentState = RobotState.Attack;
        else if (dist > alertRange)
            currentState = RobotState.Patrol;
    }
    /// <summary>
    /// Logica del estado de ataque. El robot mira al jugador y dispara. Si el jugador se aleja, regresa a alerta
    /// </summary>
    private void Attack()
    {
        transform.LookAt(player);
        shootingScript?.Shoot();

        if (Vector3.Distance(transform.position, player.position) > attackRange)
            currentState = RobotState.Alert;
    }
    /// <summary>
    /// Verifica si el jugador esta dentro del rango de alerta para cambiar de estado
    /// </summary>
    private void CheckForPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= alertRange)
            currentState = RobotState.Alert;
    }
    /// <summary>
    /// Mueve al robot hacia un objetivo y rota para mirarlo
    /// </summary>
    /// <param name="target">Posicion de destino</param>
    /// <param name="speed">Velocidad de movimiento</param>
    private void MoveTowards(Vector3 target, float speed)
    {
        Vector3 dir = (target - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        transform.LookAt(target);
    }

    /// <summary>
    /// Se llama cuando el jugador genera ruido. Cambia al estado de alerta si el robot lo escucha
    /// </summary>
    /// <param name="noisePosition">Posicion donde se genero el ruido</param>
    public void OnPlayerMakesNoise(Vector3 noisePosition)
    {
        if (Vector3.Distance(transform.position, noisePosition) <= hearingRange)
            currentState = RobotState.Alert;
    }
    /// <summary>
    /// Detecta colisiones con la espada. Si es golpeado, entra en estado Hit
    /// </summary>
    /// <param name="other">El collider que entro en contacto con el robot</param>
    private void OnTriggerEnter(Collider other)
    {
        if (!isHit && other.CompareTag("Sword"))
        {
            StartCoroutine(HandleHit());
            Destroy(other.gameObject);  
        }
    }
    /// <summary>
    /// Maneja el estado de golpe recibido durante cierto tiempo, luego vuelve a patrullar
    /// </summary>
    /// <returns>Corrutina que espera un tiempo antes de cambiar de estado</returns>
    private IEnumerator HandleHit()
    {
        isHit = true;
        currentState = RobotState.Hit;
        yield return new WaitForSeconds(hitDuration);
        isHit = false;
        currentState = RobotState.Patrol;
    }
}
