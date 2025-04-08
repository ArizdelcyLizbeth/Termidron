using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotFSM : MonoBehaviour
{
    public enum RobotState { Patrol, Alert, Attack, Hit }
    public RobotState currentState;

    [Header("References")]
    public Transform player;
    public List<Transform> waypoints;
    public GameManager gameManager;

    [Header("Speeds & Ranges")]
    public float patrolSpeed = 3f;
    public float alertSpeed = 4.5f;
    public float attackRange = 10f;
    public float alertRange = 15f;
    public float hearingRange = 20f;
    public float waypointThreshold = 1f;

    [Header("Hit Reaction")]
    public float hitDuration = 6f;       
    private bool isHit = false;          

    private int currentWaypointIndex = 0;
    private bool isReturning = false;
    private DroneShooting shootingScript;

    private void Start()
    {
        currentState = RobotState.Patrol;
        shootingScript = GetComponent<DroneShooting>();
    }

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

    private void Alert()
    {
        MoveTowards(player.position, alertSpeed);

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange)
            currentState = RobotState.Attack;
        else if (dist > alertRange)
            currentState = RobotState.Patrol;
    }

    private void Attack()
    {
        transform.LookAt(player);
        shootingScript?.Shoot();

        if (Vector3.Distance(transform.position, player.position) > attackRange)
            currentState = RobotState.Alert;
    }

    private void CheckForPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= alertRange)
            currentState = RobotState.Alert;
    }

    private void MoveTowards(Vector3 target, float speed)
    {
        Vector3 dir = (target - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        transform.LookAt(target);
    }

    public void OnPlayerMakesNoise(Vector3 noisePosition)
    {
        if (Vector3.Distance(transform.position, noisePosition) <= hearingRange)
            currentState = RobotState.Alert;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isHit && other.CompareTag("Sword"))
        {
            StartCoroutine(HandleHit());
            Destroy(other.gameObject);  
        }
    }

    private IEnumerator HandleHit()
    {
        isHit = true;
        currentState = RobotState.Hit;
        yield return new WaitForSeconds(hitDuration);
        isHit = false;
        currentState = RobotState.Patrol;
    }
}
