using System.Collections;
using TMPro;
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
    [SerializeField] private GameObject cursedBook;
    [SerializeField] private GameObject cursedCrucifix;
    [SerializeField] private GameObject cursedKnife;
    [SerializeField] private GameObject doorBoxCollider;
    [SerializeField] private GameObject _flashlight;
    [SerializeField] private GameObject candleLight;
    [SerializeField] private GameObject[] switchLights;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI cursedItemsCounter;

    [Header("AUDIO")]
    public AudioSource equipKeysAudioSource;
    public AudioSource drinkAudioSource;
    public AudioSource eatAudioSource;
    public AudioSource lockedAudioSource;
    public AudioSource switchAudioSource;
    public AudioClip drinkAudioclip;
    public AudioClip eatAudioClip;
    public AudioClip equipKeysAudioClip;
    public AudioClip lockedAudioClip;
    public AudioClip switchAudioClip;

    [Header("OTHER")]
    [SerializeField] private BoxCollider fridgeCollider;
    [SerializeField] private BoxCollider[] doorColliders;
    [SerializeField] private BoxCollider[] doorHandleColliders;
    [SerializeField] private Transform interactionSource;
    [SerializeField] private Animator flashlightAnimator;
    [SerializeField] private Light kidRoomLight;

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

        if (Input.GetKeyDown(KeyCode.E) && noPause && canToggle)
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
            OtherInteractable otherInteractable = hit.collider.GetComponent<OtherInteractable>();
            OtherInteractableSwitches otherInteractableSwitches = hit.collider.GetComponent<OtherInteractableSwitches>();
            HandleInteractableGameObject(interactable, doorBase, otherInteractable, otherInteractableSwitches);
        }
    }

    public void HandleInteractableGameObject(Interactable interactable, DoorBase doorBase,
        OtherInteractable otherInteractable, OtherInteractableSwitches otherInteractableSwitches)
    {
        if (interactable != null)
        {
            HandleInteractableCollectableItems(interactable);
        }

        if (doorBase != null)
        {
            HandleLockedDoors(doorBase);
            HandleUnlockedDoors(doorBase);
        }

        if (otherInteractable != null)
        {
            HandleInteractableItems(otherInteractable);
        }

        if (otherInteractableSwitches != null)
        {
            HandleInteractableItem(otherInteractableSwitches);
        }
    }

    public void HandleInteractableItem(OtherInteractableSwitches otherInteractableSwitches)
    {
        if (otherInteractableSwitches.gameObject.CompareTag("Switches"))
        {
            if (kidRoomLight.enabled)
            {
                AudioManager.instance.PlaySound(switchAudioSource, switchAudioClip);
            }
            else
            {
                AudioManager.instance.PlaySound(switchAudioSource, switchAudioClip);
            }
            StartCoroutine(ToggleDelay());
        }
    }

    public void HandleInteractableItems(OtherInteractable otherInteractable)
    {
        if (otherInteractable.gameObject.CompareTag("KidRoomSwitch"))
        {
            if (kidRoomLight.enabled)
            {
                kidRoomLight.enabled = inactive;
                AudioManager.instance.PlaySound(switchAudioSource, switchAudioClip);
            }
            else
            {
                kidRoomLight.enabled = active;
                AudioManager.instance.PlaySound(switchAudioSource, switchAudioClip);
            }
            StartCoroutine(ToggleDelay());
        }
    }

    public void HandleInteractableCollectableItems(Interactable interactable)
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
            mainDoorKey.SetActive(inactive);
            cursedBook.SetActive(active);
            candleLight.SetActive(active);
            cursedItemsCounter.enabled = active;
            StartCoroutine(DisplayFoundPhoneText());
        }
        else if (interactable.gameObject.CompareTag("Flashlight"))
        {
            hasFlashlight = active;
            isEquipped = active;
            _flashlight.SetActive(active);
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
            InteractWithDoor(doorBase, 4);
        }
        else if (doorBase.gameObject.CompareTag("SecondBathroomDoor"))
        {
            InteractWithDoor(doorBase, 3);
        }
        else if (doorBase.gameObject.CompareTag("ClothingsDoor"))
        {
            InteractWithDoor(doorBase, 1);
        }
        else if (doorBase.gameObject.CompareTag("BedroomDoor"))
        {
            InteractWithDoor(doorBase, 2);
        }
        else if (doorBase.gameObject.CompareTag("BathroomDoor"))
        {
            InteractWithDoor(doorBase, 6);
        }
        else if (doorBase.gameObject.CompareTag("FridgeDoor"))
        {
            doorBase.OnDoorInteract();
            StartCoroutine(DoorCollidersDelay(doorColliders[7]));
        }
    }

    public void HandleLockedDoors(DoorBase doorBase)
    {
        if (doorBase.gameObject.CompareTag("KidsDoor"))
        {
            if (RoundManager.instance.currentKidsDoorState == KidsDoorState.unlocked)
            {
                InteractWithDoor(doorBase, 0);
            }
            else
            {
                if (RoundManager.instance.currentKeyState != KeyState.kidsRoomKey && !isLocked)
                {
                    StartCoroutine(LockedUIDelay());
                    AudioManager.instance.PlaySound(lockedAudioSource, lockedAudioClip);
                }
                else if (RoundManager.instance.currentKeyState == KeyState.kidsRoomKey && !isLocked)
                {
                    InteractWithDoor(doorBase, 0);
                    RoundManager.instance.currentKeyState = KeyState.none;
                    RoundManager.instance.currentKidsDoorState = KidsDoorState.unlocked;
                }
            }
        }
        else if (doorBase.gameObject.CompareTag("GarageDoor"))
        {
            if (RoundManager.instance.currentGarageDoorState == GarageDoorState.unlocked)
            {
                InteractWithDoor(doorBase, 5);
            }
            else
            {
                if (RoundManager.instance.currentKeyState != KeyState.garageKey && !isLocked)
                {
                    StartCoroutine(LockedUIDelay());
                    AudioManager.instance.PlaySound(lockedAudioSource, lockedAudioClip);
                }
                else if (RoundManager.instance.currentKeyState == KeyState.garageKey && !isLocked)
                {
                    InteractWithDoor(doorBase, 5);
                    RoundManager.instance.currentKeyState = KeyState.none;
                    RoundManager.instance.currentGarageDoorState = GarageDoorState.unlocked;
                }
            }
        }
        else if (doorBase.gameObject.CompareTag("MainDoor"))
        {
            if (RoundManager.instance.currentMainDoorState == MainDoorState.unlocked)
            {
                doorBase.OnDoorInteract();
            }
            else
            {
                if (RoundManager.instance.currentKeyState != KeyState.mainDoorKey && !isLocked)
                {
                    StartCoroutine(LockedUIDelay());
                    AudioManager.instance.PlaySound(lockedAudioSource, lockedAudioClip);
                }
                else if (RoundManager.instance.currentKeyState == KeyState.mainDoorKey && !isLocked)
                {
                    doorBase.OnDoorInteract();
                    RoundManager.instance.currentKeyState = KeyState.none;
                    RoundManager.instance.currentMainDoorState = MainDoorState.unlocked;
                }
            }
        }
    }

    public void InteractWithDoor(DoorBase doorBase, int colliderIndex)
    {
        doorBase.OnDoorInteract();
        StartCoroutine(DoorCollidersDelay(doorColliders[colliderIndex]));
        StartCoroutine(doorHandleCollidersDelay(doorHandleColliders[colliderIndex]));
    }

    public void DetectInteractable()
    {
        Ray ray = new Ray(interactionSource.position, interactionSource.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            DoorBase doorBase = hit.collider.GetComponent<DoorBase>();
            OtherInteractable otherInteractable = hit.collider.GetComponent<OtherInteractable>();
            OtherInteractableSwitches otherInteractableSwitches = hit.collider.GetComponent<OtherInteractableSwitches>();

            if (interactable != null || doorBase != null || otherInteractable != null || otherInteractableSwitches != null)
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
        if (collider.CompareTag("KidsDoor")
            || collider.CompareTag("BathroomDoor") || collider.CompareTag("SecondBathroomDoor")
            || collider.CompareTag("SecondBedroomDoor") || collider.CompareTag("GarageDoor"))
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

        if (collider.CompareTag("KidsDoor")
            || collider.CompareTag("BathroomDoor") || collider.CompareTag("SecondBathroomDoor")
            || collider.CompareTag("SecondBedroomDoor") || collider.CompareTag("GarageDoor"))
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
