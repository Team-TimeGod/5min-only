using UnityEngine;

public class Enemy_Zone_Checker : MonoBehaviour
{
    [Header("Zone Settings")]
    [SerializeField] private float checkRadius = 10f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Animation")]
    [SerializeField] private Animator targetAnimator;
    [SerializeField] private string animationTriggerName = "Open";

    private bool hasTriggered = false;

    private void Update()
    {
        if (hasTriggered) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, enemyLayer);

        bool allDead = true;

        foreach (Collider col in colliders)
        {
            HealthSystem enemyHealth = col.GetComponent<HealthSystem>();
            if (enemyHealth != null && enemyHealth.getLife() > 0)
            {
                allDead = false;
                break;
            }
        }

        if (allDead && colliders.Length > 0)
        {
            hasTriggered = true;
            Debug.Log("Tutti i nemici nella zona sono stati sconfitti!");
            if (targetAnimator != null)
                targetAnimator.SetTrigger(animationTriggerName);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
