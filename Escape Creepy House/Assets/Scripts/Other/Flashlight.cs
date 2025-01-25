using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool isOn = false;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject flashlight;

    [Header("AUDIO")]
    [SerializeField] private AudioSource flashlightAudioSource;
    [SerializeField] private AudioClip flashlightAudioClip;

    [Header("OTHER")]
    public Light newLight;

    private void Start()
    {
        newLight = GetComponent<Light>();
        newLight.enabled = false;
        DeactivateGameObject.deactivateInstance.DeactivateObject(flashlight);
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
