using UnityEngine;

public class ClockAudio : MonoBehaviour
{
    [Header("AUDIO")]
    public AudioSource clockAudioSource;
    public AudioClip clockAudioClip;
    private float clockTimer = 0f;
    private float clockInterval = 1f;
    public LayerMask groundLayer;


    private void Start()
    {
        clockAudioSource.Stop();
    }

    private void Update()
    {
        if (RoundManager.instance.currentGameState == GameState.playing)
        {
            if (!IsPlayerOnGround())
            {
                clockTimer += Time.deltaTime;

                if (clockTimer >= clockInterval)
                {
                    if (!clockAudioSource.isPlaying)
                    {
                        AudioManager.instance.PlaySound(clockAudioSource, clockAudioClip);
                    }
                    clockTimer -= clockInterval;
                }
            }
            else
            {
                if (clockAudioSource.isPlaying)
                {
                    clockAudioSource.Stop();
                }
                clockTimer = 0f;
            }
        }
    }

    private bool IsPlayerOnGround()
    {
        return Physics.CheckSphere(transform.position, 0.1f, groundLayer);
    }
}
