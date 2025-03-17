using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraTracker : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public float rotationSpeed = 5f; 
    private float height;
    private float radius;
    private float currentAngle;
    private Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        if (target == null) return;

        height = transform.position.y - target.position.y;

        radius = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(target.position.x, 0, target.position.z));

        currentAngle = Mathf.Atan2(transform.position.x - target.position.x, transform.position.z - target.position.z) * Mathf.Rad2Deg;

        Vector3 initialPosition = target.position - new Vector3(0, -height, radius);
        transform.position = initialPosition;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float targetAngle = target.eulerAngles.y;
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        Vector3 offset = new Vector3(Mathf.Sin(currentAngle * Mathf.Deg2Rad) * radius, height, Mathf.Cos(currentAngle * Mathf.Deg2Rad) * radius);

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed * Time.deltaTime);

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, 0f);
    }
}