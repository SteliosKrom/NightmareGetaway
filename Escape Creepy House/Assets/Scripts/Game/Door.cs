using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimator;

    private bool isOpen;
    private bool active = true;
    private bool inactive = false;

    private void Start()
    {
        isOpen = inactive;
        doorAnimator.SetBool("IsOpen", inactive);
        doorAnimator.SetBool("IsClosed", inactive);
        doorAnimator.SetBool("IsIdle", active);
    }

    public virtual void OnInteract()
    {
        DoorInteraction();
    }

    public void DoorInteraction()
    {
        if (isOpen == inactive)
        {
            DoorOpens();
        }
        else if (isOpen == active)
        {
            DoorCloses();
        }
    }

    public void DoorOpens()
    {
        doorAnimator.SetBool("IsOpen", active);
        doorAnimator.SetBool("IsClosed", inactive);
        isOpen = active;
    }

    public void DoorCloses()
    {
        doorAnimator.SetBool("IsOpen", inactive);
        doorAnimator.SetBool("IsClosed", active);
        isOpen = inactive;
    }
}
