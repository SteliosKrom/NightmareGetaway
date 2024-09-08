using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform interactionSource;
    public GameObject interactionUI;
    public Flashlight flashlight;

    public float interactionRange;

    private void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.Toggle();
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
                if (interactionUI != null)
                {
                    interactionUI.SetActive(false);
                }
            }
        }
        else
        {
            if (interactionUI != null)
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
}
