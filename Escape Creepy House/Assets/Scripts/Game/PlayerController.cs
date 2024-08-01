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
    [SerializeField] private GameObject dot;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator playerAnimator;

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
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.instance.currentState == GameState.pause)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        dot.SetActive(false);
        RoundManager.instance.currentState = GameState.pause;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        dot.SetActive(true);
        RoundManager.instance.currentState = GameState.playing;
    }
}
