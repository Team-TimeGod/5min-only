using UnityEngine;

public class PlayOnEnter : MonoBehaviour
{
    [SerializeField] private string BoolName;
    [SerializeField] private bool BoolValue;
    [SerializeField] private Animator _AC;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _AC.SetBool(BoolName, BoolValue);
        }
    }
}
