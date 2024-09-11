using System.Collections;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    private readonly float movementSpeed = 1f;
    private readonly float movementRange = 0.5f;
    private readonly float playButtonDelay = 6f;

    [Header("BUTTONS")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button audioCategoryButton;
    [SerializeField] private Button videoCategoryButton;
    [SerializeField] private Button graphicsCategoryButton;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject audioButton;
    [SerializeField] private GameObject videoButton;
    [SerializeField] private GameObject graphicsButton;
    [SerializeField] private GameObject controlsButton;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject videoMenu;
    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject backToMenu;
    [SerializeField] private GameObject backToGame;
    [SerializeField] private GameObject dot;
    [SerializeField] private GameObject taskText;
    [SerializeField] private GameObject taskChange;
    [SerializeField] private GameObject loadingPanel;

    [Header("OTHER")]
    [SerializeField] private Camera secondaryCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private AudioSource hoverAudioSource;
    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private AudioClip hoverAudioClip;
    private Vector3 initialPos;

    private void Start()
    {
        Time.timeScale = 1f;
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
        settings.SetActive(false);
        audioMenu.SetActive(false);
        videoMenu.SetActive(false);
        graphicsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        secondaryCamera.enabled = true;
        mainCamera.enabled = false;
        mainMenuAudioSource.Play();
        dot.SetActive(false);
        taskText.SetActive(false);
        loadingPanel.SetActive(false);
        initialPos = secondaryCamera.transform.position;
    }

    private void Update()
    {
        if (secondaryCamera.enabled)
        {
            float offsetX = Mathf.Sin(Time.time * movementSpeed) * movementRange;
            float offsetY = Mathf.Sin(Time.time * movementSpeed) * movementRange;
            float offsetZ = Mathf.Sin(Time.time * movementSpeed) * movementRange;
            secondaryCamera.transform.position = initialPos + new Vector3(-offsetX, -offsetY, offsetZ);
        }
    }

    public void PlayButton()
    {
        StartCoroutine(PlayButtonDelay());
    }

    IEnumerator PlayButtonDelay()
    {
        loadingPanel.SetActive(true);
        mainMenuAudioSource.Stop();
        yield return new WaitForSecondsRealtime(playButtonDelay);
        Time.timeScale = 1f;
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
        secondaryCamera.enabled = false;
        mainCamera.enabled = true;
        mainMenuAudioSource.Stop();
        dot.SetActive(true);
        taskText.SetActive(true);
        loadingPanel.SetActive(false);
        RoundManager.instance.currentState = GameState.playing;
    }

    public void ControlsButton()
    {
        audioButton.SetActive(false);
        controlsButton.SetActive(false);
        graphicsButton.SetActive(false);
        videoButton.SetActive(false);

        creditsMenu.SetActive(false);
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

        RoundManager.instance.currentState = GameState.onMainMenu;
    }

    public void SettingsButton()
    {
        settings.SetActive(true);

        audioButton.SetActive(true);
        videoButton.SetActive(true);
        graphicsButton.SetActive(true);
        controlsButton.SetActive(true);

        audioMenu.SetActive(false);
        videoMenu.SetActive(false);
        graphicsMenu.SetActive(false);
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);

        backToGame.SetActive(false);
        backToMenu.SetActive(true);

        taskText.SetActive(false);

        RoundManager.instance.currentState = GameState.onSettings;
    }

    public void CreditsButton()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);

        taskText.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        controlsMenu.SetActive(false);
        settings.SetActive(false);
        mainMenu.SetActive(true);

        audioButton.SetActive(false);
        videoButton.SetActive(false);
        graphicsButton.SetActive(false);
        controlsButton.SetActive(false);
        creditsMenu.SetActive(false);

        taskText.SetActive(false);

        RoundManager.instance.currentState = GameState.onMainMenu;
    }

    public void BackToPrevious()
    {
        audioButton.SetActive(true);
        videoButton.SetActive(true);
        graphicsButton.SetActive(true);
        controlsButton.SetActive(true);

        mainMenu.SetActive(false) ;
        audioMenu.SetActive(false);
        videoMenu.SetActive(false);
        graphicsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(true);

        RoundManager.instance.currentState = GameState.onMainMenu;
    }

    public void AudioCategoryButton()
    {
        audioMenu.SetActive(true);

        audioButton.SetActive(false);
        videoButton.SetActive(false);
        graphicsButton.SetActive(false);
        controlsButton.SetActive(false);
    }

    public void VideoCategoryButton()
    {
        videoMenu.SetActive(true);
        videoButton.SetActive(false);
        graphicsButton.SetActive(false);
        audioButton.SetActive(false);
        controlsButton.SetActive(false);
    }

    public void GraphicsCategoryButton()
    {
        graphicsMenu.SetActive(true);
        graphicsButton.SetActive(false);
        videoButton.SetActive(false);
        audioButton.SetActive(false);
        controlsButton.SetActive(false);
    }

    public void HoverSoundEffect()
    {
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }
}
