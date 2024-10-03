using System.Collections;
using UnityEngine;

public class FlashlightFlickering : MonoBehaviour
{
    private float flickerDuration = 5.0f;
    private float flickerDelay = 0.1f;

    [Header("SCRIPT REFERENCES")]
    [SerializeField] private Flashlight flashlight;

    [Header("AUDIO")]
    [SerializeField] private AudioSource flickeringAudioSrouce;
    [SerializeField] private AudioClip flickeringAudioClip;

    private void Start()
    {
        flashlight = GetComponent<Flashlight>();
    }

    public void TriggerFlicker()
    {
        StartCoroutine(FlickerLight());
        AudioManager.instance.PlaySound(flickeringAudioSrouce, flickeringAudioClip);
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
