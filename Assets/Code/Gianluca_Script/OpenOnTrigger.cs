using UnityEngine;

[RequireComponent(typeof(Animator))]

public class OpenOnTrigger : MonoBehaviour
{
    [SerializeField] private Animator _AC;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _AC.SetBool("isOpen", true);
            Debug.Log("ASD");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _AC.SetBool("isOpen", false);
        }
    }
}
