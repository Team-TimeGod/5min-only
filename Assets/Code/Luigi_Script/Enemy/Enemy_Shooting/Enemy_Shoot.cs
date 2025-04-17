using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Transform target; // Il giocatore o altro bersaglio
    public float shootInterval = 2f;
    public float projectileSpeed = 5f;

    private float shootTimer;

    void Update()
    {
        if (target != null)
        {
            // Ruota l'enemy per guardare il target
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval && target != null)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null && target != null)
        {
            Vector3 direction = (target.position - firePoint.position).normalized;
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}
