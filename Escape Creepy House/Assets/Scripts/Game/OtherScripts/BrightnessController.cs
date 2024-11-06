using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
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
            float savedBrightness = PlayerPrefs.GetFloat("Brightness");
            brightnessText.text = savedBrightness.ToString("0.0");
        }
    }

    public void OnBrightnessChanged(float value)
    {
        exposure.keyValue.value = value;
        PlayerPrefs.SetFloat("Brightness", exposure.keyValue.value);
        brightnessText.text = brightnessSlider.value.ToString("0.0");
    }

}
