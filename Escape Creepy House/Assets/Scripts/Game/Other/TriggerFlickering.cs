using UnityEngine;

public class TriggerFlickering : MonoBehaviour
{
    [Header("SCRIPT REFERENCES")]
    [SerializeField] private FlashlightFlickering flashlightFlickering;
    [SerializeField] private Interactor interactor;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject onTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && interactor.hasFlashlight)
        {
            flashlightFlickering.TriggerFlicker();
            onTrigger.SetActive(false);
        }
    }
}
