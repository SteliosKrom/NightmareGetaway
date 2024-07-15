using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [Header("CLASSES")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerController playerController;

    [Header("TYPES")]
    private bool isCrouching = false;
    private float originalHeight;
    private float crouchHeight = 1f;
    private float crouchSpeed = 1.5f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        originalHeight = characterController.height;
    }

    private void Update()
    {
        Crouching();
    }

    public void Crouching()
    {
        if (RoundManager.instance.currentState == GameState.playing && Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                characterController.height = originalHeight;
                playerController.playerSpeed = 3f;
            }
            else
            {
                characterController.height = crouchHeight;
                playerController.playerSpeed = crouchSpeed;
            }
        }
    }
}
