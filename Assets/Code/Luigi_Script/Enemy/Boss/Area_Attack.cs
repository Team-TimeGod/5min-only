using UnityEngine;
using System.Collections;

public class Area_Attack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float delayBeforeActivation = 1.5f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float activeTime = 1f;
    [SerializeField] private LayerMask targetLayer;

    [Header("VFX")]
    [SerializeField] private GameObject warningEffect;
    [SerializeField] private GameObject explosionEffect;

    private bool isActive = false;
    private Transform target;

    public void Initialize(Transform playerTarget)
    {
        target = playerTarget;
        StartCoroutine(ActivateTriggerAfterDelay());
    }

    private IEnumerator ActivateTriggerAfterDelay()
    {
        // Warning visual
        if (warningEffect != null)
            Instantiate(warningEffect, transform.position, Quaternion.identity, transform);

        // Aspetta prima di attivare l'area
        yield return new WaitForSeconds(delayBeforeActivation);
        isActive = true;

        // Effetto visivo d'attivazione
        if (explosionEffect != null)
        {
            GameObject fx = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(fx, 2f);
        }

        // L'area rimane attiva per un po' prima di sparire
        yield return new WaitForSeconds(activeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        // Verifica se il collider è il target (player)
        if (((1 << other.gameObject.layer) & targetLayer) != 0 && other.transform == target)
        {
            HealthSystem health = other.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
