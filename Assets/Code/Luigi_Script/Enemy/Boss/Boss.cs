using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    [Header("Movement Options")]
    [SerializeField] private Transform[] pathpoints;
    [SerializeField] private float waitTimeAtPoint = 2f;
    [SerializeField] private float movespeed = 3f;

    [Header("Attack Options")]
    [SerializeField] private GameObject areaAttackPrefab;
    [SerializeField] private GameObject projectilePreFab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform target;
    [SerializeField] private float projectileSpeed = 10f;

    [Header("Health System")]
    [SerializeField] private HealthSystem healthSystem;

    [Header("Door To Open")]
    [SerializeField] private Boss_Door doorToOpen;

    private int currentPointIndex = 0;
    private bool isAttacking = false;

    private Queue<string> attackQueue = new Queue<string>();

    private void Start()
    {
        if (healthSystem == null)
            healthSystem = GetComponent<HealthSystem>();

        RebuildAttackQueue();
        StartCoroutine(MoveToNextPoint());
    }

    IEnumerator MoveToNextPoint()
    {
        while (true)
        {
            if (!isAttacking)
            {
                Vector3 targetPos = pathpoints[currentPointIndex].position;

                while (Vector3.Distance(transform.position, targetPos) > 0.2f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, movespeed * Time.deltaTime);
                    transform.LookAt(targetPos);
                    yield return null;
                }

                yield return new WaitForSeconds(0.5f);
                StartCoroutine(AttackRoutine());

                yield return new WaitForSecondsRealtime(waitTimeAtPoint);
                currentPointIndex = (currentPointIndex + 1) % pathpoints.Length;
            }
            yield return null;
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        if (attackQueue.Count == 0)
            RebuildAttackQueue();

        string attack = attackQueue.Dequeue();

        if (attack == "Projectile" && target != null)
        {
            ShootProjectile();
        }
        else if (attack == "Area" && target != null)
        {
            Vector3 spawnPosition = target.position;
            GameObject area = Instantiate(areaAttackPrefab, spawnPosition, Quaternion.identity);

            Area_Attack areaScript = area.GetComponent<Area_Attack>();
            if (areaScript != null)
            {
                areaScript.Initialize(target);
            }
        }

        yield return new WaitForSecondsRealtime(1.5f);
        isAttacking = false;
    }

    void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePreFab, firePoint.position, firePoint.rotation);

        Vector3 direction = (target.position - firePoint.position).normalized;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }

        Destroy(projectile, 5f);
    }

    private bool isDead = false;

    private void Update()
    {
        if (!isDead && healthSystem.getLife() <= 0f)
        {
            isDead = true;

            StopAllCoroutines();

            // Apri la porta
            if (doorToOpen != null)
                doorToOpen.OpenDoor();

        }
    }

    void RebuildAttackQueue()
    {
        attackQueue.Clear();

        float maxHealth = healthSystem != null ? healthSystem.GetType().GetField("MaxHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(healthSystem) is float val ? val : 100f : 100f;
        float currentHealth = healthSystem != null ? healthSystem.getLife() : maxHealth;

        float healthPercent = currentHealth / maxHealth;

        if (healthPercent > 0.7f)
        {
            attackQueue.Enqueue("Projectile");
            attackQueue.Enqueue("Area");
        }
        else if (healthPercent < 0.4f)
        {
            attackQueue.Enqueue("Projectile");
            attackQueue.Enqueue("Area");
            attackQueue.Enqueue("Area");
        }
        else
        {
            attackQueue.Enqueue("Projectile");
            attackQueue.Enqueue("Area");
            attackQueue.Enqueue("Area");
            attackQueue.Enqueue("Area");
        }
    }
}
