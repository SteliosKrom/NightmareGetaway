using DG.Tweening;
using UnityEngine;
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
    [SerializeField] private Button backToGameButton;

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
        Application.Quit();
    }

    public void BackToGameButton()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(active);
        settingsMenu.SetActive(inactive);
        taskChange.SetActive(inactive);

        backToGameButton.transform.DOScale(3.2f, 0.2f);
        RoundManager.instance.currentGameState = GameState.pause;
    }
}
