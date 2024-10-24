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

        DeactivateGameObject.deactivateInstance.DeactivateObject(pauseMenu);

        ActivateGameObject.activateInstance.ActivateObject(dot);
        ActivateGameObject.activateInstance.ActivateObject(taskChange);

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
        // Make a method for this
        DeactivateGameObject.deactivateInstance.DeactivateObject(pauseMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(audioMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(videoMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(graphicsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(backToMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(taskChange);

        // Make a method for this
        ActivateGameObject.activateInstance.ActivateObject(audioButton);
        ActivateGameObject.activateInstance.ActivateObject(videoButton);
        ActivateGameObject.activateInstance.ActivateObject(graphicsButton);
        ActivateGameObject.activateInstance.ActivateObject(controlsButton);
        ActivateGameObject.activateInstance.ActivateObject(settings);
        ActivateGameObject.activateInstance.ActivateObject(settingsMenu);
        ActivateGameObject.activateInstance.ActivateObject(backToGame);
        ActivateGameObject.activateInstance.ActivateObject(backToPrevious);

        settingsButton.transform.DOScale(0.8f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onSettingsGame;
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;

        ActivateGameObject.activateInstance.ActivateObject(mainMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(dot);
        DeactivateGameObject.deactivateInstance.DeactivateObject(taskChange);
        DeactivateGameObject.deactivateInstance.DeactivateObject(settings);
        DeactivateGameObject.deactivateInstance.DeactivateObject(pauseMenu);

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
        ActivateGameObject.activateInstance.ActivateObject(pauseMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(settings);
        DeactivateGameObject.deactivateInstance.DeactivateObject(taskChange);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsMenu);

        backToGameButton.transform.DOScale(3.2f, 0.2f);
        RoundManager.instance.currentGameState = GameState.pause;
    }
}
