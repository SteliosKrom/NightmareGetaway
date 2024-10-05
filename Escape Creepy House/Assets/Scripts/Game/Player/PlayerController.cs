using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private float gravity;
    private bool active = true;
    private bool inactive = false;
    private bool isDoorOpenedSoundPaused = false;
    private bool isDoorClosedSoundPaused = false;

    [Header("SCRIPT REFERENCES")]
    [SerializeField] private DoorBase doorBase;
    [SerializeField] private Interactor interactor;
    [SerializeField] private ClockAudio clockAudio;
    [SerializeField] private FlashlightFlickering flashlightFlickering;
    [SerializeField] private AddEventTrigger addEventTrigger;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
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
        if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.instance.currentGameState == GameState.playing)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.instance.currentGameState == GameState.pause)
        {
            ResumeGameFromPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.instance.currentGameState == GameState.onSettingsGame)
        {
            ResumeGameFromGameSettings();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(active);
        taskChange.SetActive(inactive);
        foundItemMessage.SetActive(inactive);
        lockedText.SetActive(inactive);
        dot.SetActive(inactive);
        foundGarageDoorKey.SetActive(inactive);
        foundMainDoorKey.SetActive(inactive);
        foundRoomKey.SetActive(inactive);

        AudioManager.instance.PauseSound(lockedAudioSource);
        AudioManager.instance.PauseSound(eatAudioSource);
        AudioManager.instance.PauseSound(drinkAudioSource);
        AudioManager.instance.PauseSound(keysAudioSource);
        AudioManager.instance.PauseSound(rainAudioSource);
        AudioManager.instance.PauseSound(mainGameAudioSource);
        AudioManager.instance.PauseSound(clockAudioSource);

        Time.timeScale = 0f;
        CheckDoorStateOnPause();
        RoundManager.instance.currentGameState = GameState.pause;
    }

    public void ResumeGameFromGameSettings()
    {
        settingsMenu.SetActive(inactive);
        pauseMenu.SetActive(inactive);
        Time.timeScale = 1f;
        addEventTrigger.ExitHoverSoundEffectPause(transform);
        RoundManager.instance.currentGameState = GameState.playing;
    }

    public void ResumeGameFromPauseMenu()
    {
        pauseMenu.SetActive(inactive);
        dot.SetActive(active);
        taskChange.SetActive(active);

        AudioManager.instance.UnPauseSound(lockedAudioSource);
        AudioManager.instance.UnPauseSound(drinkAudioSource);
        AudioManager.instance.UnPauseSound(eatAudioSource);
        AudioManager.instance.UnPauseSound(clockAudioSource);
        AudioManager.instance.UnPauseSound(keysAudioSource);
        AudioManager.instance.UnPauseSound(mainGameAudioSource);

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
}
