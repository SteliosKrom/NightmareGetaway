using UnityEngine;

public class RainAudio : MonoBehaviour
{
    public AudioSource rainAudioSource;
    public AudioSource mainGameAudioSource;

    private void Update()
    {
        if (RoundManager.instance.currentEnvironmentState == EnvironmentState.outdoors && RoundManager.instance.currentGameState != GameState.pause)
        {
            if (!rainAudioSource.isPlaying)
            {
                rainAudioSource.UnPause();
                rainAudioSource.volume = 0.1f;
                mainGameAudioSource.Pause();
            }
        }
        else if (RoundManager.instance.currentEnvironmentState == EnvironmentState.indoors && RoundManager.instance.currentGameState != GameState.pause)
        {
            if (rainAudioSource.isPlaying)
            {
                rainAudioSource.Pause();
                mainGameAudioSource.UnPause();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Indoor"))
            {
                RoundManager.instance.currentEnvironmentState = EnvironmentState.indoors;
            }
            else if (gameObject.CompareTag("Outdoor"))
            {
                RoundManager.instance.currentEnvironmentState = EnvironmentState.outdoors;
            }
        }
    }
}
