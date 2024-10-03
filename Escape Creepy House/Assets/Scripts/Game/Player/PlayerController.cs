using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private float gravity;
    private bool active = true;
    private bool inactive = false;
    private bool isDoorOpenedSoundPaused = false;
    private bool isDoorClosedSoundPaused = false;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject dot;
    [SerializeField] private GameObject taskChange;
    [SerializeField] private GameObject foundItemMessage;
    [SerializeField] private GameObject lockedText;

    [Header("OTHER")]
    [SerializeField] DoorBase doorBase;
    [SerializeField] Interactor interactor;
    [SerializeField] ClockAudio clockAudio;
    [SerializeField] RainAudio rainAudio;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator playerAnimator;
    public RaycastHit hit;
    private Vector3 velocity;

    [Header("AUDIO")]
    [SerializeField] private AudioSource mainGameAudioSource;

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
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(active);
        dot.SetActive(inactive);
        taskChange.SetActive(inactive);
        foundItemMessage.SetActive(inactive);
        lockedText.SetActive(inactive);
        interactor.lockedAudioSource.Pause();
        clockAudio.clockAudioSource.Pause();
        rainAudio.rainAudioSource.Pause();
        mainGameAudioSource.Pause();
        Time.timeScale = 0f;
        CheckDoorStateOnPause();
        RoundManager.instance.currentGameState = GameState.pause;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(inactive);
        dot.SetActive(active);
        taskChange.SetActive(active);
        interactor.lockedAudioSource.UnPause();
        clockAudio.clockAudioSource.UnPause();
        mainGameAudioSource.UnPause();
        Time.timeScale = 1f;
        CheckDoorStateOnResume();
        RoundManager.instance.currentGameState = GameState.playing;
    }

    public void CheckDoorStateOnPause()
    {
        if (doorBase.doorOpenedAudioSource.isPlaying)
        {
            doorBase.doorOpenedAudioSource.Pause();
            isDoorOpenedSoundPaused = true;
        }
        else if (doorBase.doorClosedAudioSource.isPlaying)
        {
            doorBase.doorClosedAudioSource.Pause();
            isDoorClosedSoundPaused = true;
        }
    }

    public void CheckDoorStateOnResume()
    {
        if (isDoorOpenedSoundPaused)
        {
            doorBase.doorOpenedAudioSource.UnPause();
            isDoorOpenedSoundPaused = false;
        }
        if (isDoorClosedSoundPaused)
        {
            doorBase.doorClosedAudioSource.UnPause();
            isDoorClosedSoundPaused = false;
        }
    }
}
