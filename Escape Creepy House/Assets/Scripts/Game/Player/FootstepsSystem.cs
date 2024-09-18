using UnityEngine;

public class FootstepsSystem : MonoBehaviour
{
    [SerializeField] private AudioSource footstepsAudioSource;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (RoundManager.instance.currentState == GameState.playing)
            {
                footstepsAudioSource.enabled = true;
                footstepsAudioSource.pitch = Random.Range(0.5f, 1.5f);
                footstepsAudioSource.volume = 0.2f;
            }
        }
        else
        {
            footstepsAudioSource.enabled = false;
        }
    }
}
