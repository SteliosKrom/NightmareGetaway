using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MainMenuUIManager : MonoBehaviour
{
    private readonly float movementSpeed = 1f;
    private readonly float movementRange = 0.5f;
    private readonly float playButtonDelay = 1f;
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
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button backToPreviousButton;
    [SerializeField] private Button bakcToGameButton;

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

    [Header("OTHER")]
    [SerializeField] private Camera secondaryCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private AudioSource hoverAudioSource;
    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private AudioSource mainGameAudioSource;
    [SerializeField] private AudioSource rainAudioSource;
    [SerializeField] private AudioClip rainAudioClip;
    [SerializeField] private AudioClip hoverAudioClip;
    private Vector3 initialPos;

    private void Start()
    {
        Time.timeScale = 1f;

        mainMenu.SetActive(active);
        controlsMenu.SetActive(inactive);
        settings.SetActive(inactive);
        audioMenu.SetActive(inactive);
        videoMenu.SetActive(inactive);
        graphicsMenu.SetActive(inactive);
        creditsMenu.SetActive(inactive);

        secondaryCamera.enabled = active;
        mainCamera.enabled = inactive;

        dot.SetActive(inactive);
        taskChange.SetActive(inactive);
        loadingPanel.SetActive(inactive);

        initialPos = secondaryCamera.transform.position;
        mainMenuAudioSource.Play();
        mainGameAudioSource.Stop();
        rainAudioSource.Play();

        AttachButtonHoverEventsMenu(playButton);
        AttachButtonHoverEventsMenu(settingsButton);
        AttachButtonHoverEventsMenu(creditsButton);
        AttachButtonHoverEventsMenu(exitButton);

        AttachButtonHoverEventsSettings(audioCategoryButton);
        AttachButtonHoverEventsSettings(videoCategoryButton);
        AttachButtonHoverEventsSettings(graphicsCategoryButton);
        AttachButtonHoverEventsSettings(controlsCategoryButon);
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
        loadingPanel.SetActive(active);
        rainAudioSource.Pause();
        mainMenuAudioSource.Stop();
        yield return new WaitForSecondsRealtime(playButtonDelay);
        mainGameAudioSource.Play();
        Time.timeScale = 1f;

        mainMenu.SetActive(inactive);
        creditsMenu.SetActive(inactive);
        dot.SetActive(active);
        taskChange.SetActive(active);
        loadingPanel.SetActive(inactive);

        secondaryCamera.enabled = inactive;
        mainCamera.enabled = active;

        playButton.transform.DOScale(1f, 0.2f);
        RoundManager.instance.currentGameState = GameState.playing;
    }

    public void ControlsButton()
    {
        audioButton.SetActive(inactive);
        controlsButton.SetActive(inactive);
        graphicsButton.SetActive(inactive);
        videoButton.SetActive(inactive);

        creditsMenu.SetActive(inactive);
        controlsMenu.SetActive(active);
        mainMenu.SetActive(inactive);
        settingsMenu.SetActive(active);

        controlsButton.transform.DOScale(4f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onMainMenu;
    }

    public void SettingsButton()
    {
        settings.SetActive(active);

        audioButton.SetActive(active);
        videoButton.SetActive(active);
        graphicsButton.SetActive(active);
        controlsButton.SetActive(active);

        audioMenu.SetActive(inactive);
        videoMenu.SetActive(inactive);
        graphicsMenu.SetActive(inactive);
        mainMenu.SetActive(inactive);
        creditsMenu.SetActive(inactive);

        backToGame.SetActive(inactive);
        backToMenu.SetActive(active);
        backToPrevious.SetActive(active);

        taskChange.SetActive(inactive);

        settingsButton.transform.DOScale(1f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onSettings;
    }

    public void CreditsButton()
    {
        mainMenu.SetActive(inactive);
        creditsMenu.SetActive(active);
        taskChange.SetActive(inactive);
        creditsButton.transform.DOScale(1f, 0.2f);
    }

    public void ExitButton()
    {
        exitButton.transform.DOScale(1f, 0.2f);
        Application.Quit();
    }

    public void BackToMenu()
    {
        controlsMenu.SetActive(inactive);
        settings.SetActive(inactive);
        mainMenu.SetActive(active);
        audioButton.SetActive(inactive);
        videoButton.SetActive(inactive);
        graphicsButton.SetActive(inactive);
        controlsButton.SetActive(inactive);
        creditsMenu.SetActive(inactive);

        taskChange.SetActive(inactive);

        RoundManager.instance.currentGameState = GameState.onMainMenu;
    }

    public void BackToPrevious()
    {
        audioButton.SetActive(active);
        videoButton.SetActive(active);
        graphicsButton.SetActive(active);
        controlsButton.SetActive(active);

        mainMenu.SetActive(inactive) ;
        audioMenu.SetActive(inactive);
        videoMenu.SetActive(inactive);
        graphicsMenu.SetActive(inactive);
        controlsMenu.SetActive(inactive);
        settingsMenu.SetActive(active);

        RoundManager.instance.currentGameState = GameState.onMainMenu;
    }

    public void AudioCategoryButton()
    {
        audioMenu.SetActive(active);

        audioButton.SetActive(inactive);
        videoButton.SetActive(inactive);
        graphicsButton.SetActive(inactive);
        controlsButton.SetActive(inactive);
        audioCategoryButton.transform.DOScale(4f, 0.2f);
    }

    public void VideoCategoryButton()
    {
        videoMenu.SetActive(active);
        videoButton.SetActive(inactive);
        graphicsButton.SetActive(inactive);
        audioButton.SetActive(inactive);
        controlsButton.SetActive(inactive);
        videoCategoryButton.transform.DOScale(4f, 0.2f);
    }

    public void GraphicsCategoryButton()
    {
        graphicsMenu.SetActive(active);
        graphicsButton.SetActive(inactive);
        videoButton.SetActive(inactive);
        audioButton.SetActive(inactive);
        controlsButton.SetActive(inactive);
        graphicsCategoryButton.transform.DOScale(4f, 0.2f);
    }

    public void EnterHoverSoundEffect()
    {
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }

    public void EnterHoverSoundEffectMenu(Transform buttonTransform)
    {
        buttonTransform.DOScale(1.2f, 0.2f);
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }

    public void ExitHoverSoundEffectMenu(Transform buttonTransform)
    {
        buttonTransform.DOScale(1f, 0.2f);
    }

    public void EnterHoverSoundEffectSettings(Transform buttonTransform)
    {
        buttonTransform.DOScale(4.5f, 0.2f);
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }

    public void ExitHoverSoundEffectSettings(Transform buttonTransform)
    {
        buttonTransform.DOScale(4f, 0.2f);
    }

    private void AttachButtonHoverEventsMenu(Button menuButtons)
    {
        EventTrigger menuTrigger = menuButtons.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry menuEntryEnter = new EventTrigger.Entry();
        menuEntryEnter.eventID = EventTriggerType.PointerEnter;
        menuEntryEnter.callback.AddListener((data) => { EnterHoverSoundEffectMenu(menuButtons.transform); });
        menuTrigger.triggers.Add(menuEntryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { ExitHoverSoundEffectMenu(menuButtons.transform); });
        menuTrigger.triggers.Add(entryExit);
    }

    private void AttachButtonHoverEventsSettings(Button settingsButtons)
    {
        EventTrigger settingsTrigger = settingsButtons.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry settingsEntryEnter = new EventTrigger.Entry();
        settingsEntryEnter.eventID = EventTriggerType.PointerEnter;
        settingsEntryEnter.callback.AddListener((data) => { EnterHoverSoundEffectSettings(settingsButtons.transform); });
        settingsTrigger.triggers.Add(settingsEntryEnter);

        EventTrigger.Entry settingsEntryExit = new EventTrigger.Entry();
        settingsEntryExit.eventID = EventTriggerType.PointerExit;
        settingsEntryExit.callback.AddListener((data) => { ExitHoverSoundEffectSettings(settingsButtons.transform); });
        settingsTrigger.triggers.Add(settingsEntryExit);
    }
}
