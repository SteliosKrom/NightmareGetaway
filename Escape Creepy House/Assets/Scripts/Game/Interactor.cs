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
    public TextMeshProUGUI foundItemText;
    public GameObject foundItem;
    public GameObject interactionUI;

    public float interactionRange;
    public bool hasFlashlight = false;
    private bool active = true;
    private bool inactive = false;

    private void Start()
    {
        hasFlashlight = inactive;
        foundItem.SetActive(inactive);

        if (interactionUI != null)
        {
            interactionUI.SetActive(inactive);
        }
    }

    void Update()
    {
        DetectInteractable();
        DebugRaycast();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        if (hasFlashlight && Input.GetKeyDown(KeyCode.F))
        {
            if (RoundManager.instance.currentState != GameState.pause && RoundManager.instance.currentState != GameState.onSettings && RoundManager.instance.currentState != GameState.onMainMenu)
            {
                flashlight.Toggle();
            }
        }
    }

    public void TryInteract()
    {
        Ray ray = new Ray(interactionSource.position, interactionSource.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.OnInteract();
                if (interactable.gameObject.CompareTag("RoomKey"))
                {
                    taskManager.CompleteTask();
                    StartCoroutine(DisplayFoundItemText());
                    AudioManager.instance.PlaySound(equipKeysAudioSource, equipKeysAudioClip);
                }
                else if (interactable.gameObject.CompareTag("WaterGlass"))
                {
                    taskManager.CompleteTask();
                    AudioManager.instance.PlaySound(drinkAudioSource, drinkAudioclip);
                }
                else if (interactable.gameObject.CompareTag("Food"))
                {   
                    taskManager.CompleteTask();
                    AudioManager.instance.PlaySound(eatAudioSource, eatAudioClip);
                }
                else if (interactable.gameObject.CompareTag("Phone"))
                {
                    taskManager.CompleteTask();
                    StartCoroutine(DisplayFoundItemText());
                }
                else if (interactable.gameObject.CompareTag("Flashlight"))
                {
                    StartCoroutine(DisplayFoundItemText());
                    hasFlashlight = true;
                }
                else if (interactable.gameObject.CompareTag("GarageKey"))
                {
                    StartCoroutine(DisplayFoundItemText());
                    AudioManager.instance.PlaySound(equipKeysAudioSource, equipKeysAudioClip);
                }
                else if (interactable.gameObject.CompareTag("MainDoorKey"))
                {
                    taskManager.CompleteTask();
                    StartCoroutine(DisplayFoundItemText());
                    AudioManager.instance.PlaySound(equipKeysAudioSource, equipKeysAudioClip);
                }
            }
        }
    }

    public IEnumerator DisplayFoundItemText()
    {
        foundItem.SetActive(active);
        yield return new WaitForSeconds(1f);
        foundItem.SetActive(inactive);
    }

    public void DetectInteractable()
    {
        Ray ray = new Ray(interactionSource.position, interactionSource.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && RoundManager.instance.currentState != GameState.pause && RoundManager.instance.currentState != GameState.onSettings)
            {
                interactionUI.SetActive(active);
            }
            else
            {
                interactionUI.SetActive(inactive);
            }
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

    public void OpenFoundItemTextDelay()
    {
        foundItem.SetActive(active);
    }

    public void CloseFoundItemTextDelay()
    {
        foundItem.SetActive(inactive);
    }
}
