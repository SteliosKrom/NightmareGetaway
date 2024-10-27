using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("GAME OBJECTS")]
    [SerializeField] private AudioSource[] objects1;
    [SerializeField] private AudioSource[] objects2;
    [SerializeField] private AudioSource[] objects3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("The instance of the object already exists");
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

    public void PauseSoundInPause()
    {
        foreach (AudioSource source in objects1)
        {
            source.Pause();
        }
    }

    public void UnpauseSoundInResumeGameFromSettings()
    {
        foreach (AudioSource source in objects2)
        {
            source.UnPause();
        }
    }

    public void UnpauseSoundInResumeGameFromPause()
    {
        foreach (AudioSource source in objects3)
        {
            source.UnPause();
        }
    }
}
