using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private bool active = true;
    private bool inactive = false;

    private float splashScreenDelay = 7f;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject theLegendKnightPanel;
    [SerializeField] private GameObject headsetPanel;
    [SerializeField] private GameObject seizurePanel;

    [Header("AUDIO")]
    public AudioSource secondSplashAudioSource;
    public AudioClip secondSplashAudioClip;

    private void Start()
    {
        StartCoroutine(ShowSplashScreens());
    }

    public IEnumerator ShowSplashScreens()
    {
        LoadIntroPanel();
        yield return new WaitForSeconds(splashScreenDelay);
        LoadSeizureWarningPanel();
        yield return new WaitForSeconds(splashScreenDelay);
        LoadHeadsetPanel();
        yield return new WaitForSeconds(splashScreenDelay);
        LoadTheLegendKnightPanel();
        yield return new WaitForSeconds(splashScreenDelay);
        LoadGame();
    }

    public void LoadIntroPanel()
    {
        introPanel.SetActive(active);
        theLegendKnightPanel.SetActive(inactive);
        headsetPanel.SetActive(inactive);
        seizurePanel.SetActive(inactive);
    }

    public void LoadTheLegendKnightPanel()
    {
        theLegendKnightPanel.SetActive(active);
        headsetPanel.SetActive(inactive);
        introPanel.SetActive(inactive);
        seizurePanel.SetActive(inactive);
        AudioManager.instance.PlaySound(secondSplashAudioSource, secondSplashAudioClip);
    }

    public void LoadHeadsetPanel()
    {
        headsetPanel.SetActive(active);
        introPanel.SetActive(inactive);
        theLegendKnightPanel.SetActive(inactive);
        seizurePanel.SetActive(inactive);
    }

    public void LoadSeizureWarningPanel()
    {
        introPanel.SetActive(inactive);
        headsetPanel.SetActive(inactive);
        theLegendKnightPanel.SetActive(inactive);
        seizurePanel.SetActive(active);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
