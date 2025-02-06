using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum DoorStates
{
    isOpened, 
    isClosed,
    isIdle
}

public class DoorBase : MonoBehaviour
{
    public string openParameter;
    public string closeParameter;
    public string idleParameter;

    private bool active = true;
    private bool inactive = false;
    private bool canInteract = true;
    private bool isLocked = false;

    private float interactioDelay = 1f;

    [Header("OTHER")]
    public Animator doorAnimator;
    public DoorStates currentState;

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
        if (!canInteract)
        {
            return;
        }
        
        if (isLocked)
        {
            return;
        }
        canInteract = inactive;

        if (currentState == DoorStates.isIdle || currentState == DoorStates.isClosed)
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

    private IEnumerator InteractionDelay()
    {
        yield return new WaitForSeconds(interactioDelay);
        canInteract = active;
        Debug.Log("Can interact with the door again.");
    }
}
