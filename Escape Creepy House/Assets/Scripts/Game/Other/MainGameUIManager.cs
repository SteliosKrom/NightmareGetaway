using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{
    private bool active = true;
    private bool inactive = false;

    [Header("BUTTONS")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button exitButton;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject backToMenu;
    [SerializeField] private GameObject backToGame;
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
    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private PlayerRespawn playerRespawn;

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

        RoundManager.instance.currentState = GameState.playing;
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
        taskChange.SetActive(inactive);

        RoundManager.instance.currentState = GameState.onSettings;
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
        RoundManager.instance.currentState = GameState.onMainMenu;
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
        RoundManager.instance.currentState = GameState.pause;
    }
}
