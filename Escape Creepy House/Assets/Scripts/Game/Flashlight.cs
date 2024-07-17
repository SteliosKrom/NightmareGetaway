using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [Header("CLASSES")]
    [SerializeField] private Light newLight;
    [SerializeField] private AudioSource flashlightAudioSource;
    [SerializeField] private AudioClip flashlightAudioClip;

    private void Start()
    {
        newLight = GetComponent<Light>();
        newLight.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && newLight.enabled == false && RoundManager.instance.currentState == GameState.playing)
        {
            newLight.enabled = true;
            AudioManager.instance.PlaySound(flashlightAudioSource, flashlightAudioClip);
            flashlightAudioSource.volume = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.F) && newLight.enabled == true && RoundManager.instance.currentState == GameState.playing)
        {
            newLight.enabled = false;
            AudioManager.instance.PlaySound(flashlightAudioSource, flashlightAudioClip);
            flashlightAudioSource.volume = 1f;
        }
    }
}
