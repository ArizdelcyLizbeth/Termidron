using UnityEngine;

public class DroneShooting : MonoBehaviour
{
    public GameObject bulletPrefab;      
    public Transform shootPoint;         
    public float shootInterval = 0.5f;   
    public float bulletSpeed = 10f;     
    public AudioClip shootSound;        

    private float nextShootTime = 0f;    

    void Update()
    {
       
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootInterval;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;  
            rb.velocity = shootPoint.forward * bulletSpeed;  
        }

        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, shootPoint.position);
        }
    }
}
