using UnityEngine;
using System.Collections.Generic;

public class RobotFSM : MonoBehaviour
{
    public enum RobotState { Patrol, Alert, Attack }
    public RobotState currentState;

    public Transform player;
    public List<Transform> waypoints;
    public float patrolSpeed = 3f;
    public float alertSpeed = 4.5f;
    public float attackRange = 10f;
    public float alertRange = 15f;
    public float hearingRange = 20f;
    public float waypointThreshold = 1f;
    public GameManager gameManager;
    
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
        }
    }

    void Patrol()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        MoveTowards(targetWaypoint.position, patrolSpeed);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < waypointThreshold)
        {
            if (!isReturning)
            {
                if (currentWaypointIndex < waypoints.Count - 1)
                    currentWaypointIndex++;
                else
                    isReturning = true;
            }
            else
            {
                if (currentWaypointIndex > 0)
                    currentWaypointIndex--;
                else
                    isReturning = false;
            }
        }

        CheckForPlayer();
    }

    void Alert()
    {
        MoveTowards(player.position, alertSpeed);

        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            currentState = RobotState.Attack;
        }
        else if (Vector3.Distance(transform.position, player.position) > alertRange)
        {
            currentState = RobotState.Patrol;
        }
    }

    void Attack()
    {
        transform.LookAt(player);
        
        if (shootingScript != null)
        {
            shootingScript.Shoot();
        }

        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            currentState = RobotState.Alert;
        }
    }

    void CheckForPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= alertRange)
        {
            currentState = RobotState.Alert;
        }
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(target);
    }

    public void OnPlayerMakesNoise(Vector3 noisePosition)
    {
        if (Vector3.Distance(transform.position, noisePosition) <= hearingRange)
        {
            currentState = RobotState.Alert;
        }
    }
}
