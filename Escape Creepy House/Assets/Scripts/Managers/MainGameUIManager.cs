using DG.Tweening;
using TMPro;
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
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private TaskManager taskManager;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI cursedItemsCounter;

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
    [SerializeField] private GameObject backToGame;
    [SerializeField] private GameObject backToPrevious;

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject backToMenu;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject videoMenu;
    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject controlsMenu;

    [SerializeField] private GameObject audioButton;
    [SerializeField] private GameObject videoButton;
    [SerializeField] private GameObject controlsButton;
    [SerializeField] private GameObject graphicsButton;
    [SerializeField] private GameObject notesButton;

    [SerializeField] private GameObject audioTitle;
    [SerializeField] private GameObject displayTitle;
    [SerializeField] private GameObject controlsTitle;
    [SerializeField] private GameObject graphicsTitle;

    [SerializeField] private GameObject dot;
    [SerializeField] private GameObject taskChange;
    [SerializeField] private GameObject notesPanel;

    [Header("OTHER")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera secondaryCamera;
    [SerializeField] private Animator taskAnimator;

    [Header("AUDIO")]
    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private AudioSource lockedAudioSource;
    [SerializeField] private AudioSource keysAudioSource;
    [SerializeField] private AudioSource drinkAudioSource;
    [SerializeField] private AudioSource eatAudioSource;
    [SerializeField] private AudioSource clockAudioSource;
    [SerializeField] private AudioSource hoverAudioSource;
    [SerializeField] private AudioSource flickeringAudioSource;
    [SerializeField] private AudioSource rainAudioSource;
    [SerializeField] private AudioClip hoverAudioClip;

    public void ResumeButton()
    {
        if (taskManager.currentTaskIndex == 4)
            cursedItemsCounter.enabled = active;

        DeactivateGameObject.deactivateInstance.DeactivateObject(pauseMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(notesButton);
        ActivateGameObject.activateInstance.ActivateObject(dot);
        ActivateGameObject.activateInstance.ActivateObject(taskChange);
        AudioManager.instance.UnpauseSoundInResumeGameFromPause();
        AudioManager.instance.UnPauseSound(flickeringAudioSource);
        AudioManager.instance.UnPauseSound(rainAudioSource);

        resumeButton.transform.DOScale(0.8f, 0.2f);
        playerController.CheckDoorStateOnResume();
        Time.timeScale = 1.0f;
        taskAnimator.cullingMode = AnimatorCullingMode.CullCompletely;

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
        DeactivateGameObject.deactivateInstance.DeactivateObjectsInBackToMenu();

        backToGameButton.transform.DOScale(3.2f, 0.2f);
        RoundManager.instance.currentGameState = GameState.pause;
    }
}
