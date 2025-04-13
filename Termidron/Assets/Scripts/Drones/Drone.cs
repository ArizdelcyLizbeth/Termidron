using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Este script controla el comportamiento de un drone dentro de un área de navegación definida.
/// El drone persigue al personaje principal dentro de un área específica y se mueve aleatoriamente dentro de su zona asignada cuando no está siguiendo al personaje.
/// </summary>
public class Drone : MonoBehaviour
{
    public string area;
    public GameObject mainCharacter;
    private Vector3 initialPosition;
    private NavMeshGraph graph;
    private List<Vector3> vertices;
    private bool isGaming;
    private bool hasPath;
    private Vector3 randomVertex;
    private NavMeshAgent agent;
    private float timeAtLastDestination = 0f;
    private const float proximityRadius = 5f;
    private Vector3 lastPosition;
    private float timeSinceLastPositionUpdate = 0f;
    private const float positionUpdateInterval = 1.5f;
    private const float inactivityThreshold = 3f;
    private float timeAtLastInactivityCheck = 0f;

    /// <summary>
    /// Método de inicio. Se ejecuta cuando el drone es instanciado o al iniciar el juego.
    /// Inicializa las variables, el agente de navegación y configura los vértices en función del área asignada al drone.
    /// </summary>
    void Start()
    {
        initialPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        graph = new NavMeshGraph();
        if (area == "A")
        {
            vertices = graph.areaAVertices;
        }
        else
        {
            vertices = graph.areaBVertices;
        }
        hasPath = false;
        isGaming = false;
        lastPosition = transform.position;
    }

    /// <summary>
    /// Método de actualización que se ejecuta cada fotograma.
    /// Controla el movimiento del drone en función de la actividad del juego y la proximidad al objetivo.
    /// </summary>
    void Update()
    {
        if (isGaming)
        {
            Vector3 objective = mainCharacter.transform.position;
            if (graph.IsPointInArea(objective, area))
            {
                Vector3 directionToObjective = (objective - transform.position).normalized;
                float minDistance = 2f;
                Vector3 adjustedTarget = objective - directionToObjective * minDistance;
                agent.SetDestination(adjustedTarget);
                timeAtLastDestination = Time.time;
                lastPosition = transform.position;
            }
            else
            {
                if (!hasPath)
                {
                    randomVertex = GetRandomVertex();
                    agent.SetDestination(randomVertex);
                    hasPath = true;
                    timeAtLastDestination = Time.time;
                    lastPosition = transform.position;
                }
                else
                {
                    if (Vector3.Distance(transform.position, randomVertex) < proximityRadius)
                    {
                        randomVertex = GetRandomVertex();
                        agent.SetDestination(randomVertex);
                        timeAtLastDestination = Time.time;
                        lastPosition = transform.position;
                    }
                    else
                    {
                        // Verificamos si el robot está quieto
                        if (Vector3.Distance(transform.position, lastPosition) < 0.5f)
                        {
                            // Acumulamos el tiempo de inactividad
                            timeAtLastInactivityCheck += Time.deltaTime;

                            // Si ha pasado 7 segundos sin movimiento
                            if (timeAtLastInactivityCheck >= inactivityThreshold)
                            {
                                // Generamos un nuevo punto aleatorio
                                randomVertex = GetRandomVertex();
                                agent.SetDestination(randomVertex);
                                timeAtLastDestination = Time.time;
                                timeAtLastInactivityCheck = 0f;
                                lastPosition = transform.position;
                            }
                        }
                        else
                        {
                            // Si el robot se mueve, reiniciamos el contador de inactividad
                            timeAtLastInactivityCheck = 0f;
                        }
                    }
                }
            }
            // Actualizamos la última posición del robot cada 2 segundos
            timeSinceLastPositionUpdate += Time.deltaTime;
            if (timeSinceLastPositionUpdate >= positionUpdateInterval)
            {
                lastPosition = transform.position;
                timeSinceLastPositionUpdate = 0f;
            }
        }
        else
        {
            agent.ResetPath();
        }
    }

    /// <summary>
    /// Obtiene un vértice aleatorio dentro de los vértices del área asignada al drone.
    /// </summary>
    /// <returns>Un vértice aleatorio dentro del área.</returns>
    private Vector3 GetRandomVertex()
    {
        if (vertices == null || vertices.Count == 0)
            return transform.position;

        int index = Random.Range(0, vertices.Count);
        return vertices[index];
    }

    /// <summary>
    /// Activa el drone para que comience a moverse y seguir al personaje principal.
    /// </summary>
    public void EnableGaming()
    {
        isGaming = true;
    }

    /// <summary>
    /// Restaura al drone a su posición inicial y detiene su movimiento.
    /// </summary>
    public void ResetToInitialPosition()
    {
        isGaming = false;
        if (agent != null)
        {
            agent.ResetPath();
        }
        transform.position = initialPosition;
    }
}