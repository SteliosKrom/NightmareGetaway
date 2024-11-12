using UnityEngine;

public class CeilingFanAudio : MonoBehaviour
{
    private float ceilingFanTimer = 0f;
    private float ceilingFanInterval = 1f;

    [Header("AUDIO")]
    public AudioSource ceilingFanAudioSource;
    public AudioClip ceilingFanAudioClip;

    [Header("OTHER")]
    public LayerMask groundLayer;

    private void Start()
    {
        AudioManager.instance.StopSound(ceilingFanAudioSource);
    }

    private void Update()
    {
        if (RoundManager.instance.currentGameState == GameState.playing)
        {
            if (!IsPlayerOnGround())
            {
                ceilingFanTimer += Time.deltaTime;

                if (ceilingFanTimer >= ceilingFanInterval)
                {
                    if (!ceilingFanAudioSource.isPlaying)
                    {
                        AudioManager.instance.PlaySound(ceilingFanAudioSource, ceilingFanAudioClip);
                    }
                    ceilingFanTimer -= ceilingFanInterval;
                }
            }
            else
            {
                if (ceilingFanAudioSource.isPlaying)
                {
                    AudioManager.instance.StopSound(ceilingFanAudioSource);
                }
                ceilingFanTimer = 0f;
            }
        }
    }

    private bool IsPlayerOnGround()
    {
        return Physics.CheckSphere(transform.position, 0.1f, groundLayer);
    }
}
