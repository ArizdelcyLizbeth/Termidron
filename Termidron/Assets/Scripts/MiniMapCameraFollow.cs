using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    public float smoothFactor = 0.1f;

    private Camera miniMapCamera;
    private Vector3 lastTargetPosition;

    void Start()
    {
        miniMapCamera = GetComponent<Camera>();
        lastTargetPosition = objectToFollow.transform.position;
    }

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