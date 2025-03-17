using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float bulletSpeed = 20f;  

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  

        if (rb != null)
        {
            rb.velocity = transform.forward * bulletSpeed;  
        }
    }

    void Update()
    {
    
        // despues vemos aqui que show para destruirla después de cierto tiempo
    }
}
