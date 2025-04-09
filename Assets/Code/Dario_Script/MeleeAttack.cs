using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 0.5f;
    public int attackDamage = 10; // Danno inflitto al nemico
    public LayerMask enemyLayer;
    public LayerMask projectileLayer;
    public Transform attackPoint;

    private float lastAttackTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }

    void PerformAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("Nemico colpito: " + enemy.name);
            Enemy enemyComponent = enemy.GetComponent<Enemy>();

            if (enemyComponent != null)

            {

                enemyComponent.TakeDamage(attackDamage);

            }
        }

        Collider[] hitProjectiles = Physics.OverlapSphere(attackPoint.position, attackRange, projectileLayer);
        foreach (Collider projectile in hitProjectiles)
        {
            Debug.Log("Proiettile respinto: " + projectile.name);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = -rb.linearVelocity;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
