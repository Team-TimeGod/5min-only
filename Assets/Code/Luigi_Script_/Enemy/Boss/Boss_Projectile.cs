using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage = 10;
    public string targetTag = "Player"; // Assicurati che il player abbia questo tag

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            HealthSystem hs = other.GetComponent<HealthSystem>();
            if (hs != null)
            {
                hs.TakeDamage(damage);
                Debug.Log("Proiettile ha colpito " + other.name + " per " + damage + " danni.");
            }

            Destroy(gameObject); // Distrugge il proiettile dopo l'impatto
        }
        else if (!other.isTrigger) // Se colpisce qualcosa di solido (es. muro), lo distruggi comunque
        {
            Destroy(gameObject);
        }
    }
}
