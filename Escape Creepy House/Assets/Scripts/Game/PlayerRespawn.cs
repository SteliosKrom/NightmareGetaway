using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public Animator animator;
    public CharacterController characterController;

    public CameraRotate cameraRotate;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public void Respawn()
    {
        transform.position = spawnPoint.position;
        
        if (characterController != null)
        {
            characterController.enabled = false;
            characterController.enabled = true;
        }

        if (cameraRotate != null)
        {
            cameraRotate.SetInitialRotation(-90f);
        }

        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }
    }
}
