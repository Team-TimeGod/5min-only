using UnityEngine;

public class Checkkey : MonoBehaviour
{

    public InventoryMananger _IM;
    public Animator _AC;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        if (_IM.GetKey(0) && _IM.GetKey(1))
        {
            _AC.SetBool("Open", true);
        }

        }
    }
}
