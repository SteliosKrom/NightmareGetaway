using UnityEngine;

public class DoorBase : MonoBehaviour
{
    public Animator doorAnimator;

    public string openParameter;
    public string closeParameter;
    public string idleParameter;
    protected bool isOpen;
    protected bool active = true;
    protected bool inactive = false;

    public void Start()
    {
        isOpen = inactive;
        doorAnimator.SetBool(openParameter, inactive);
        doorAnimator.SetBool(closeParameter, inactive);
        doorAnimator.SetBool(idleParameter, active);
    }

    public virtual void OnDoorInteract()
    {
        if (isOpen == inactive)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool(openParameter, active);
        doorAnimator.SetBool(closeParameter, inactive);
        doorAnimator.SetBool(idleParameter, inactive);
        isOpen = active;
    }

    public void CloseDoor()
    {
        doorAnimator.SetBool(openParameter, inactive);
        doorAnimator.SetBool(closeParameter, active);
        doorAnimator.SetBool(idleParameter, inactive);
        isOpen = inactive;
    }
}
