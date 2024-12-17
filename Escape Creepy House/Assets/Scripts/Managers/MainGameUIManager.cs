using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{
    [Header("TYPES")]
    private bool active = true;
    private bool inactive = false;

    [Header("SCRIPT REFERENCES")]
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private KidsRoomLight kidsRoomLight;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SettingsManager settingsManager;

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
    [SerializeField] private GameObject notesPanel;
    [SerializeField] private GameObject notesButton;

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
        DeactivateGameObject.deactivateInstance.DeactivateObject(pauseMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(notesButton);
        ActivateGameObject.activateInstance.ActivateObject(dot);
        ActivateGameObject.activateInstance.ActivateObject(taskChange);
        AudioManager.instance.UnpauseSoundInResumeGameFromPause();

        resumeButton.transform.DOScale(0.8f, 0.2f);
        playerController.CheckDoorStateOnResume();
        Time.timeScale = 1.0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = inactive;
        RoundManager.instance.currentGameState = GameState.playing;
    }

    public void SettingsButton()
    {
        DeactivateGameObject.deactivateInstance.DeactivateObjectsInGameSettings();
        DeactivateGameObject.deactivateInstance.DeactivateObject(notesButton);
        ActivateGameObject.activateInstance.ActivateObjectsInGameSettings();
        settingsButton.transform.DOScale(0.8f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onSettingsGame;
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("MainGameScene");

        AudioManager.instance.Play(mainMenuAudioSource);
        ActivateGameObject.activateInstance.ActivateObject(mainMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObjectsInHome();
        DeactivateGameObject.deactivateInstance.DeactivateObject(notesButton);

        mainCamera.enabled = inactive;
        secondaryCamera.enabled = active;
        kidsRoomLight.enabled = inactive;
        playerRespawn.Respawn();

        Time.timeScale = 1f;
        RoundManager.instance.currentGameState = GameState.onMainMenu;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void NotesButton()
    {
        ActivateGameObject.activateInstance.ActivateObject(notesPanel);
        DeactivateGameObject.deactivateInstance.DeactivateObject(notesButton);
        notesButton.transform.DOScale(1f, 0.2f);
        RoundManager.instance.currentGameState = GameState.onSettingsGame;
    }

    public void BackToGameButton()
    {
        ActivateGameObject.activateInstance.ActivateObject(pauseMenu);
        ActivateGameObject.activateInstance.ActivateObject(notesButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(settings);
        DeactivateGameObject.deactivateInstance.DeactivateObject(taskChange);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(notesPanel);

        backToGameButton.transform.DOScale(3.2f, 0.2f);
        RoundManager.instance.currentGameState = GameState.pause;
    }
}
