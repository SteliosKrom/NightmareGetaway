using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomDoor : MonoBehaviour
{
    public Animator bathroomAnimator;

    private bool isOpen;
    private bool active = true;
    private bool inactive = false;

    private void Start()
    {
        isOpen = inactive;
        bathroomAnimator.SetBool("Opened", inactive);
        bathroomAnimator.SetBool("Closed", inactive);
        bathroomAnimator.SetBool("Idle", active);
    }

    public virtual void OnBathroomDoorInteract()
    {
        BathroomDoorInteraction();
    }

    public void BathroomDoorInteraction()
    {
        if (isOpen == inactive)
        {
            BathroomDoorOpens();
        }
        else
        {
            BathroomDoorCloses();
        }
    }

    public void BathroomDoorOpens()
    {
        bathroomAnimator.SetBool("Opened", active);
        bathroomAnimator.SetBool("Closed", inactive);
        isOpen = active;
    }

    public void BathroomDoorCloses()
    {
        bathroomAnimator.SetBool("Opened", inactive);
        bathroomAnimator.SetBool("Closed", active);
        isOpen = inactive;
    }
}
