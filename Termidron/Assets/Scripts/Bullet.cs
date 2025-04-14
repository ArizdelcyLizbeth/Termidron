using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Método que se llama cuando la bala entra en colisión con otro objeto
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("CRGDRobot"))
        {
            Destroy(this.gameObject);
        }
    }
}