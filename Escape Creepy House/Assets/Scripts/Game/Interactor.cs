using System.Collections;
using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform interactionSource;
    public GameObject interactionUI;
    public Flashlight flashlight;
    public TextMeshProUGUI foundItemText;
    public GameObject foundItem;

    public float interactionRange;
    public bool hasFlashlight = false;

    private void Start()
    {
        hasFlashlight = false;
        foundItem.SetActive(false);

        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
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
            }
        }
        Invoke("OpenFoundItemTextDelay", 0.5f);
        Invoke("CloseFoundItemTextDelay", 1.5f);
    }

    public void DetectInteractable()
    {
        Ray ray = new Ray(interactionSource.position, interactionSource.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactionUI.SetActive(true);
            }
            else
            {
                interactionUI.SetActive(false);
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
        foundItem.SetActive(true);
    }

    public void CloseFoundItemTextDelay()
    {
        foundItem.SetActive(false);
    }
}
