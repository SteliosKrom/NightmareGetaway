using System.Collections;
using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;

    [Header("AUDIO")]
    public AudioSource equipKeysAudioSource;
    public AudioSource drinkAudioSource;
    public AudioSource eatAudioSource;

    public AudioClip drinkAudioclip;
    public AudioClip eatAudioClip;
    public AudioClip equipKeysAudioClip;

    [Header("OTHER")]
    public Transform interactionSource;
    public Flashlight flashlight;
    public GameObject foundGarageKey;
    public GameObject foundFlashlight;
    public GameObject foundMainDoorKey;
    public GameObject foundRoomKey;
    public GameObject foundPhone;
    public GameObject interactionUI;
    public GameObject roomKey;
    public GameObject mainDoorKey;
    public GameObject garageKey;
    public GameObject waterGlass;
    public GameObject food;
    public GameObject phone;

    public float interactionRange;
    private float displayTextDelay = 1f;
    private float interactionDelay = 0.3f;
    public bool hasFlashlight = false;
    private bool active = true;
    private bool inactive = false;

    private void Start()
    {
        hasFlashlight = inactive;
        foundFlashlight.SetActive(inactive);
        foundRoomKey.SetActive(inactive);
        foundMainDoorKey.SetActive(inactive);
        foundGarageKey.SetActive(inactive);
        foundPhone.SetActive(inactive);
        interactionUI.SetActive(inactive);
        roomKey.SetActive(active);
        mainDoorKey.SetActive(inactive);
        food.SetActive(inactive);
        phone.SetActive(inactive);
        waterGlass.SetActive(inactive);
        garageKey.SetActive(inactive);
    }

    void Update()
    {
        DetectInteractable();
        DebugRaycast();
        InputForInteraction();
        InputForFlashlight();
    } 

    public void InputForFlashlight()
    {
        if (hasFlashlight && Input.GetKeyDown(KeyCode.F))
        {
            if (RoundManager.instance.currentState != GameState.pause && RoundManager.instance.currentState != GameState.onSettings && RoundManager.instance.currentState != GameState.onMainMenu)
            {
                flashlight.Toggle();
            }
        }
    }

    public void InputForInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    public void TryInteract()
    {
        Ray ray = new Ray(interactionSource.position, interactionSource.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            DoorBase doorBase = hit.collider.GetComponent<DoorBase>();
            HandleInteractableGameObject(interactable, doorBase);
        }
    }

    public void HandleInteractableGameObject(Interactable interactable, DoorBase doorBase)
    {
        if (interactable != null)
        {
            interactable.OnInteract();
            if (interactable.gameObject.CompareTag("RoomKey"))
            {
                taskManager.CompleteTask();
                StartCoroutine(DisplayFoundRoomkeyText());
                waterGlass.SetActive(active);
                AudioManager.instance.PlaySound(equipKeysAudioSource, equipKeysAudioClip);
            }
            else if (interactable.gameObject.CompareTag("WaterGlass"))
            {
                taskManager.CompleteTask();
                food.SetActive(active);
                AudioManager.instance.PlaySound(drinkAudioSource, drinkAudioclip);
            }
            else if (interactable.gameObject.CompareTag("Food"))
            {
                taskManager.CompleteTask();
                garageKey.SetActive(active);
                AudioManager.instance.PlaySound(eatAudioSource, eatAudioClip);
            }
            else if (interactable.gameObject.CompareTag("Phone"))
            {
                taskManager.CompleteTask();
                mainDoorKey.SetActive(active);
                StartCoroutine(DisplayFoundPhoneText());
            }
            else if (interactable.gameObject.CompareTag("Flashlight"))
            {
                hasFlashlight = true;
                StartCoroutine(DisplayFoundFlashlightText());
            }
            else if (interactable.gameObject.CompareTag("GarageKey"))
            {
                StartCoroutine(DisplayFoundGarageKeyText());
                phone.SetActive(active);
                AudioManager.instance.PlaySound(equipKeysAudioSource, equipKeysAudioClip);
            }
            else if (interactable.gameObject.CompareTag("MainDoorKey"))
            {
                taskManager.CompleteTask();
                StartCoroutine(DisplayFoundMainDoorKeyText());
                AudioManager.instance.PlaySound(equipKeysAudioSource, equipKeysAudioClip);
            }
        }

        if (doorBase != null)
        {
            if (doorBase.gameObject.CompareTag("KidsDoor"))
            {
                doorBase.OnDoorInteract();
            }
            else if (doorBase.gameObject.CompareTag("BathroomDoor"))
            {
                doorBase.OnDoorInteract();
            }
            else if (doorBase.gameObject.CompareTag("BedroomDoor"))
            {
                doorBase.OnDoorInteract();
            }
            else if (doorBase.gameObject.CompareTag("GarageDoor"))
            {
                doorBase.OnDoorInteract();
            }
        }
    }

    public void DetectInteractable()
    {
        Ray ray = new Ray(interactionSource.position, interactionSource.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            DoorBase doorBase = hit.collider.GetComponent<DoorBase>();

            if (interactable != null || doorBase != null && RoundManager.instance.currentState != GameState.pause && RoundManager.instance.currentState != GameState.onSettings)
            {
                interactionUI.SetActive(active);
            }
            else
            {
                interactionUI.SetActive(inactive);
            }
        }
        else
        {
            interactionUI.SetActive(inactive);
        }
    }

    void DebugRaycast()
    {
        if (interactionSource != null)
        {
            Debug.DrawRay(interactionSource.position, interactionSource.forward * interactionRange, Color.red);
        }
        else
        {
            Debug.LogWarning("interactionSource is not assigned in the Inspector!");
        }
    }

    public IEnumerator DisplayFoundFlashlightText()
    {
        foundFlashlight.SetActive(active);
        yield return new WaitForSeconds(displayTextDelay);
        foundFlashlight.SetActive(inactive);
    }

    public IEnumerator DisplayFoundGarageKeyText()
    {
        foundGarageKey.SetActive(active);
        yield return new WaitForSeconds(displayTextDelay);
        foundGarageKey.SetActive(inactive);
    }

    public IEnumerator DisplayFoundMainDoorKeyText()
    {
        foundMainDoorKey.SetActive(active);
        yield return new WaitForSeconds(displayTextDelay);
        foundMainDoorKey.SetActive(inactive);
    }

    public IEnumerator DisplayFoundRoomkeyText()
    {
        foundRoomKey.SetActive(active);
        yield return new WaitForSeconds(displayTextDelay);
        foundRoomKey.SetActive(inactive);
    }

    public IEnumerator DisplayFoundPhoneText()
    {
        foundPhone.SetActive(active);
        yield return new WaitForSeconds(displayTextDelay);
        foundPhone.SetActive(inactive);
    }
}
