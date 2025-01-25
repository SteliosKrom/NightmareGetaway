using System.Collections;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("TYPES")]
    public float interactionRange;
    private float displayTextDelay = 1.5f;
    private float lockedUIDelay = 1f;
    private float doorCollidersDelay = 1f;
    private float toggleDelay = 1f;

    private bool active = true;
    private bool inactive = false;
    private bool isLocked = false;
    public bool canToggle = true;
    public bool hasFlashlight = true;
    public bool isEquipped = true;

    [Header("SCRIPT REFERENCES")]
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private Flashlight flashlight;
    [SerializeField] private FlashlightFlickering flashlightFlickering;
    [SerializeField] private PlayerController playerController;

    [Header("AUDIO")]
    public AudioSource equipKeysAudioSource;
    public AudioSource drinkAudioSource;
    public AudioSource eatAudioSource;
    public AudioSource lockedAudioSource;
    public AudioClip drinkAudioclip;
    public AudioClip eatAudioClip;
    public AudioClip equipKeysAudioClip;
    public AudioClip lockedAudioClip;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject foundGarageKey;
    [SerializeField] private GameObject foundFlashlight;
    [SerializeField] private GameObject foundMainDoorKey;
    [SerializeField] private GameObject foundRoomKey;
    [SerializeField] private GameObject foundPhone;
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private GameObject lockedUI;
    [SerializeField] private GameObject roomKey;
    [SerializeField] private GameObject mainDoorKey;
    [SerializeField] private GameObject garageKey;
    [SerializeField] private GameObject waterGlass;
    [SerializeField] private GameObject food;
    [SerializeField] private GameObject phone;
    [SerializeField] private GameObject doorBoxCollider;
    [SerializeField] private GameObject _flashlight;

    [Header("OTHER")]
    [SerializeField] private BoxCollider fridgeCollider;
    [SerializeField] private BoxCollider[] doorColliders;
    [SerializeField] private BoxCollider[] doorHandleColliders;
    [SerializeField] private Transform interactionSource;
    [SerializeField] private Animator flashlightAnimator;

    private void Start()
    {
        DisplayItems(roomKey, mainDoorKey, food, phone, waterGlass, garageKey);
        DisplayUI(foundFlashlight, foundRoomKey, foundMainDoorKey, foundGarageKey, lockedUI, foundPhone, interactionUI);
    }

    public void DisplayItems(GameObject roomKey, GameObject mainDoorKey, GameObject food, 
        GameObject phone, GameObject waterGlass, GameObject garageKey)
    {
        ActivateGameObject.activateInstance.ActivateObject(roomKey);
        DeactivateGameObject.deactivateInstance.DeactivateItems();
    }

    public void DisplayUI(GameObject foundFlashlight, GameObject foundRoomKey, GameObject foundMainDoorKey,
        GameObject foundGarageKey, GameObject lockedUI, GameObject foundPhone, GameObject interactionUI)
    {
        DeactivateGameObject.deactivateInstance.DeactivateDisplayUI();
    }

    void Update()
    {
        DetectInteractable();
        DebugRaycast();
        InputForInteraction();
        InputForFlashlight();
        InputForEquipFlashlight();
    }

    public void InputForFlashlight()
    {
        bool onPlaying = RoundManager.instance.currentGameState == GameState.playing;

        if (hasFlashlight && canToggle && isEquipped && Input.GetKeyDown(KeyCode.F)) 
        {
            if (onPlaying)
            {
                flashlight.Toggle();
            }
            StartCoroutine(ToggleDelay());
        }
    }

    public void InputForEquipFlashlight()
    {
        bool onPlaying = RoundManager.instance.currentGameState == GameState.playing;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (onPlaying)
            {
                if (isEquipped)
                {
                    flashlightAnimator.SetBool("Unequipped", active);
                    flashlightAnimator.SetBool("Equipped", inactive);
                    isEquipped = inactive;
                    flashlight.newLight.enabled = inactive;
                }
                else
                {
                    flashlightAnimator.SetBool("Equipped", active);
                    flashlightAnimator.SetBool("Unequipped", inactive);
                    isEquipped = active;
                    flashlight.newLight.enabled = active;
                    flashlight.isOn = active;
                }
            }
            else
            {
                onPlaying = RoundManager.instance.currentGameState != GameState.playing;
                Debug.Log("You can't equip the flashlight right now! Game state is: " + onPlaying);
            }
        }
    }

    private IEnumerator ToggleDelay()
    {
        canToggle = inactive;
        yield return new WaitForSeconds(toggleDelay);
        canToggle = active;
    }

    public void InputForInteraction()
    {
        bool noPause = RoundManager.instance.currentGameState != GameState.pause;

        if (Input.GetKeyDown(KeyCode.E) && noPause)
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
            fridgeCollider.enabled = active;
            AudioManager.instance.PlaySound(drinkAudioSource, drinkAudioclip);
        }
        else if (interactable.gameObject.CompareTag("Food"))
        {
            taskManager.CompleteTask();
            garageKey.SetActive(active);
            doorBoxCollider.SetActive(inactive);
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
            hasFlashlight = active;
            isEquipped = active;
            ActivateGameObject.activateInstance.ActivateObject(_flashlight);
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
        if (doorBase.gameObject.CompareTag("SecondBedroomDoor"))
        {
            doorBase.OnDoorInteract();
            StartCoroutine(DoorCollidersDelay(doorColliders[4]));
            StartCoroutine(doorHandleCollidersDelay(doorHandleColliders[4]));
        }
        else if (doorBase.gameObject.CompareTag("SecondBathroomDoor"))
        {
            doorBase.OnDoorInteract();
            StartCoroutine(DoorCollidersDelay(doorColliders[3]));
            StartCoroutine(doorHandleCollidersDelay(doorHandleColliders[3]));
        }
        else if (doorBase.gameObject.CompareTag("ClothingsDoor"))
        {
            doorBase.OnDoorInteract();
            StartCoroutine(DoorCollidersDelay(doorColliders[1]));
            StartCoroutine(doorHandleCollidersDelay(doorHandleColliders[1]));
        }
        else if (doorBase.gameObject.CompareTag("BedroomDoor"))
        {
            doorBase.OnDoorInteract();
            StartCoroutine(DoorCollidersDelay(doorColliders[2]));
            StartCoroutine(doorHandleCollidersDelay(doorHandleColliders[2]));
        }
        else if (doorBase.gameObject.CompareTag("BathroomDoor"))
        {
            doorBase.OnDoorInteract();
            StartCoroutine(DoorCollidersDelay(doorColliders[6]));
            StartCoroutine(doorHandleCollidersDelay(doorHandleColliders[6]));
        }
        else if (doorBase.gameObject.CompareTag("FridgeDoor"))
        {
            doorBase.OnDoorInteract();
            StartCoroutine(DoorCollidersDelay(doorColliders[7]));
        }
    }

    public void HandleLockedDoors(DoorBase doorBase)
    {
        if (doorBase.gameObject.CompareTag("KidsDoor") 
            && RoundManager.instance.currentKeyState != KeyState.kidsRoomKey && !isLocked)
        {
            StartCoroutine(LockedUIDelay());
            AudioManager.instance.PlaySound(lockedAudioSource, lockedAudioClip);
        }
        else if (doorBase.gameObject.CompareTag("KidsDoor") 
            && RoundManager.instance.currentKeyState == KeyState.kidsRoomKey && !isLocked)
        {
            doorBase.OnDoorInteract();
            StartCoroutine(DoorCollidersDelay(doorColliders[0]));
            StartCoroutine(doorHandleCollidersDelay(doorHandleColliders[0]));
            isLocked = inactive;
            RoundManager.instance.currentKeyState = KeyState.kidsRoomKey;
        }

        if (doorBase.gameObject.CompareTag("GarageDoor") 
            && RoundManager.instance.currentKeyState != KeyState.garageKey && !isLocked)
        {
            StartCoroutine(LockedUIDelay());
            AudioManager.instance.PlaySound(lockedAudioSource, lockedAudioClip);
        }
        else if (doorBase.gameObject.CompareTag("GarageDoor") 
            && RoundManager.instance.currentKeyState == KeyState.garageKey && !isLocked)
        {
            doorBase.OnDoorInteract();
            StartCoroutine(DoorCollidersDelay(doorColliders[5]));
            StartCoroutine(doorHandleCollidersDelay(doorHandleColliders[5]));
            isLocked = inactive;
            RoundManager.instance.currentKeyState = KeyState.garageKey;
        }

        if (doorBase.gameObject.CompareTag("MainDoor") 
            && RoundManager.instance.currentKeyState != KeyState.mainDoorKey && !isLocked)
        {
            StartCoroutine(LockedUIDelay());
            AudioManager.instance.PlaySound(lockedAudioSource, lockedAudioClip);
        }    
        else if (doorBase.gameObject.CompareTag("MainDoor") 
            && RoundManager.instance.currentKeyState == KeyState.mainDoorKey && !isLocked)
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

            if (interactable != null || doorBase != null)
            {
                if (RoundManager.instance.currentGameState == GameState.playing)
                {
                    interactionUI.SetActive(active);
                }
                else if (RoundManager.instance.currentGameState != GameState.playing)
                {
                    interactionUI.SetActive(inactive);
                }
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

    public IEnumerator DoorCollidersDelay(BoxCollider collider)
    {
        if (collider.CompareTag("KidsDoor") || collider.CompareTag("BathroomDoor") || collider.CompareTag("SecondBathroomDoor") || collider.CompareTag("SecondBedroomDoor") || collider.CompareTag("GarageDoor"))
        {
            collider.enabled = inactive;
            yield return new WaitForSeconds(doorCollidersDelay);
            collider.enabled = active;
        }
        else if (collider.CompareTag("BedroomDoor") || collider.CompareTag("ClothingsDoor") || collider.CompareTag("FridgeDoor"))
        {
            collider.enabled = inactive;
            yield return new WaitForSeconds(doorCollidersDelay);
            collider.enabled = active;
        }
    }

    public IEnumerator doorHandleCollidersDelay(BoxCollider collider)
    {

        if (collider.CompareTag("KidsDoor") || collider.CompareTag("BathroomDoor") || collider.CompareTag("SecondBathroomDoor") || collider.CompareTag("SecondBedroomDoor") || collider.CompareTag("GarageDoor"))
        {
            collider.enabled = inactive;
            yield return new WaitForSeconds(doorCollidersDelay);
            collider.enabled = active;
        }
        else if (collider.CompareTag("BedroomDoor") || collider.CompareTag("ClothingsDoor"))
        {
            collider.enabled = inactive;
            yield return new WaitForSeconds(doorCollidersDelay);
            collider.enabled = active;
        }
    }
}
