using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private new Light light;
    [SerializeField] private AudioSource flashlightAudioSource;
    [SerializeField] private AudioClip flashlightAudioClip;

    private void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && light.enabled == false && RoundManager.instance.currentState == GameState.playing)
        {
            light.enabled = true;
            AudioManager.instance.PlaySound(flashlightAudioSource, flashlightAudioClip);
            flashlightAudioSource.volume = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.F) && light.enabled == true && RoundManager.instance.currentState == GameState.playing)
        {
            light.enabled = false;
            AudioManager.instance.PlaySound(flashlightAudioSource, flashlightAudioClip);
            flashlightAudioSource.volume = 1f;
        }
    }
}
