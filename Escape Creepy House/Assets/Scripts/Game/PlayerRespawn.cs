using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public Animator animator;
    public CharacterController characterController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public void Respawn()
    {
        transform.position = spawnPoint.position;
        transform.rotation = Quaternion.Euler(0f, -90f, 0);

        if (characterController != null)
        {
            characterController.enabled = false;
            characterController.enabled = true;
        }

        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }
    }
}
