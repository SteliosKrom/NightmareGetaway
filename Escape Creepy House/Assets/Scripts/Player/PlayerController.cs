using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("TYPES")]
    public float playerSpeed;
    private float gravity;
    private float pauseDelay = 0.25f;

    private bool active = true;
    private bool inactive = false;
    private bool isDoorOpenedSoundPaused = false;
    private bool isDoorClosedSoundPaused = false;
    private bool canPause = true;

    [Header("SCRIPT REFERENCES")]
    [SerializeField] private DoorBase doorBase;
    [SerializeField] private Interactor interactor;
    [SerializeField] private ClockAudio clockAudio;
    [SerializeField] private FlashlightFlickering flashlightFlickering;
    [SerializeField] private AddEventTrigger addEventTrigger;
    [SerializeField] private TaskManager taskManager;

    [Header("UI")]
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
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject dot;
    [SerializeField] private GameObject taskChange;
    [SerializeField] private GameObject foundItemMessage;
    [SerializeField] private GameObject lockedText;
    [SerializeField] private GameObject foundRoomKey;
    [SerializeField] private GameObject foundMainDoorKey;
    [SerializeField] private GameObject foundGarageDoorKey;
    [SerializeField] private GameObject notesButton;
    [SerializeField] private GameObject notesPanel;
    [SerializeField] private GameObject cursedItemsCounter;

    [Header("AUDIO")]
    [SerializeField] private AudioSource rainAudioSource;
    [SerializeField] private AudioSource keysAudioSource;
    [SerializeField] private AudioSource eatAudioSource;
    [SerializeField] private AudioSource drinkAudioSource;
    [SerializeField] private AudioSource lockedAudioSource;
    [SerializeField] private AudioSource clockAudioSource;
    [SerializeField] private AudioSource ceilingFanAudioSource;
    [SerializeField] private AudioSource flickeringAudioSource;
    [SerializeField] private AudioClip flickeringAudioClip;

    [Header("OTHER")]
    [SerializeField] CharacterController characterController;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator taskAnimator;
    public RaycastHit hit;
    private Vector3 velocity;

    private void Update()
    {
        PauseAndResume();
        MovePlayer();
    }

    public void MovePlayer()
    {
        if (RoundManager.instance.currentGameState == GameState.playing)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 moveDirection = transform.TransformDirection(horizontal, 0f, vertical);
            Vector3 finalMovement = moveDirection * playerSpeed + velocity;
            characterController.Move(finalMovement * Time.deltaTime);

            if (characterController.isGrounded)
            {
                velocity.y = -1f;
            }
            else
            {
                velocity.y -= gravity * -2f * Time.deltaTime;
            }

            if (moveDirection != Vector3.zero)
            {
                playerAnimator.SetBool("isMoving", active);
            }
            else
            {
                playerAnimator.SetBool("isMoving", inactive);
            }
        }
    }

    public void PauseAndResume()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && canPause)
        {
            StartCoroutine(PauseDelay());
        }
    }

    public void UpdateCursorDisplay()
    {
        switch (RoundManager.instance.currentGameState)
        {
            case GameState.playing:
                PauseGame();
                break;

            case GameState.pause:
                ResumeGameFromPauseMenu();
                break;

            case GameState.onSettingsGame:
                ResumeGameFromGameSettings();
                ResumeGameFromNotesPanel();
                break;
        }
    }

    public void PauseGame()
    {
        ActivateGameObject.activateInstance.ActivateObject(pauseMenu);
        ActivateGameObject.activateInstance.ActivateObject(notesButton);
        DeactivateGameObject.deactivateInstance.DeactivateObjectsInPause();

        cursedItemsCounter.SetActive(inactive);

        AudioManager.instance.PauseSoundInPause();
        AudioManager.instance.PauseSound(flickeringAudioSource);
        AudioManager.instance.PauseSound(rainAudioSource);

        CheckDoorStateOnPause();
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = active;
        RoundManager.instance.currentGameState = GameState.pause;
    }

    public void ResumeGameFromGameSettings()
    {
        ActivateGameObject.activateInstance.ActivateObject(pauseMenu);
        ActivateGameObject.activateInstance.ActivateObject(notesButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(settingsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(controlsMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(dot);
        DeactivateGameObject.deactivateInstance.DeactivateObject(taskChange);
        DeactivateGameObject.deactivateInstance.DeactivateObjectsInBackToMenu();

        addEventTrigger.ExitHoverSoundEffectSettings(audioCategoryButton.transform);
        addEventTrigger.ExitHoverSoundEffectSettings(videoCategoryButton.transform);
        addEventTrigger.ExitHoverSoundEffectSettings(graphicsCategoryButton.transform);
        addEventTrigger.ExitHoverSoundEffectSettings(controlsCategoryButon.transform);
        addEventTrigger.ExitHoverSoundEffectOther(backToGameButton.transform);
        addEventTrigger.ExitHoverSoundEffectOther(backToPreviousButton.transform);

        Time.timeScale = 0f;
        RoundManager.instance.currentGameState = GameState.pause;
    }

    public void ResumeGameFromPauseMenu()
    {
        if (taskManager.currentTaskIndex == 4)
            cursedItemsCounter.SetActive(active);

        ActivateGameObject.activateInstance.ActivateObject(dot);
        ActivateGameObject.activateInstance.ActivateObject(taskChange);
        DeactivateGameObject.deactivateInstance.DeactivateObject(notesButton);
        DeactivateGameObject.deactivateInstance.DeactivateObject(pauseMenu);

        AudioManager.instance.UnpauseSoundInResumeGameFromPause();
        AudioManager.instance.UnPauseSound(flickeringAudioSource);
        AudioManager.instance.UnPauseSound(rainAudioSource);

        addEventTrigger.ExitHoverSoundEffectPause(resumeButton.transform);
        addEventTrigger.ExitHoverSoundEffectPause(settingsButton.transform);
        addEventTrigger.ExitHoverSoundEffectPause(homeButton.transform);
        addEventTrigger.ExitHoverSoundEffectPause(exitButton.transform);

        CheckDoorStateOnResume();
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = inactive;
        RoundManager.instance.currentGameState = GameState.playing;
    }

    public void ResumeGameFromNotesPanel()
    {
        ActivateGameObject.activateInstance.ActivateObject(pauseMenu);
        DeactivateGameObject.deactivateInstance.DeactivateObject(notesPanel);

        addEventTrigger.ExitHoverSoundEffectOther(backToGameButton.transform);

        Time.timeScale = 0f;
        RoundManager.instance.currentGameState = GameState.pause;
    }

    public void CheckDoorStateOnPause()
    {
        if (doorBase.doorOpenedAudioSource.isPlaying)
        {
            AudioManager.instance.PauseSound(doorBase.doorOpenedAudioSource);
            isDoorOpenedSoundPaused = active;
        }
        else if (doorBase.doorClosedAudioSource.isPlaying)
        {
            AudioManager.instance.PauseSound(doorBase.doorClosedAudioSource);
            isDoorClosedSoundPaused = active;
        }
    }

    public void CheckDoorStateOnResume()
    {
        if (isDoorOpenedSoundPaused)
        {
            AudioManager.instance.UnPauseSound(doorBase.doorOpenedAudioSource);
            isDoorOpenedSoundPaused = inactive;
        }
        else if (isDoorClosedSoundPaused)
        {
            AudioManager.instance.UnPauseSound(doorBase.doorClosedAudioSource);
            isDoorClosedSoundPaused = inactive;
        }
    }

    public IEnumerator PauseDelay()
    {
        canPause = inactive;
        yield return new WaitForSecondsRealtime(pauseDelay);
        UpdateCursorDisplay();
        canPause = active;
    }
}
