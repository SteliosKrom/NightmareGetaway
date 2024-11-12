using UnityEngine;

public class TriggerFlickering : MonoBehaviour
{
    [Header("TYPES")]
    private bool inactive = false;
    private bool active = true;

    [Header("SCRIPT REFERENCES")]
    [SerializeField] private FlashlightFlickering flashlightFlickering;
    [SerializeField] private Interactor interactor;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject onTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && interactor.hasFlashlight && interactor.canToggle)
        {
            flashlightFlickering.TriggerFlicker();
            onTrigger.SetActive(inactive);
            interactor.canToggle = inactive;
        }
    }
}
