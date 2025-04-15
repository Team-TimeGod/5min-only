using System.Collections;
using UnityEngine;

public class Damage_Cube : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float warningDuration = 0.5f;
    [SerializeField] private float activeDuration = 1.5f; // ⏱ tempo in cui il cubo resta attivo
    [SerializeField] private Color warningColor = Color.yellow;
    [SerializeField] private Color activeColor = Color.red;
    [SerializeField] private Renderer rend;

    private bool isActive = false;

    private void Awake()
    {
        if (rend == null)
            rend = GetComponent<Renderer>();

        rend.material.color = Color.white;
    }

    public void ActivateWithWarning()
    {
        StartCoroutine(ActivateRoutine());
    }

    private IEnumerator ActivateRoutine()
    {
        rend.material.color = warningColor;
        yield return new WaitForSeconds(warningDuration);
        Activate();
        yield return new WaitForSeconds(activeDuration);
        Deactivate();
    }

    public void Activate()
    {
        isActive = true;
        rend.material.color = activeColor;
    }

    public void Deactivate()
    {
        isActive = false;
        rend.material.color = Color.white;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        HealthSystem health = other.GetComponent<HealthSystem>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }
    }
}
