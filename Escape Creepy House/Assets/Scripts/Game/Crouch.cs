using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace CrouchSystem
{
    public class Crouch : MonoBehaviour
    {
        [Header("CLASSES")]
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerController playerController;

        [SerializeField] private AudioSource crouchingAudioSourceStart;
        [SerializeField] private AudioSource crouchingAudioSourceEnd;

        [Header("TYPES")]
        private bool isCrouching = false;
        private bool canCrouch = true;
        private float crouchingDelay = 1f;
        private float originalHeight;
        private float crouchHeight = 1f;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerController = GetComponent<PlayerController>();
            originalHeight = characterController.height;
            isCrouching = !isCrouching;
        }

        private void Update()
        {
            if (canCrouch && RoundManager.instance.currentGameState == GameState.playing)
            {
                Crouching();
            }

        }

        public void Crouching()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isCrouching = !isCrouching;

                if (isCrouching)
                {
                    characterController.height = originalHeight;
                    playerController.playerSpeed = 3f;
                    crouchingAudioSourceEnd.Play();
                }
                else
                {
                    characterController.height = crouchHeight;
                    playerController.playerSpeed = 1.5f;
                    crouchingAudioSourceStart.Play();
                }
                StartCoroutine(CrouchingDelay());
            }
        }

        public IEnumerator CrouchingDelay()
        {
            canCrouch = false;
            yield return new WaitForSeconds(crouchingDelay);
            canCrouch = true;
        }
    }
}

