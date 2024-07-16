using System.Collections;
using UnityEditor;
using UnityEngine;
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
    [SerializeField] private GameObject graphicsButton;
    [SerializeField] private GameObject dot;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera secondaryCamera;

    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private AudioSource mainGameAudioSource;

    private void Start()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        mainGameAudioSource.UnPause();
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
        mainGameAudioSource.Stop();
        dot.SetActive(false);
        playerRespawn.Respawn();
        RoundManager.instance.currentState = GameState.onMainMenu;
    }

    public void ExitButton()
    {
        Application.Quit();
        EditorApplication.ExitPlaymode();
    }

    public void BackToGameButton()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        RoundManager.instance.currentState = GameState.pause;
    }
}
