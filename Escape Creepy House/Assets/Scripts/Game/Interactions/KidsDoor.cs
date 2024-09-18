using UnityEngine;

public class KidsDoor : MonoBehaviour
{
    public Animator kidsDoorAnimator;

    private bool active = true;
    private bool inactive = false;
    private bool isOpen;

    private void Start()
    {
        isOpen = inactive;
        kidsDoorAnimator.SetBool("IsOpen", inactive);
        kidsDoorAnimator.SetBool("IsClosed", inactive);
        kidsDoorAnimator.SetBool("IsIdle", active);
    }

    public virtual void OnKidsDoorInteract()
    {
        KidsDoorInteraction();
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
        kidsDoorAnimator.SetBool("IsOpen", active);
        kidsDoorAnimator.SetBool("IsClosed", inactive);
        isOpen = active;
    }

    public void KidsDoorCloses()
    {
        kidsDoorAnimator.SetBool("IsOpen", inactive);
        kidsDoorAnimator.SetBool("IsClosed", active);
        isOpen = inactive;
    }
}
