using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private bool active = true;
    private bool inactive = false;

    [Header("GAME OBJECTS")]
    public GameObject introPanel;
    public GameObject theLegendKnightPanel;
    public GameObject headsetPanel;

    [Header("AUDIO")]
    public AudioSource secondSplashAudioSource;
    public AudioClip secondSplashAudioClip;

    private void Start()
    {
        LoadIntroPanel();
        Invoke("LoadIntroPanel", 7f);
        Invoke("LoadTheLegendKnightPanel", 7f);
        Invoke("LoadHeadsetPanel", 14f);
        Invoke("LoadMainGameScene", 21f);
    }

    public void LoadMainGameScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void LoadIntroPanel()
    {
        introPanel.SetActive(active);
        theLegendKnightPanel.SetActive(inactive);
        headsetPanel.SetActive(inactive);
    }

    public void LoadTheLegendKnightPanel()
    {
        theLegendKnightPanel.SetActive(active);
        headsetPanel.SetActive(inactive);
        introPanel.SetActive(inactive);
        AudioManager.instance.PlaySound(secondSplashAudioSource, secondSplashAudioClip);
    }

    public void LoadHeadsetPanel()
    {
        headsetPanel.SetActive(active);
        introPanel.SetActive(inactive);
        theLegendKnightPanel.SetActive(inactive);
    }
}
