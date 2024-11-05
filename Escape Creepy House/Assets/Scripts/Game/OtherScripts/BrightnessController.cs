using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    [Header("TYPES")]
    private const string brightnessKey = "Brightness";

    [Header("UI")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TextMeshProUGUI brightnessText;

    [Header("OTHER")]
    [SerializeField] private PostProcessProfile brightnessProfile;
    [SerializeField] private PostProcessLayer brightnessLayer;
    private AutoExposure exposure;

    private void Start()
    {
        InitializeBrightness();
    }

    public void InitializeBrightness()
    {
        PostProcessVolume postProcessVolume = Camera.main.GetComponent<PostProcessVolume>();

        if (postProcessVolume != null && postProcessVolume.profile.TryGetSettings(out exposure))
        {
            brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);
            float savedBrightness = PlayerPrefs.GetFloat(brightnessKey);
            brightnessText.text = savedBrightness.ToString("0.0");
        }
        else
        {
            Debug.LogWarning("Post Process Volume or Color Grading effect not found on the main camera.");
        }
    }

    public void OnBrightnessChanged(float value)
    {
        exposure.keyValue.value = value;
        brightnessText.text = brightnessSlider.value.ToString("0.0");
        PlayerPrefs.SetFloat(brightnessKey, value);
    }

}
