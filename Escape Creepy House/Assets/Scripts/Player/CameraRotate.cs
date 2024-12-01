using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotate : MonoBehaviour
{
    [Header("CLASSES")]
    [SerializeField] private PlayerRotate playerRotate;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityValueText;
    [SerializeField] private Transform spotlight;
    [SerializeField] private Transform mainCamera;

    [Header("TYPES")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float sensY;
    [SerializeField] private float sensX;

    private float defaultSensitivity = 1f;
    private float spotlightRotationSpeed = 10f;
    private float mainCameraRotationSpeed = 4f;

    private float xRotation;
    private float yRotation;
    private float zRotation;

    private float spotlightYRotation;
    private float spotlightXRotation;

    private float mainCameraYRotation;
    private float mainCameraXRotation;

    private void Start()
    {
        xRotation = 0f;
        yRotation = -90f;
        zRotation = 0f;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        sensitivitySlider.value = defaultSensitivity;

        spotlightYRotation = yRotation;
        spotlightXRotation = xRotation;

        mainCameraYRotation = yRotation;
        mainCameraXRotation = xRotation; 
    }

    private void Update()
    {
        if (RoundManager.instance.currentGameState == GameState.onMainMenu || RoundManager.instance.currentGameState == GameState.onSettings ||RoundManager.instance.currentGameState == GameState.onSettingsMenu || RoundManager.instance.currentGameState == GameState.pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (RoundManager.instance.currentGameState == GameState.playing)
        {
            float mouseY = Input.GetAxis("Mouse Y") * sensY;
            float mouseX = Input.GetAxis("Mouse X") * sensX;

            xRotation -= mouseY;
            yRotation += mouseX;

            xRotation = Mathf.Clamp(xRotation, minX, maxX);
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);

            playerRotate.RotatePlayer(yRotation);

            UpdateSpotlightRotation(yRotation, xRotation);
            UpdateCameraRotation(yRotation, xRotation);
        }
    }

    public void UpdateSpotlightRotation(float newYRotation, float newXRotation)
    {
        spotlightYRotation = Mathf.Lerp(spotlightYRotation, newYRotation, spotlightRotationSpeed * Time.deltaTime);
        spotlightXRotation = Mathf.Lerp(spotlightXRotation, newXRotation, spotlightRotationSpeed * Time.deltaTime);
        spotlight.rotation = Quaternion.Euler(spotlightXRotation, spotlightYRotation, zRotation);
    }

    public void UpdateCameraRotation(float newYRotation, float newXRotation)
    {
        mainCameraYRotation = Mathf.Lerp(mainCameraYRotation, newYRotation, mainCameraRotationSpeed * Time.deltaTime);
        mainCameraXRotation = Mathf.Lerp(mainCameraXRotation, newXRotation, mainCameraRotationSpeed * Time.deltaTime);
        mainCamera.rotation = Quaternion.Euler(mainCameraXRotation, mainCameraYRotation, zRotation);
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
