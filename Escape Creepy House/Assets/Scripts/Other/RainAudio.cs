using UnityEngine;

public class RainAudio : MonoBehaviour
{
    public AudioSource rainAudioSource;

    private void Update()
    {
        if (RoundManager.instance.currentEnvironmentState == EnvironmentState.outdoors
            && RoundManager.instance.currentGameState != GameState.pause
            && RoundManager.instance.currentGameState != GameState.onSettingsGame)
        {
            rainAudioSource.spatialBlend = 0f;
        }
        else if (RoundManager.instance.currentEnvironmentState == EnvironmentState.indoors
            && RoundManager.instance.currentGameState != GameState.pause)
        {
            rainAudioSource.spatialBlend = 1f;
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
