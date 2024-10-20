using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{
    private bool active = true;
    private bool inactive = false;

    [Header("SCRIPT REFERENCES")]
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private KidsRoomLight kidsRoomLight;

    [Header("BUTTONS")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button audioCategoryButton;
    [SerializeField] private Button videoCategoryButton;
    [SerializeField] private Button graphicsCategoryButton;
    [SerializeField] private Button controlsCategoryButon;
    [SerializeField] private Button backToPreviousButton;
    [SerializeField] private Button backToGameButton;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject backToMenu;
    [SerializeField] private GameObject backToGame;
    [SerializeField] private GameObject backToPrevious;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject videoMenu;
    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject audioButton;
    [SerializeField] private GameObject videoButton;
    [SerializeField] private GameObject controlsButton;
    [SerializeField] private GameObject graphicsButton;
    [SerializeField] private GameObject dot;
    [SerializeField] private GameObject taskChange;

    [Header("OTHER")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera secondaryCamera;

    [Header("AUDIO")]
    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private AudioSource mainGameAudioSource;
    [SerializeField] private AudioSource lockedAudioSource;
    [SerializeField] private AudioSource keysAudioSource;
    [SerializeField] private AudioSource drinkAudioSource;
    [SerializeField] private AudioSource eatAudioSource;
    [SerializeField] private AudioSource clockAudioSource;
    [SerializeField] private AudioSource hoverAudioSource;
    [SerializeField] private AudioClip hoverAudioClip;

    public void ResumeButton()
    {
        Time.timeScale = 1.0f;

        DeactivateGameObject.instance.DeactivateObject(pauseMenu);

        ActivateGameObject.instance.ActivateObject(dot);
        ActivateGameObject.instance.ActivateObject(taskChange);

        AudioManager.instance.UnPauseSound(lockedAudioSource);
        AudioManager.instance.UnPauseSound(clockAudioSource);
        AudioManager.instance.UnPauseSound(mainGameAudioSource);
        AudioManager.instance.UnPauseSound(keysAudioSource);
        AudioManager.instance.UnPauseSound(eatAudioSource);
        AudioManager.instance.UnPauseSound(drinkAudioSource);

        resumeButton.transform.DOScale(0.8f, 0.2f);
        RoundManager.instance.currentGameState = GameState.playing;
    }

    public void SettingsButton()
    {
        DeactivateGameObject.instance.DeactivateObject(pauseMenu);
        DeactivateGameObject.instance.DeactivateObject(audioMenu);
        DeactivateGameObject.instance.DeactivateObject(videoMenu);
        DeactivateGameObject.instance.DeactivateObject(graphicsMenu);
        DeactivateGameObject.instance.DeactivateObject(backToMenu);
        DeactivateGameObject.instance.DeactivateObject(taskChange);

        ActivateGameObject.instance.ActivateObject(audioButton);
        ActivateGameObject.instance.ActivateObject(videoButton);
        ActivateGameObject.instance.ActivateObject(graphicsButton);
        ActivateGameObject.instance.ActivateObject(controlsButton);
        ActivateGameObject.instance.ActivateObject(settings);
        ActivateGameObject.instance.ActivateObject(settingsMenu);
        ActivateGameObject.instance.ActivateObject(backToGame);
        ActivateGameObject.instance.ActivateObject(backToPrevious);

        settingsButton.transform.DOScale(0.8f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onSettingsGame;
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;

        ActivateGameObject.instance.ActivateObject(mainMenu);

        DeactivateGameObject.instance.DeactivateObject(dot);
        DeactivateGameObject.instance.DeactivateObject(taskChange);
        DeactivateGameObject.instance.DeactivateObject(settings);
        DeactivateGameObject.instance.DeactivateObject(pauseMenu);

        AudioManager.instance.Play(mainMenuAudioSource);
        mainCamera.enabled = inactive;
        secondaryCamera.enabled = active;
        playerRespawn.Respawn();

        SceneManager.LoadScene("MainGameScene");
        kidsRoomLight.enabled = inactive;
        RoundManager.instance.currentGameState = GameState.onMainMenu;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackToGameButton()
    {
        ActivateGameObject.instance.ActivateObject(pauseMenu);

        DeactivateGameObject.instance.DeactivateObject(settings);
        DeactivateGameObject.instance.DeactivateObject(taskChange);
        DeactivateGameObject.instance.DeactivateObject(controlsMenu);

        backToGameButton.transform.DOScale(3.2f, 0.2f);
        RoundManager.instance.currentGameState = GameState.pause;
    }
}
