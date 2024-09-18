using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomDoor : MonoBehaviour
{
    public Animator bedroomAnimator;
    private bool isOpen;
    private bool active = true;
    private bool inactive = false;

    private void Start()
    {
        isOpen = inactive;
        bedroomAnimator.SetBool("Closed", inactive);
        bedroomAnimator.SetBool("Opened", inactive);
        bedroomAnimator.SetBool("Idle", active);
    }

    public virtual void OnBedroomDoorInteract()
    {
        BedroomDoorInteraction();
    }

    public void BedroomDoorInteraction()
    {
        if (isOpen == inactive)
        {
            BedroomDoorOpens();
        }
        else
        {
            BedroomDoorCloses();
        }
    }

    public void BedroomDoorOpens()
    {
        bedroomAnimator.SetBool("Opened", active);
        bedroomAnimator.SetBool("Closed", inactive);
        isOpen = active;
    }

    public void BedroomDoorCloses()
    {
        bedroomAnimator.SetBool("Opened", inactive);
        bedroomAnimator.SetBool("Closed", active);
        isOpen = inactive;
    }

}
