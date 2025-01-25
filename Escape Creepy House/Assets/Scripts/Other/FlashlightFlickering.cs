using System.Collections;
using UnityEngine;

public class FlashlightFlickering : MonoBehaviour
{
    [Header("TYPES")]
    private float flickerDuration = 5.0f;
    private float flickerDelay = 0.1f;

    [Header("SCRIPT REFERENCES")]
    [SerializeField] private Flashlight flashlight;
    [SerializeField] private Interactor interactor;

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
    }

    public IEnumerator FlickerLight()
    {
        float timePassed = 0;

        while (timePassed <= flickerDuration)
        {
            flashlight.ToggleFlicker();
            timePassed += flickerDelay;
            yield return new WaitForSeconds(flickerDelay);
        }
        flashlight.ToggleFlicker();
        interactor.canToggle = true;
    }
}
