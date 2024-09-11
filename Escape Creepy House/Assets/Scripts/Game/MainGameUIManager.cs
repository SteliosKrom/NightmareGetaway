using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{

    [Header("CLASSES")]
    public PlayerRespawn playerRespawn;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button exitButton;

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
    [SerializeField] private GameObject taskText;
    [SerializeField] private GameObject taskFindTheRoomKeyText;
    [SerializeField] private GameObject taskDrinkWaterText;
    [SerializeField] private GameObject taskCheckForFoodText;
    [SerializeField] private GameObject taskFindMobilePhoneText;
    [SerializeField] private GameObject taskFindMainDoorKeyText;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera secondaryCamera;

    [SerializeField] private AudioSource mainMenuAudioSource;

    private void Start()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        dot.SetActive(true);
        RoundManager.instance.currentState = GameState.playing;
    }

    public void SettingsButton()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        audioButton.SetActive(true);
        videoButton.SetActive(true);
        graphicsButton.SetActive(true);
        controlsButton.SetActive(true);
        audioMenu.SetActive(false);
        videoMenu.SetActive(false);
        graphicsMenu.SetActive(false);
        backToMenu.SetActive(false);
        backToGame.SetActive(true);
        RoundManager.instance.currentState = GameState.onSettings;
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        mainCamera.enabled = false;
        secondaryCamera.enabled = true;
        mainMenuAudioSource.Play();
        dot.SetActive(false);
        playerRespawn.Respawn();
        taskText.SetActive(false);
        taskFindMainDoorKeyText.SetActive(false);
        taskFindMobilePhoneText.SetActive(false);
        taskCheckForFoodText.SetActive(false);
        taskDrinkWaterText.SetActive(false);
        taskFindTheRoomKeyText.SetActive(false);
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
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        RoundManager.instance.currentState = GameState.pause;
    }
}
