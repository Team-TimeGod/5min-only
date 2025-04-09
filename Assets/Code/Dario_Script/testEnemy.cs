using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public float fireRate = 2f;
    public float detectionRange = 10f;
    public float health = 50f;
    public Transform firePoint;

    private Transform player;
    private float nextFireTime = 0f;

    void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform; 
        }

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Ruota verso il giocatore
                Vector3 directionToPlayer = (player.position - firePoint.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                if (Time.time > nextFireTime)
                {
                    FireProjectile(directionToPlayer);
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    private void FireProjectile(Vector3 direction)
    {
        GameObject Projectile = Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = Projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * 10f; 
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy destroyed.");
        Destroy(gameObject);
    }
}
