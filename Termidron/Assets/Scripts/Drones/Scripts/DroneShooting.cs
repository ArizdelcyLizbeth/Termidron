using UnityEngine;

public class DroneShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootInterval = 0.5f;
    public float bulletSpeed = 10f;

    private float lastShootTime = 0f;

    public void Shoot()
    {
        if (Time.time >= lastShootTime + shootInterval)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = shootPoint.forward * bulletSpeed;
            lastShootTime = Time.time;
        }
    }
}
