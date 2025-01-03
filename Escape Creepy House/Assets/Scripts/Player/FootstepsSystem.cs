using System.Collections;
using UnityEngine;

public class FootstepsSystem : MonoBehaviour
{
    private bool active = true;
    private bool inactive = false;

    [Header("AUDIO SOURCES")]
    [SerializeField] private AudioSource footstepsAudioSource;


    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (RoundManager.instance.currentGameState == GameState.playing)
            {
                footstepsAudioSource.enabled = active;
                footstepsAudioSource.pitch = Random.Range(0.5f, 1.5f);
                footstepsAudioSource.volume = 0.2f;
            }
            else
            {
                footstepsAudioSource.enabled = inactive;
            }
        }
        else
        {
            footstepsAudioSource.enabled = inactive;
        }
    }
}
