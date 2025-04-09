using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; 

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Deflect(Vector3 direction)
    {
        Vector3 newDirection = -direction.normalized; 
        transform.forward = newDirection; 

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = newDirection * speed; 
        }
    }
}