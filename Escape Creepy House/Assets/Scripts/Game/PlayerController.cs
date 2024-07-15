using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("STRUCTS")]
    public RaycastHit hit;
    private Vector3 velocity;

    [Header("TYPES")]
    public float playerSpeed;
    [SerializeField] private float gravity;

    [Header("CLASSES")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator playerAnimator;
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

        if (Physics.Raycast(transform.position, Vector3.down, out var hit))
        {
            Debug.Log(hit.collider);
        }
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
                playerAnimator.SetBool("isMoving", true);
            }
            else
            {
                playerAnimator.SetBool("isMoving", false);
            }
        }
    }

    public void PauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.instance.currentState == GameState.playing)
        {
            PauseGame();
            mainGameAudioSource.Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.instance.currentState == GameState.pause)
        {
            ResumeGame();
            mainGameAudioSource.UnPause();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        RoundManager.instance.currentState = GameState.pause;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        RoundManager.instance.currentState = GameState.playing;
    }
}
