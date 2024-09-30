using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{
    public Interactor interactor;
    public ClockAudio clockAudio;
    private bool active = true;
    private bool inactive = false;

    [Header("BUTTONS")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button audioCategoryButton;
    [SerializeField] private Button videoCategoryButton;
    [SerializeField] private Button graphicsCategoryButton;
    [SerializeField] private Button controlsCategoryButon;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button backToPreviousButton;
    [SerializeField] private Button backToGameButon;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject backToMenu;
    [SerializeField] private GameObject backToGame;
    [SerializeField] private GameObject backToPrevious;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject videoMenu;
    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject audioButton;
    [SerializeField] private GameObject videoButton;
    [SerializeField] private GameObject controlsButton;
    [SerializeField] private GameObject graphicsButton;
    [SerializeField] private GameObject dot;
    [SerializeField] private GameObject taskChange;

    [Header("OTHER")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera secondaryCamera;
    [SerializeField] private PlayerRespawn playerRespawn;

    [Header("AUDIO")]
    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private AudioSource mainGameAudioSource;
    [SerializeField] private AudioSource hoverAudioSource;
    [SerializeField] private AudioClip hoverAudioClip;


    private void Start()
    {
        settingsMenu.SetActive(inactive);
        pauseMenu.SetActive(inactive);

        AttachButtonHoverEventsPause(resumeButton);
        AttachButtonHoverEventsPause(settingsButton);
        AttachButtonHoverEventsPause(homeButton);
        AttachButtonHoverEventsPause(exitButton);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(inactive);
        dot.SetActive(active);
        taskChange.SetActive(active);
        interactor.lockedAudioSource.UnPause();
        clockAudio.clockAudioSource.UnPause();
        mainGameAudioSource.UnPause();
        resumeButton.transform.DOScale(0.8f, 0.2f);
        RoundManager.instance.currentGameState = GameState.playing;
    }

    public void SettingsButton()
    {
        pauseMenu.SetActive(inactive);
        settingsMenu.SetActive(active);
        audioMenu.SetActive(inactive);
        videoMenu.SetActive(inactive);
        graphicsMenu.SetActive(inactive);
        backToMenu.SetActive(inactive);

        audioButton.SetActive(active);
        videoButton.SetActive(active);
        graphicsButton.SetActive(active);
        controlsButton.SetActive(active);

        backToGame.SetActive(active);
        backToPrevious.SetActive(active);
        taskChange.SetActive(inactive);

        settingsButton.transform.DOScale(0.8f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onSettings;
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(inactive);
        mainMenu.SetActive(active);
        settingsMenu.SetActive(inactive);
        mainCamera.enabled = inactive;
        secondaryCamera.enabled = active;
        mainMenuAudioSource.Play();
        dot.SetActive(inactive);
        playerRespawn.Respawn();
        taskChange.SetActive(inactive);
        SceneManager.LoadScene("MainGameScene");
        RoundManager.instance.currentGameState = GameState.onMainMenu;
    }

    public void ExitButton()
    {
        exitButton.transform.DOScale(0.8f, 0.2f);
        Application.Quit();
    }

    public void BackToGameButton()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(active);
        settingsMenu.SetActive(inactive);
        taskChange.SetActive(inactive);
        RoundManager.instance.currentGameState = GameState.pause;
    }

    public void EnterHoverSoundEffect()
    {
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }

    public void EnterHoverSoundEffectPause(Transform buttonTransform)
    {
        buttonTransform.DOScale(1f, 0.2f);
        Time.timeScale = 1f;
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }

    public void ExitHoverSoundEffectPause(Transform buttonTransform)
    {
        buttonTransform.DOScale(0.8f, 0.2f);
        Time.timeScale = 1f;
    }

    public void EnterHoverSoundEffectSettings(Transform buttonTransform)
    {
        buttonTransform.DOScale(4.5f, 0.2f);
        Time.timeScale = 1f;
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }

    public void ExitHoverSoundEffectSettings(Transform buttonTransform)
    {
        buttonTransform.DOScale(4f, 0.2f);
        Time.timeScale = 1f;
    }

    private void AttachButtonHoverEventsPause(Button pauseButtons)
    {
        EventTrigger pauseTrigger = pauseButtons.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pauseEntryEnter = new EventTrigger.Entry();
        pauseEntryEnter.eventID = EventTriggerType.PointerEnter;
        pauseEntryEnter.callback.AddListener((data) => { EnterHoverSoundEffectPause(pauseButtons.transform); });
        pauseTrigger.triggers.Add(pauseEntryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { ExitHoverSoundEffectPause(pauseButtons.transform); });
        pauseTrigger.triggers.Add(entryExit);
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
