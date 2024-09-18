using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [Header("CLASSES")]
    [SerializeField] private Light newLight;
    [SerializeField] private AudioSource flashlightAudioSource;
    [SerializeField] private AudioClip flashlightAudioClip;

    private bool isOn = false;

    private void Start()
    {
        newLight = GetComponent<Light>();
        newLight.enabled = false;
    }

    public void Toggle()
    {
        isOn = !isOn;
        newLight.enabled = isOn;

        if (flashlightAudioSource != null && flashlightAudioClip != null)
        {
            flashlightAudioSource.PlayOneShot(flashlightAudioClip);
        }
    }
}
