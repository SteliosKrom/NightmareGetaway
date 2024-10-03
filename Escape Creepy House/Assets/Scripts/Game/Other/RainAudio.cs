using UnityEngine;

public class RainAudio : MonoBehaviour
{
    public AudioSource rainAudioSource;

    private void Update()
    {
        if (RoundManager.instance.currentEnvironmentState == EnvironmentState.outdoors)
        {
            if (!rainAudioSource.isPlaying)
            {
                rainAudioSource.UnPause();
                rainAudioSource.volume = 0.5f;
            }
        }
        else if (RoundManager.instance.currentEnvironmentState == EnvironmentState.indoors)
        {
            if (rainAudioSource.isPlaying)
            {
                rainAudioSource.Stop();
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
