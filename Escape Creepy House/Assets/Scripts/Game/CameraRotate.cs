using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotate : MonoBehaviour
{
    [Header("CLASSES")]
    [SerializeField] private PlayerRotate playerRotate;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityValueText;

    [Header("TYPES")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float sensY;
    [SerializeField] private float sensX;

    private float defaultSensitivity = 1f;
    private float xRotation;
    private float yRotation;
    private float zRotation;

    private void Start()
    {
        xRotation = 0f;
        yRotation = -90f;
        zRotation = 0f;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);

        sensitivitySlider.value = defaultSensitivity;
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

    public void SetInitialRotation(float initialYRotation)
    {
        yRotation = initialYRotation;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
    }

    public void OnSensitivityChanged()
    {
        sensX = sensitivitySlider.value;
        sensY = sensitivitySlider.value;

        sensitivityValueText.text = sensitivitySlider.value.ToString("0.0");
    }
}
