using UnityEngine;

public class FootstepsSystem : MonoBehaviour
{
    private bool active = true;
    private bool inactive = false;

    [Header("AUDIO SOURCES")]
    [SerializeField] private AudioSource footstepsAudioSource;

    [Header("FOOTSTEPS SETTINGS")]
    [SerializeField] private float holdThreshold = 0.1f;

    private float keyHoldTime = 0f;

    private void Update()
    {
        bool isHoldingKey = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A);

        if (RoundManager.instance.currentGameState == GameState.playing)
        {
            if (isHoldingKey)
            {
                keyHoldTime += Time.deltaTime;

                if (keyHoldTime >= holdThreshold)
                {
                    footstepsAudioSource.enabled = active;
                    footstepsAudioSource.pitch = Random.Range(0.5f, 1.5f);
                    footstepsAudioSource.volume = 0.025f;
                }
            }
            else
            {
                keyHoldTime = 0f;
                footstepsAudioSource.Stop();
                footstepsAudioSource.enabled = inactive;

            }
        }
        else
        {
            footstepsAudioSource.Stop();
            footstepsAudioSource.enabled = inactive;
        }
    }
}

