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
}
