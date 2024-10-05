using System.Collections;
using UnityEngine;

public class FlashlightFlickering : MonoBehaviour
{
    private float flickerDuration = 5.0f;
    private float flickerDelay = 0.1f;

    [Header("SCRIPT REFERENCES")]
    [SerializeField] private Flashlight flashlight;

    [Header("AUDIO")]
    [SerializeField] private AudioSource flickeringAudioSource;
    [SerializeField] private AudioClip flickeringAudioClip;

    private void Start()
    {
        flashlight = GetComponent<Flashlight>();
    }

    public void TriggerFlicker()
    {
        if (RoundManager.instance.currentGameState == GameState.playing)
        {
            StartCoroutine(FlickerLight());
            AudioManager.instance.PlaySound(flickeringAudioSource, flickeringAudioClip);
        }
        else
        {
            Time.timeScale = 0f;
            AudioManager.instance.StopSound(flickeringAudioSource);
        }
    }

    public IEnumerator FlickerLight()
    {
        float timePassed = 0;

        while (timePassed < flickerDuration)
        {
            flashlight.ToggleFlicker();
            timePassed += flickerDelay;
            yield return new WaitForSeconds(flickerDelay);
        }
        flashlight.ToggleFlicker();
    }
}
