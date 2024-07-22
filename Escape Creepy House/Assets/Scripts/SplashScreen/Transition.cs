using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [Header("CLASSES")]
    public GameObject logoText;

    public AudioSource secondSplashAudioSource;
    public AudioClip secondSplashAudioClip;

    private void Awake()
    {
        logoText.SetActive(false);
    }

    private void Start()
    {
        Invoke("LoadText", 6f);
        Invoke("LoadMainGameScene", 12f);

    }

    public void LoadMainGameScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void LoadText()
    {
        logoText.SetActive(true);
        AudioManager.instance.PlaySound(secondSplashAudioSource, secondSplashAudioClip);
    }
}
