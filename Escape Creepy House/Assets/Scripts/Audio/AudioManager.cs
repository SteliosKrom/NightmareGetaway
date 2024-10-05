using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("There is not instance of an audio manager");
        }
    }

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void Play(AudioSource source)
    {
        source.Play();
    }

    public void StopSound(AudioSource source)
    {
        source.Stop();
    }

    public void PauseSound(AudioSource source)
    {
        source.Pause();
    }

    public void UnPauseSound(AudioSource source)
    {
        source.UnPause();
    }
}
