using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    public int bulletDamage = 10;
    public float bulletSpeed = 40f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        // Fire rate limit
        if (Time.time < nextFireTime) return;

        Shoot();

        nextFireTime = Time.time + fireRate;
    }

    void Shoot()
    {
        // Spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set bullet stats
        Bullet b = bullet.GetComponent<Bullet>();
        b.damage = bulletDamage;
        b.speed = bulletSpeed;

    }
}
