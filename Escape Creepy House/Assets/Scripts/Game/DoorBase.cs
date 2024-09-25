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
    public DoorStates currentState;

    public string openParameter;
    public string closeParameter;
    public string idleParameter;

    private bool active = true;
    private bool inactive = false;
    private bool canInteract = false;

    private float interactioDelay = 1f;

    [Header("AUDIO")]
    public AudioSource doorOpenedAudioSource;
    public AudioSource doorClosedAudioSource;

    public AudioClip doorOpenedAudioClip;
    public AudioClip doorClosedAudioClip;
    
    public void Start()
    {
        currentState = DoorStates.isClosed;
        doorAnimator.SetBool(openParameter, inactive);
        doorAnimator.SetBool(closeParameter, inactive);
        doorAnimator.SetBool(idleParameter, active);
    }

    public virtual void OnDoorInteract()
    {
        if (canInteract == inactive)
        {
            return;
        }

        canInteract = inactive;
        if (currentState == DoorStates.isClosed && RoundManager.instance.currentKeyState != KeyState.none)
        {
            OpenDoor();
            AudioManager.instance.PlaySound(doorOpenedAudioSource, doorOpenedAudioClip);
        }
        else if (currentState == DoorStates.isOpened && RoundManager.instance.currentKeyState != KeyState.none)
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
        canInteract = inactive;
        yield return new WaitForSeconds(interactioDelay);
        canInteract = active;
    }
}
