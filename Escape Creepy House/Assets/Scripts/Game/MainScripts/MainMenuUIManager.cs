using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class MainMenuUIManager : MonoBehaviour
{
    private readonly float movementSpeed = 1f;
    private readonly float movementRange = 0.5f;
    private readonly float playButtonDelay = 8f; 
    private readonly float gameIntroDelay = 18f; 

    private bool active = true;
    private bool inactive = false;

    [Header("BUTTONS")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button audioCategoryButton;
    [SerializeField] private Button videoCategoryButton;
    [SerializeField] private Button graphicsCategoryButton;
    [SerializeField] private Button controlsCategoryButon;
    [SerializeField] private Button backToMenuButtonSettings;
    [SerializeField] private Button backToPreviousButton;
    [SerializeField] private Button backToGameButton;
    [SerializeField] private Button backToMenuButtonCredits;

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
    [SerializeField] private GameObject backToPrevious;
    [SerializeField] private GameObject dot;
    [SerializeField] private GameObject taskChange;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject gameIntroPanel;

    [Header("OTHER")]
    [SerializeField] private Camera secondaryCamera;
    [SerializeField] private Camera mainCamera;
    private Vector3 initialPos;

    [Header("AUDIO")]
    [SerializeField] private AudioSource hoverAudioSource;
    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private AudioSource mainGameAudioSource;
    [SerializeField] private AudioSource rainAudioSource;
    [SerializeField] private AudioClip hoverAudioClip;


    private void Start()
    {
        Time.timeScale = 1f;

        ActivateGameObject.activateInstance.ActivateObject(mainMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(taskChange);

        secondaryCamera.enabled = active;
        mainCamera.enabled = inactive;
        initialPos = secondaryCamera.transform.position;

        AudioManager.instance.Play(mainMenuAudioSource);
        AudioManager.instance.StopSound(mainGameAudioSource);
        AudioManager.instance.Play(rainAudioSource);
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

    public void EndGameIntro()
    {
        DeactivateGameObject.deactivateInstance.DeactivateObject(gameIntroPanel);
        DeactivateGameObject.deactivateInstance.DeactivateObject(loadingPanel);
        DeactivateGameObject.deactivateInstance.DeactivateObject(mainMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(creditsMenu);

        ActivateGameObject.activateInstance.ActivateObject(dot);
        ActivateGameObject.activateInstance.ActivateObject(taskChange);

        Time.timeScale = 1f;
        secondaryCamera.enabled = inactive;
        mainCamera.enabled = active;
        playButton.transform.DOScale(1f, 0.2f);
        AudioManager.instance.Play(mainGameAudioSource);
        RoundManager.instance.currentGameState = GameState.playing;
    }

    public void PlayButton()
    {
        StartCoroutine(PlayButtonDelay());
    }

    public IEnumerator PlayButtonDelay()
    {
        ActivateGameObject.activateInstance.ActivateObject(loadingPanel);
        AudioManager.instance.PauseSound(rainAudioSource);
        AudioManager.instance.StopSound(mainMenuAudioSource);

        yield return new WaitForSeconds(playButtonDelay);

        ActivateGameObject.activateInstance.ActivateObject(gameIntroPanel);
        DeactivateGameObject.deactivateInstance.DeactivateObject(loadingPanel);
        RoundManager.instance.currentGameState = GameState.inIntro;

        yield return new WaitForSeconds(gameIntroDelay);

        EndGameIntro();
    }

    public void ControlsButton()
    {
        // Make a method for this
        DeactivateGameObject.deactivateInstance.DeactivateObject(audioButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(graphicsButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(videoButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(creditsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(mainMenu);

        ActivateGameObject.activateInstance.ActivateObject(controlsMenu);
        ActivateGameObject.activateInstance.ActivateObject(settingsMenu);

        controlsButton.transform.DOScale(4f, 0.2f);
    }

    public void SettingsButton()
    {
        // Make a method for this
        ActivateGameObject.activateInstance.ActivateObject(settings);
        ActivateGameObject.activateInstance.ActivateObject(settingsMenu);
        ActivateGameObject.activateInstance.ActivateObject(settings);
        ActivateGameObject.activateInstance.ActivateObject(audioButton);
        ActivateGameObject.activateInstance.ActivateObject(videoButton);
        ActivateGameObject.activateInstance.ActivateObject(graphicsButton);
        ActivateGameObject.activateInstance.ActivateObject(controlsButton);
        ActivateGameObject.activateInstance.ActivateObject(backToMenu);
        ActivateGameObject.activateInstance.ActivateObject(backToPrevious);

        // Make a method for this
        DeactivateGameObject.deactivateInstance.DeactivateObject(audioMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(videoMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(graphicsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(mainMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(creditsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(backToGame);
        DeactivateGameObject.deactivateInstance.DeactivateObject(taskChange);

        settingsButton.transform.DOScale(1f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onSettingsMenu;
    }

    public void CreditsButton()
    {
        DeactivateGameObject.deactivateInstance.DeactivateObject(taskChange);
        DeactivateGameObject.deactivateInstance.DeactivateObject(mainMenu);
        creditsMenu.SetActive(active);
        creditsButton.transform.DOScale(1f, 0.2f);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackToMenuSettings()
    {
        // Make a method for this
        ActivateGameObject.activateInstance.ActivateObject(mainMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(settings);
        DeactivateGameObject.deactivateInstance.DeactivateObject(audioButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(videoButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(graphicsButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(creditsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(taskChange);

        backToMenuButtonSettings.transform.DOScale(3.2f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onMainMenu;
    }

    public void BackToMenuCredits()
    {
        // Make a method for this
        ActivateGameObject.activateInstance.ActivateObject(mainMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(settings);
        DeactivateGameObject.deactivateInstance.DeactivateObject(audioButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(videoButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(graphicsButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(creditsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(taskChange);

        backToMenuButtonCredits.transform.DOScale(3.2f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onMainMenu;
    }

    public void BackToPrevious()
    {
        // Make a method for this
        DeactivateGameObject.deactivateInstance.DeactivateObject(mainMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(audioMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(videoMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(graphicsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsMenu);

        // Make a method for this
        ActivateGameObject.activateInstance.ActivateObject(audioButton);
        ActivateGameObject.activateInstance.ActivateObject(videoButton);
        ActivateGameObject.activateInstance.ActivateObject(graphicsButton);
        ActivateGameObject.activateInstance.ActivateObject(controlsButton);
        ActivateGameObject.activateInstance.ActivateObject(settingsMenu);

        backToPreviousButton.transform.DOScale(3.2f, 0.2f);
    }

    public void AudioCategoryButton()
    {
        ActivateGameObject.activateInstance.ActivateObject(audioMenu);

        // Make a method for this
        DeactivateGameObject.deactivateInstance.DeactivateObject(audioButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(videoButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(graphicsButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsButton);

        audioCategoryButton.transform.DOScale(4f, 0.2f);
    }

    public void VideoCategoryButton()
    {
        ActivateGameObject.activateInstance.ActivateObject(videoMenu);

        // Make a method for this
        DeactivateGameObject.deactivateInstance.DeactivateObject(audioButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(videoButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(graphicsButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsButton);

        videoCategoryButton.transform.DOScale(4f, 0.2f);
    }

    public void GraphicsCategoryButton()
    {
        ActivateGameObject.activateInstance.ActivateObject(graphicsMenu);

        // Make a method for this
        DeactivateGameObject.deactivateInstance.DeactivateObject(audioButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(videoButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(graphicsButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsButton);

        graphicsCategoryButton.transform.DOScale(4f, 0.2f);
    }
}
