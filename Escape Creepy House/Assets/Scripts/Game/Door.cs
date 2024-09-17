using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator KidsDoorAnimator;
    public Animator bathroomAnimator;

    private bool isOpen;
    private bool active = true;
    private bool inactive = false;

    private void Start()
    {
        isOpen = inactive;
        KidsDoorAnimator.SetBool("IsOpen", inactive);
        KidsDoorAnimator.SetBool("IsClosed", inactive);
        KidsDoorAnimator.SetBool("IsIdle", active);
        bathroomAnimator.SetBool("Opened", inactive);
        bathroomAnimator.SetBool("Closed", inactive);
        bathroomAnimator.SetBool("Idle", active);
    }

    public virtual void OnKidsDoorInteract()
    {
        KidsDoorInteraction();
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

    public void KidsDoorInteraction()
    {
        if (isOpen == inactive)
        {
            KidsDoorOpens();
        }
        else
        {
            KidsDoorCloses();
        }
    }

    public void KidsDoorOpens()
    {
        KidsDoorAnimator.SetBool("IsOpen", active);
        KidsDoorAnimator.SetBool("IsClosed", inactive);
        isOpen = active;
    }

    public void KidsDoorCloses()
    {
        KidsDoorAnimator.SetBool("IsOpen", inactive);
        KidsDoorAnimator.SetBool("IsClosed", active);
        isOpen = inactive;
    }
}
