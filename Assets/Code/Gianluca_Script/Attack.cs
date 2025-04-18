using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    /*
     
    Questo script va collegato all'arma del nemico o del player
     
     */
    [Header("Gameobject Setup")]
    [SerializeField] private Type _type;
    
    [Header("Damage Setup")]
    [SerializeField] private float _damage;
    [SerializeField] private bool _isAttacking;

    [Header("Reference")]
    [SerializeField] HealthSystem _targetHS;
    [SerializeField] Animator _AC;
    enum Type : byte 
    {
        Player = 0,
        Enemy = 1
    }

    private void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAttacking)
        {
            // Gestione del danno ai nemici
            if (other.CompareTag("Enemy"))
            {
                _targetHS = other.GetComponent<HealthSystem>();
            }

            if (_targetHS != null)
            {
                _targetHS.TakeDamage(_damage * Time.timeScale); // In base al moltiplicatore del tempo arreco +- danno
                _isAttacking = false; // Replace with false in animation
            }

            // KICK BACK
            if (other.CompareTag("Projectile"))
            {
                Rigidbody projectileRb = other.GetComponent<Rigidbody>();
                if (projectileRb != null)
                {
                    Vector3 directionToOrigin = (transform.position - other.transform.position).normalized;
                    projectileRb.linearVelocity = directionToOrigin * projectileRb.linearVelocity.magnitude;

                    Debug.Log("Proiettile respinto");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _targetHS = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Play Animation with _isAttacking = true;
            //_isAttacking = true; //Replace with animation
            _AC.SetBool("isAttacking", true); //start the animation
            _isAttacking = true;
            Debug.LogWarning("ATTACK");
        }

        if (_AC.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            StartCoroutine("Wait");
        }
        
            Debug.Log("Value: " + _isAttacking);
            _AC.SetBool("isAttacking", _isAttacking); //Pass the value to the animator bool cuz _isAttacking at the end of clip go false
        

        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(_AC.GetCurrentAnimatorStateInfo(0).length);
        _isAttacking = false;
        _AC.SetBool("isAttacking", false);
        Debug.Log("Waiting");
    }

}
