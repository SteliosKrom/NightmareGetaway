using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool isOn = false;
    private bool active = true;

    [Header("AUDIO")]
    [SerializeField] private AudioSource flashlightAudioSource;
    [SerializeField] private AudioClip flashlightAudioClip;

    [Header("OTHER")]
    [SerializeField] private Light newLight;

    private void Start()
    {
        newLight = GetComponent<Light>();
        newLight.enabled = false;
    }

    public void Toggle()
    {
        isOn = !isOn;
        newLight.enabled = isOn;
        flashlightAudioSource.PlayOneShot(flashlightAudioClip);
    }

    public void ToggleFlicker()
    {
        isOn = !isOn;
        newLight.enabled = isOn;
    }
}
