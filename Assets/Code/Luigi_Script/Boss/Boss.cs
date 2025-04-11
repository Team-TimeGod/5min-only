using UnityEngine;
using System.Collections;
using UnityEngine.AI;
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

    [Header("Health System")]
    [SerializeField] private HealthSystem healthSystem;

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
                Vector3 target = pathpoints[currentPointIndex].position;

                while (Vector3.Distance(transform.position, target) > 0.2f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, movespeed * Time.deltaTime);
                    transform.LookAt(target);
                    yield return null;
                }

                //Stop and attack
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(AttackRoutine());

                yield return new WaitForSecondsRealtime(waitTimeAtPoint);

                currentPointIndex = (currentPointIndex + 1) % pathpoints.Length;
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        //Choose one of the attack
        if (attackQueue.Count == 0)
            RebuildAttackQueue();

        string attack = attackQueue.Dequeue();

        if (attack == "Projectile")
        {
            GameObject projectile = Instantiate(projectilePreFab, firePoint.position, firePoint.rotation);
            Rigidbody _rigidbody = projectile.GetComponent<Rigidbody>();
            _rigidbody.linearVelocity = transform.forward*Time.deltaTime*10f;
        }
        else if (attack == "Area")
        {
            GameObject area = Instantiate(areaAttackPrefab, transform.position, Quaternion.identity);
            Destroy(area, 2f);
        }

        yield return new WaitForSecondsRealtime(1.5f);
        isAttacking= false;
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
        else if (healthPercent <0.4f)
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
