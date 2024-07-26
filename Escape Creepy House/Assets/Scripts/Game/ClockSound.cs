using UnityEngine;

public class ClockSound : MonoBehaviour
{
    private float time = 0f;
    private float clockInterval = 1f;
    private float clockTimer = 0f;

    [SerializeField] private AudioSource clockAudioSource;
    [SerializeField] private AudioClip clockAudioClip;

    private void Update()
    {
        UpdateClockSound();
        clockAudioSource.clip = clockAudioClip;
    }

    public void UpdateClockSound()
    {
        if (RoundManager.instance.currentState == GameState.playing)
        {
            time += Time.deltaTime;
            clockTimer += Time.deltaTime;

            if (clockTimer >= clockInterval)
            {
                PlayClockSound();
                clockTimer = 0f;
            }
        }
        else
        {
            clockAudioSource.Stop();
        }
    }

    private void PlayClockSound()
    {
        if (!clockAudioSource.isPlaying)
        {
            clockAudioSource.Play();
        }
    }
}
