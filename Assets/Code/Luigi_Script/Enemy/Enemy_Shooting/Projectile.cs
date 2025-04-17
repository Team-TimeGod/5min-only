using UnityEngine;


public class Projectile : MonoBehaviour
{
    public float lifeTime = 8f;
    public int damage = 1;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Proiettile ha colpito il player");
            other.GetComponent<HealthSystem>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

