using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public DoorBase doorBase;
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

    [Header("OTHER")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator playerAnimator;
    public RaycastHit hit;
    private Vector3 velocity;

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
        if (RoundManager.instance.currentState == GameState.playing)
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
        if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.instance.currentState == GameState.playing)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.instance.currentState == GameState.pause)
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
        Time.timeScale = 0f;
        CheckDoorStateOnPause();
        RoundManager.instance.currentState = GameState.pause;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(inactive);
        dot.SetActive(active);
        taskChange.SetActive(active);
        Time.timeScale = 1f;
        CheckDoorStateOnResume();
        RoundManager.instance.currentState = GameState.playing;
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
