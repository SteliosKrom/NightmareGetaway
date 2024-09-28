using System;
using System.Collections;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;

    [Header("AUDIO")]
    public AudioSource equipKeysAudioSource;
    public AudioSource drinkAudioSource;
    public AudioSource eatAudioSource;
    public AudioSource lockedAudioSource;

    public AudioClip drinkAudioclip;
    public AudioClip eatAudioClip;
    public AudioClip equipKeysAudioClip;
    public AudioClip lockedAudioClip;

    [Header("OTHER")]
    public Transform interactionSource;
    public Flashlight flashlight;
    public GameObject foundGarageKey;
    public GameObject foundFlashlight;
    public GameObject foundMainDoorKey;
    public GameObject foundRoomKey;
    public GameObject foundPhone;
    public GameObject interactionUI;
    public GameObject lockedUI;
    public GameObject roomKey;
    public GameObject mainDoorKey;
    public GameObject garageKey;
    public GameObject waterGlass;
    public GameObject food;
    public GameObject phone;
    public GameObject doorBoxCollider;
    public BoxCollider fridgeCollider;

    public float interactionRange;
    private float displayTextDelay = 1.5f;
    private float lockedUIDelay = 1f;
    private bool isLocked = false;
    public bool hasFlashlight = false;
    private bool active = true;
    private bool inactive = false;

    private void Start()
    {
        DisplayItems(roomKey, mainDoorKey, food, phone, waterGlass, garageKey);
        DisplayUI(foundFlashlight, foundRoomKey, foundMainDoorKey, foundGarageKey, lockedUI, foundPhone, interactionUI);
    }

    public void DisplayItems(GameObject roomKey, GameObject mainDoorKey, GameObject food, GameObject phone, GameObject waterGlass, GameObject garageKey)
    {
        roomKey.SetActive(active);
        mainDoorKey.SetActive(inactive);
        food.SetActive(inactive);
        phone.SetActive(inactive);
        waterGlass.SetActive(inactive);
        garageKey.SetActive(inactive);
    }

    public void DisplayUI(GameObject foundFlashlight, GameObject foundRoomKey, GameObject foundMainDoorKey, GameObject foundGarageKey, GameObject lockedUI, GameObject foundPhone, GameObject interactionUI)
    {
        foundFlashlight.SetActive(inactive);
        foundRoomKey.SetActive(inactive);
        foundMainDoorKey.SetActive(inactive);
        foundGarageKey.SetActive(inactive);
        lockedUI.SetActive(inactive);
        foundPhone.SetActive(inactive);
        interactionUI.SetActive(inactive);
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
            if (RoundManager.instance.currentGameState != GameState.pause && RoundManager.instance.currentGameState != GameState.onSettings && RoundManager.instance.currentGameState != GameState.onMainMenu)
            {
                flashlight.Toggle();
            }
        }
    }

    public void InputForInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E) && RoundManager.instance.currentGameState != GameState.pause)
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
            HandleInteractableItems(interactable);
        }

        if (doorBase != null)
        {
            HandleLockedDoors(doorBase);
            HandleUnlockedDoors(doorBase);
        }
    }

    public void HandleInteractableItems(Interactable interactable)
    {
        interactable.OnInteract();
        if (interactable.gameObject.CompareTag("RoomKey"))
        {
            taskManager.CompleteTask();
            StartCoroutine(DisplayFoundRoomkeyText());
            waterGlass.SetActive(active);
            AudioManager.instance.PlaySound(equipKeysAudioSource, equipKeysAudioClip);
            RoundManager.instance.currentKeyState = KeyState.kidsRoomKey;
        }
        else if (interactable.gameObject.CompareTag("WaterGlass"))
        {
            taskManager.CompleteTask();
            food.SetActive(active);
            fridgeCollider.enabled = true;
            AudioManager.instance.PlaySound(drinkAudioSource, drinkAudioclip);
        }
        else if (interactable.gameObject.CompareTag("Food"))
        {
            taskManager.CompleteTask();
            garageKey.SetActive(active);
            doorBoxCollider.SetActive(inactive);
            fridgeCollider.enabled = inactive;
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
            RoundManager.instance.currentKeyState = KeyState.garageKey;
        }
        else if (interactable.gameObject.CompareTag("MainDoorKey"))
        {
            taskManager.CompleteTask();
            StartCoroutine(DisplayFoundMainDoorKeyText());
            AudioManager.instance.PlaySound(equipKeysAudioSource, equipKeysAudioClip);
            RoundManager.instance.currentKeyState = KeyState.mainDoorKey;
        }
    }

    public void HandleUnlockedDoors(DoorBase doorBase)
    {
        if (doorBase.gameObject.CompareTag("Door"))
        {
            doorBase.OnDoorInteract();
        }
    }

    public void HandleLockedDoors(DoorBase doorBase)
    {
        if (doorBase.gameObject.CompareTag("KidsDoor") && RoundManager.instance.currentKeyState != KeyState.kidsRoomKey && !isLocked)
        {
            StartCoroutine(LockedUIDelay());
            AudioManager.instance.PlaySound(lockedAudioSource, lockedAudioClip);
        }
        else if (doorBase.gameObject.CompareTag("KidsDoor") && RoundManager.instance.currentKeyState == KeyState.kidsRoomKey && !isLocked)
        {
            doorBase.OnDoorInteract();
            isLocked = inactive;
            RoundManager.instance.currentKeyState = KeyState.kidsRoomKey;
        }

        if (doorBase.gameObject.CompareTag("GarageDoor") && RoundManager.instance.currentKeyState != KeyState.garageKey && !isLocked)
        {
            StartCoroutine(LockedUIDelay());
            AudioManager.instance.PlaySound(lockedAudioSource, lockedAudioClip);
        }
        else if (doorBase.gameObject.CompareTag("GarageDoor") && RoundManager.instance.currentKeyState == KeyState.garageKey && !isLocked)
        {
            doorBase.OnDoorInteract();
            isLocked = inactive;
            RoundManager.instance.currentKeyState = KeyState.garageKey;
        }

        if (doorBase.gameObject.CompareTag("MainDoor") && RoundManager.instance.currentKeyState != KeyState.mainDoorKey && !isLocked)
        {
            StartCoroutine(LockedUIDelay());
            AudioManager.instance.PlaySound(lockedAudioSource, lockedAudioClip);
        }    
        else if (doorBase.gameObject.CompareTag("MainDoor") && RoundManager.instance.currentKeyState == KeyState.mainDoorKey && !isLocked)
        {
            doorBase.OnDoorInteract();
            isLocked = inactive;
            RoundManager.instance.currentKeyState = KeyState.mainDoorKey;
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

            if (interactable != null || doorBase != null && RoundManager.instance.currentGameState != GameState.pause && RoundManager.instance.currentGameState != GameState.onSettings)
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

    public IEnumerator LockedUIDelay()
    {
        isLocked = active;
        lockedUI.SetActive(active);
        yield return new WaitForSeconds(lockedUIDelay);
        lockedUI.SetActive(inactive);
        isLocked = inactive;
    }
}
