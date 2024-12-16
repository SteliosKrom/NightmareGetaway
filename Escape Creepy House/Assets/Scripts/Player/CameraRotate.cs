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

    private float spotlightRotationSpeed = 10f;
    private float mainCameraRotationSpeed = 4f;

    private float xRotation;
    private float yRotation;
    private float zRotation;

    private float spotlightYRotation;
    private float spotlightXRotation;

    private float mainCameraYRotation;
    private float mainCameraXRotation;

    // Properties below

    public float SensitivitySlider
    {
        get { return sensitivitySlider.value; }
        set { sensitivitySlider.value = value; }
    }

    private void Start()
    {
        xRotation = 0f;
        yRotation = -90f;
        zRotation = 0f;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);

        spotlightYRotation = yRotation;
        spotlightXRotation = xRotation;

        mainCameraYRotation = yRotation;
        mainCameraXRotation = xRotation;
    }

    private void Update()
    {
        if (RoundManager.instance.currentGameState == GameState.playing)
        {
            float mouseY = Input.GetAxis("Mouse Y") * sensitivitySlider.value;
            float mouseX = Input.GetAxis("Mouse X") * sensitivitySlider.value;

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
        sensitivityValueText.text = sensitivitySlider.value.ToString("0.0");
        PlayerPrefs.SetFloat("SensValue", sensitivitySlider.value);
        PlayerPrefs.Save();
    }
}
