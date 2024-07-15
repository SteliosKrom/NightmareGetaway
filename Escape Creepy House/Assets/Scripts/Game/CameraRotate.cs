using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private PlayerRotate playerRotate;
    [SerializeField] private float sensY;
    [SerializeField] private float sensX;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private float xRotation;
    public float yRotation;
    private float zRotation;


    private void Start()
    {
        xRotation = 0f;
        yRotation = -90f;
        zRotation = 0f;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
    }

    private void Update()
    {
        if (RoundManager.instance.currentState == GameState.onMainMenu || RoundManager.instance.currentState == GameState.onSettings || RoundManager.instance.currentState == GameState.pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (RoundManager.instance.currentState == GameState.playing)
        {
            float mouseY = Input.GetAxis("Mouse Y") * sensY;
            float mouseX = Input.GetAxis("Mouse X") * sensX;

            xRotation -= mouseY;
            yRotation += mouseX;

            xRotation = Mathf.Clamp(xRotation, minX, maxX);
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        if (playerRotate != null)
        {
            playerRotate.RotatePlayer(yRotation);
        }
    }
}
