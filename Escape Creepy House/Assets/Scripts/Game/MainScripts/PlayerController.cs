using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private float gravity;
    private float cooldown = 1f;

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
    [SerializeField] private DeactivateGameObject deactivateGameObject;
    [SerializeField] private ActivateGameObject activateGameObject;

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

    [Header("OTHER")]
    [SerializeField] CharacterController characterController;
    [SerializeField] Animator playerAnimator;
    public RaycastHit hit;
    private Vector3 velocity;

    [Header("AUDIO")]
    [SerializeField] private AudioSource mainGameAudioSource;
    [SerializeField] private AudioSource rainAudioSource;
    [SerializeField] private AudioSource keysAudioSource;
    [SerializeField] private AudioSource eatAudioSource;
    [SerializeField] private AudioSource drinkAudioSource;
    [SerializeField] private AudioSource lockedAudioSource;
    [SerializeField] private AudioSource clockAudioSource;
    [SerializeField] private AudioSource ceilingFanAudioSource;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

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
        if (canPause && Input.GetKeyDown(KeyCode.Escape))
        {
            canPause = inactive;
            if (RoundManager.instance.currentGameState == GameState.playing)
            {
                PauseGame();
            }
            else if (RoundManager.instance.currentGameState == GameState.pause)
            {
                ResumeGameFromPauseMenu();
            }
            else if (RoundManager.instance.currentGameState == GameState.onSettingsGame)
            {
                ResumeGameFromGameSettings();
            }
            StartCoroutine(HandleCooldown());
        }   
    }

    public void PauseGame()
    {
        activateGameObject.ActivateObject(pauseMenu);

        //Make a method for this
        deactivateGameObject.DeactivateObject(taskChange);
        deactivateGameObject.DeactivateObject(foundItemMessage);
        deactivateGameObject.DeactivateObject(lockedText);
        deactivateGameObject.DeactivateObject(dot);
        deactivateGameObject.DeactivateObject(foundGarageDoorKey);
        deactivateGameObject.DeactivateObject(foundMainDoorKey);
        deactivateGameObject.DeactivateObject(foundRoomKey);

        // Make a method for this
        AudioManager.instance.PauseSound(lockedAudioSource);
        AudioManager.instance.PauseSound(eatAudioSource);
        AudioManager.instance.PauseSound(drinkAudioSource);
        AudioManager.instance.PauseSound(keysAudioSource);
        AudioManager.instance.PauseSound(rainAudioSource);
        AudioManager.instance.PauseSound(mainGameAudioSource);
        AudioManager.instance.PauseSound(clockAudioSource);
        AudioManager.instance.PauseSound(ceilingFanAudioSource);

        Time.timeScale = 0f;
        CheckDoorStateOnPause();
        RoundManager.instance.currentGameState = GameState.pause;
    }

    public void ResumeGameFromGameSettings()
    {
        activateGameObject.ActivateObject(pauseMenu);

        deactivateGameObject.DeactivateObject(settingsMenu);
        deactivateGameObject.DeactivateObject(controlsMenu);
        deactivateGameObject.DeactivateObject(dot);
        deactivateGameObject.DeactivateObject(taskChange);

        // Make a method for this
        AudioManager.instance.UnPauseSound(lockedAudioSource);
        AudioManager.instance.UnPauseSound(eatAudioSource);
        AudioManager.instance.UnPauseSound(drinkAudioSource);
        AudioManager.instance.UnPauseSound(keysAudioSource);
        AudioManager.instance.UnPauseSound(clockAudioSource);
        AudioManager.instance.UnPauseSound(ceilingFanAudioSource);

        // Make a method for this
        addEventTrigger.ExitHoverSoundEffectSettings(audioCategoryButton.transform);
        addEventTrigger.ExitHoverSoundEffectSettings(videoCategoryButton.transform);
        addEventTrigger.ExitHoverSoundEffectSettings(graphicsCategoryButton.transform);
        addEventTrigger.ExitHoverSoundEffectSettings(controlsCategoryButon.transform);
        addEventTrigger.ExitHoverSoundEffectOther(backToGameButton.transform);
        addEventTrigger.ExitHoverSoundEffectOther(backToPreviousButton.transform);

        Time.timeScale = 1f;
        RoundManager.instance.currentGameState = GameState.pause;
    }

    public void ResumeGameFromPauseMenu()
    {
        activateGameObject.ActivateObject(dot);
        activateGameObject.ActivateObject(taskChange);

        deactivateGameObject.DeactivateObject(pauseMenu);

        // Make a method for this
        AudioManager.instance.UnPauseSound(lockedAudioSource);
        AudioManager.instance.UnPauseSound(drinkAudioSource);
        AudioManager.instance.UnPauseSound(eatAudioSource);
        AudioManager.instance.UnPauseSound(clockAudioSource);
        AudioManager.instance.UnPauseSound(keysAudioSource);
        AudioManager.instance.UnPauseSound(mainGameAudioSource);

        addEventTrigger.ExitHoverSoundEffectPause(resumeButton.transform);
        addEventTrigger.ExitHoverSoundEffectPause(settingsButton.transform);
        addEventTrigger.ExitHoverSoundEffectPause(homeButton.transform);
        addEventTrigger.ExitHoverSoundEffectPause(exitButton.transform);

        Time.timeScale = 1f;
        CheckDoorStateOnResume();
        RoundManager.instance.currentGameState = GameState.playing;
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
            AudioManager.instance.UnPauseSound(doorBase.doorClosedAudioSource);
            isDoorOpenedSoundPaused = inactive;
        }
        if (isDoorClosedSoundPaused)
        {
            AudioManager.instance.UnPauseSound(doorBase.doorClosedAudioSource);
            isDoorClosedSoundPaused = inactive;
        }
    }

    public IEnumerator HandleCooldown()
    {
        yield return new WaitForSecondsRealtime(cooldown);
        canPause = active;
    }
}
