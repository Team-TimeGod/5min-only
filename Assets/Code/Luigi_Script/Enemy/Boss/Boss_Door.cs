using UnityEngine;

public class Boss_Door : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    private bool isOpen = false;

    public void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;
        doorAnimator.SetTrigger("Open");
    }
}
