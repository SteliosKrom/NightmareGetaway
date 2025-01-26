using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    private readonly float movementSpeed = 1f;
    private readonly float movementRange = 0.5f;
    private readonly float playButtonDelay = 2f; //8
    private readonly float gameIntroDelay = 1f;  //18
    private readonly float loadingTextDelay = 0.0010f;

    private bool active = true;
    private bool inactive = false;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI loadingText;

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
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject videoMenu;
    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject backToMenu;
    [SerializeField] private GameObject backToGame;
    [SerializeField] private GameObject backToPrevious;
    [SerializeField] private GameObject settings;

    [SerializeField] private GameObject audioButton;
    [SerializeField] private GameObject videoButton;
    [SerializeField] private GameObject graphicsButton;
    [SerializeField] private GameObject controlsButton;
    [SerializeField] private GameObject notesButton;

    [SerializeField] private GameObject audioTitle;
    [SerializeField] private GameObject displayTitle;
    [SerializeField] private GameObject graphicsTitle;
    [SerializeField] private GameObject controlsTitle;

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject gameIntroPanel;
    [SerializeField] private GameObject notesPanel;

    [SerializeField] private GameObject outdoorLight;
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject creature;
    [SerializeField] private GameObject dot;
    [SerializeField] private GameObject taskChange;


    [Header("OTHER")]
    [SerializeField] private Camera secondaryCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Light kidRoomLight;
    private Vector3 initialPos;

    [Header("AUDIO")]
    [SerializeField] private AudioSource hoverAudioSource;
    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private AudioSource rainAudioSource;
    [SerializeField] private AudioClip hoverAudioClip;

    private void Start()
    {
        Time.timeScale = 1f;
        ActivateGameObject.activateInstance.ActivateObject(mainMenu);
        ActivateGameObject.activateInstance.ActivateObject(creature);

        DeactivateGameObject.deactivateInstance.DeactivateObject(taskChange);
        DeactivateGameObject.deactivateInstance.DeactivateObject(notesPanel);
        DeactivateGameObject.deactivateInstance.DeactivateObject(notesButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(window);

        secondaryCamera.enabled = active;
        mainCamera.enabled = inactive;
        initialPos = secondaryCamera.transform.position;

        AudioManager.instance.Play(mainMenuAudioSource);
        AudioManager.instance.Play(rainAudioSource);
        RoundManager.instance.currentGameState = GameState.onMainMenu;
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
        ActivateGameObject.activateInstance.ActivateObject(dot);
        ActivateGameObject.activateInstance.ActivateObject(taskChange);
        DeactivateGameObject.deactivateInstance.DeactivateObjectsInEndGameIntro();

        Time.timeScale = 1f;
        secondaryCamera.enabled = inactive;
        mainCamera.enabled = active;
        playButton.transform.DOScale(1f, 0.2f);
        RoundManager.instance.currentGameState = GameState.playing;
    }

    public void PlayButton()
    {
        StartCoroutine(PlayButtonDelay());
    }

    public IEnumerator LoadingTextDelay()
    {
        loadingText.text = " 0%";
        float totalTime = 4f;
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / totalTime);
            loadingText.text = " " + Mathf.RoundToInt(progress * 100) + "%";
            yield return new WaitForSeconds(loadingTextDelay);
        }
        loadingText.text = " 100%";
    }

    public IEnumerator PlayButtonDelay()
    {
        ActivateGameObject.activateInstance.ActivateObject(loadingPanel);
        ActivateGameObject.activateInstance.ActivateObject(window);
        DeactivateGameObject.deactivateInstance.DeactivateObject(creature);
        AudioManager.instance.StopSound(mainMenuAudioSource);
        AudioManager.instance.PauseSound(rainAudioSource);

        StartCoroutine(LoadingTextDelay());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = inactive;
        outdoorLight.SetActive(inactive);

        yield return new WaitForSeconds(playButtonDelay);

        ActivateGameObject.activateInstance.ActivateObject(gameIntroPanel);
        DeactivateGameObject.deactivateInstance.DeactivateObject(loadingPanel);
        RoundManager.instance.currentGameState = GameState.inIntro;

        yield return new WaitForSeconds(gameIntroDelay);

        EndGameIntro();
        kidRoomLight.enabled = active;
        AudioManager.instance.UnPauseSound(rainAudioSource);
        rainAudioSource.spatialBlend = 1f;
    }

    public void ControlsButton()
    {
        ActivateGameObject.activateInstance.ActivateObject(controlsMenu);
        ActivateGameObject.activateInstance.ActivateObject(controlsTitle);
        DeactivateGameObject.deactivateInstance.DeactivateObjectsInControls();
        DeactivateGameObject.deactivateInstance.DeactivateObject(settingsMenu);
        controlsButton.transform.DOScale(4f, 0.2f);
    }

    public void SettingsButton()
    {
        ActivateGameObject.activateInstance.ActivateObjectsInSettings();
        DeactivateGameObject.deactivateInstance.DeactivateObjectsInSettings();
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
        ActivateGameObject.activateInstance.ActivateObject(mainMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObjectsInBackToMenu();
        backToMenuButtonSettings.transform.DOScale(3.2f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onMainMenu;
    }

    public void BackToMenuCredits()
    {
        ActivateGameObject.activateInstance.ActivateObject(mainMenu);

        DeactivateGameObject.deactivateInstance.DeactivateObject(settings);
        DeactivateGameObject.deactivateInstance.DeactivateObjectsInBackToMenu();

        backToMenuButtonCredits.transform.DOScale(3.2f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onMainMenu;
    }

    public void BackToPrevious()
    {
        ActivateGameObject.activateInstance.ActivateObjectsInBackToPrevious();

        DeactivateGameObject.deactivateInstance.DeactivateObjectsInBackToPrevious();
        DeactivateGameObject.deactivateInstance.DeactivateObject(audioTitle);
        DeactivateGameObject.deactivateInstance.DeactivateObject(displayTitle);
        DeactivateGameObject.deactivateInstance.DeactivateObject(graphicsTitle);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsTitle);

        backToPreviousButton.transform.DOScale(3.2f, 0.2f);
    }

    public void AudioCategoryButton()
    {
        ActivateGameObject.activateInstance.ActivateObject(audioMenu);
        ActivateGameObject.activateInstance.ActivateObject(audioTitle);

        DeactivateGameObject.deactivateInstance.DeactivateObjectsInCategoryButtons();
        DeactivateGameObject.deactivateInstance.DeactivateObject(settingsMenu);

        audioCategoryButton.transform.DOScale(4f, 0.2f);
    }

    public void VideoCategoryButton()
    {
        ActivateGameObject.activateInstance.ActivateObject(videoMenu);
        ActivateGameObject.activateInstance.ActivateObject(displayTitle);

        DeactivateGameObject.deactivateInstance.DeactivateObjectsInCategoryButtons();
        DeactivateGameObject.deactivateInstance.DeactivateObject(settingsMenu);

        videoCategoryButton.transform.DOScale(4f, 0.2f);
    }

    public void GraphicsCategoryButton()
    {
        ActivateGameObject.activateInstance.ActivateObject(graphicsMenu);
        ActivateGameObject.activateInstance.ActivateObject(graphicsTitle);

        DeactivateGameObject.deactivateInstance.DeactivateObjectsInCategoryButtons();
        DeactivateGameObject.deactivateInstance.DeactivateObject(settingsMenu);

        graphicsCategoryButton.transform.DOScale(4f, 0.2f);
    }
}
