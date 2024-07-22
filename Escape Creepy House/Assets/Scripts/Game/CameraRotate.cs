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

    [Header("TYPES")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float sensY;
    [SerializeField] private float sensX;

    private float defaultSensitivity = 1f;
    private float spotlightRotationSpeed = 2.5f;
    private float xRotation;
    private float yRotation;
    private float zRotation;
    private float spotlightYRotation;
    private float spotlightXRotation;

    private void Start()
    {
        xRotation = 0f;
        yRotation = -90f;
        zRotation = 0f;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        sensitivitySlider.value = defaultSensitivity;

        spotlightYRotation = yRotation;
        spotlightXRotation = xRotation;
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

            playerRotate.RotatePlayer(yRotation);

            UpdateSpotlightRotation(yRotation, xRotation);
        }
    }

    public void UpdateSpotlightRotation(float newYRotation, float newXRotation)
    {
        spotlightYRotation = Mathf.Lerp(spotlightYRotation, newYRotation, spotlightRotationSpeed * Time.deltaTime);
        spotlightXRotation = Mathf.Lerp(spotlightXRotation, newXRotation, spotlightRotationSpeed * Time.deltaTime);
        spotlight.rotation = Quaternion.Euler(spotlightXRotation, spotlightYRotation, zRotation);
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
