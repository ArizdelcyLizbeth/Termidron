using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento de la cámara del minimapa para que siga suavemente a un objeto en la escena,
/// manteniendo una altura fija y posicionándose en la vista superior.
/// </summary>
public class MiniMapCameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    public float smoothFactor = 0.1f;

    private Camera miniMapCamera;
    private Vector3 lastTargetPosition;

    /// <summary>
    /// Inicializa la cámara del minimapa y guarda la posición inicial del objetivo.
    /// </summary>
    void Start()
    {
        miniMapCamera = GetComponent<Camera>();
        lastTargetPosition = objectToFollow.transform.position;
    }

    /// <summary>
    /// Actualiza la posición de la cámara en cada frame si el objetivo se ha movido.
    /// La cámara se desplaza suavemente manteniendo la altura original.
    /// </summary>
    void Update()
    {
        Vector3 targetPosition = objectToFollow.transform.position;
        if (Vector3.Distance(targetPosition, lastTargetPosition) > 0.01f)
        {
            Vector3 newPosition = new Vector3(targetPosition.x, miniMapCamera.transform.position.y, targetPosition.z);
            miniMapCamera.transform.position = Vector3.Lerp(miniMapCamera.transform.position, newPosition, smoothFactor);
            lastTargetPosition = targetPosition;
        }
    }
}