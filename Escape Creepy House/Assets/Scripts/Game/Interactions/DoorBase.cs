using System.Collections;
using UnityEngine;

public enum DoorStates
{
    isOpened, 
    isClosed,
}

public class DoorBase : MonoBehaviour
{
    public Animator doorAnimator;
    private DoorStates currentState;

    public string openParameter;
    public string closeParameter;
    public string idleParameter;
    private bool active = true;
    private bool inactive = false;
    private bool canInteract = true;
    private float interactioDelay = 1f;
    
    public void Start()
    {
        currentState = DoorStates.isClosed;
        doorAnimator.SetBool(openParameter, inactive);
        doorAnimator.SetBool(closeParameter, inactive);
        doorAnimator.SetBool(idleParameter, active);
    }

    public virtual void OnDoorInteract()
    {
        if (canInteract == false)
        {
            return;
        }
        canInteract = false;
        if (currentState == DoorStates.isClosed)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
        StartCoroutine(InteractionDelay());
    }

    private IEnumerator InteractionDelay()
    {
        yield return new WaitForSeconds(interactioDelay);
        canInteract = true;
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool(openParameter, active);
        doorAnimator.SetBool(closeParameter, inactive);
        doorAnimator.SetBool(idleParameter, inactive);
        currentState = DoorStates.isOpened;
    }

    public void CloseDoor()
    {
        doorAnimator.SetBool(openParameter, inactive);
        doorAnimator.SetBool(closeParameter, active);
        doorAnimator.SetBool(idleParameter, inactive);
        currentState = DoorStates.isClosed;
    }
}
