using System.Collections;
using UnityEngine;

public enum DoorStates
{
    isOpened, 
    isClosed,
    isIdle
}

public class DoorBase : MonoBehaviour
{
    public Animator doorAnimator;
    public DoorStates currentState;

    public string openParameter;
    public string closeParameter;
    public string idleParameter;
    private bool active = true;
    private bool inactive = false;
    private bool canInteract = true;
    private float interactioDelay = 1f;

    [Header("AUDIO")]
    public AudioSource doorOpenedAudioSource;
    public AudioSource doorClosedAudioSource;

    public AudioClip doorOpenedAudioClip;
    public AudioClip doorClosedAudioClip;
    
    public void Start()
    {
        currentState = DoorStates.isIdle;
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
        if (currentState == DoorStates.isIdle)
        {
            OpenDoor();
            AudioManager.instance.PlaySound(doorOpenedAudioSource, doorOpenedAudioClip);
        }
        else if (currentState == DoorStates.isOpened)
        {
            CloseDoor();
            AudioManager.instance.PlaySound(doorClosedAudioSource, doorClosedAudioClip);
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
