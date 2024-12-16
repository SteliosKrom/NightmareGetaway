using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("TYPES")]
    private const string masterVol = "MasterVolume";
    private const string sfxVol = "SoundEffectsVolume";
    private const string menuVol = "MenuVolume";

    private bool active = true;
    private bool inactive = false;

    [Header("SCRIPT REFERENCES")]
    [SerializeField] private CameraRotate cameraRotate;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject displayFPS;

    [Header("UI")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle framesToggle;
    [SerializeField] private Toggle vSyncToggle;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider menuVolumeSlider;

    [SerializeField] private TextMeshProUGUI masterValueText;
    [SerializeField] private TextMeshProUGUI sfxValueText;
    [SerializeField] private TextMeshProUGUI menuValueText;
    [SerializeField] private TextMeshProUGUI antiAliasingText;
    [SerializeField] private TextMeshProUGUI framesText;

    [SerializeField] private TMP_Dropdown antiAliasingDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera secondaryCamera;

    [Header("AUDIO")]
    [SerializeField] private AudioMixer myAudioMixer;

    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        // Load Audio values
        float savedMasterVolume = PlayerPrefs.GetFloat("masterVolume");
        float savedSfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        float savedMenuVolume = PlayerPrefs.GetFloat("menuVolume");

        myAudioMixer.SetFloat(masterVol, Mathf.Log10(savedMasterVolume) * 20);
        myAudioMixer.SetFloat(sfxVol, Mathf.Log10(savedSfxVolume) * 20);
        myAudioMixer.SetFloat(menuVol, Mathf.Log10(savedMenuVolume) * 20);

        masterVolumeSlider.value = savedMasterVolume;
        sfxVolumeSlider.value = savedSfxVolume;
        menuVolumeSlider.value = savedMenuVolume;

        // Load Video & Graphics values
        int savedQualitySettings = PlayerPrefs.GetInt("GraphicsQuality");
        int savedVSyncCount = PlayerPrefs.GetInt("VSyncCount");

        bool savedVSyncToggle = (PlayerPrefs.GetInt("VSyncToggleValue") != 0);
        bool savedFullscreenValue = (PlayerPrefs.GetInt("ScreenValue") != 0);
        bool savedFullscreenToggle = (PlayerPrefs.GetInt("ScreenToggleValue") != 0);

        float savedSensitivityValue = PlayerPrefs.GetFloat("SensValue");

        QualitySettings.vSyncCount = savedVSyncCount;
        qualityDropdown.value = savedQualitySettings;
        cameraRotate.SensitivitySlider = savedSensitivityValue;

        fullscreenToggle.isOn = savedFullscreenToggle;
        vSyncToggle.isOn = savedVSyncToggle;
    }

    public void MasterVolumeSlider()
    {
        float masterVolume = masterVolumeSlider.value;
        masterValueText.text = masterVolume.ToString("0.0");
        myAudioMixer.SetFloat(masterVol, Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
    }

    public void SFXVolumeSlider()
    {
        float sfxVolume = sfxVolumeSlider.value;
        sfxValueText.text = sfxVolume.ToString("0.0");
        myAudioMixer.SetFloat(sfxVol, Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }

    public void MenuVolumeSlider()
    {
        float menuVolume = menuVolumeSlider.value;
        menuValueText.text = menuVolume.ToString("0.0");
        myAudioMixer.SetFloat(menuVol, Mathf.Log10(menuVolume) * 20);
        PlayerPrefs.SetFloat("menuVolume", menuVolume);
    }

    public void SetFullscreen()
    {
        int screenWidth;
        int screenHeight;

        if (fullscreenToggle.isOn)
        {
            fullscreenToggle.isOn = active;
            screenWidth = Screen.currentResolution.width;
            screenHeight = Screen.currentResolution.height;
            Screen.fullScreen = fullscreenToggle.isOn;
            Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.FullScreenWindow);
            PlayerPrefs.SetInt("ScreenToggleValue", (fullscreenToggle.isOn ? 1 : 0));
        }
        else
        {
            fullscreenToggle.isOn = inactive;
            screenWidth = 1280;
            screenHeight = 720;
            Screen.fullScreen = !fullscreenToggle.isOn;
            Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.Windowed);
            PlayerPrefs.SetInt("ScreenToggleValue", (fullscreenToggle.isOn ? 1 : 0));
        }
    }


    public void SetVSync()
    {
        if (vSyncToggle.isOn)
        {
            vSyncToggle.isOn = active;
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("VSyncCount", QualitySettings.vSyncCount);
            PlayerPrefs.SetInt("VSyncToggleValue", (vSyncToggle.isOn ? 1 : 0));
        }
        else
        {
            vSyncToggle.isOn = inactive;
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("VSyncCount", QualitySettings.vSyncCount);
            PlayerPrefs.SetInt("VSyncToggleValue", (vSyncToggle.isOn ? 1 : 0));
        }
    }

    public void SetAntiAliasing()
    {
        if (antiAliasingDropdown.value == 0)
        {
            QualitySettings.antiAliasing = 0;
        }
        else if (antiAliasingDropdown.value == 1)
        {
            QualitySettings.antiAliasing = 2;
        }
        else if (antiAliasingDropdown.value == 2)
        {
            QualitySettings.antiAliasing = 4;
        }
        else if (antiAliasingDropdown.value == 3)
        {
            QualitySettings.antiAliasing = 8;
        }
    }

    public void SetGraphicsQuality()
    {
        int qualityValue = qualityDropdown.value;
        QualitySettings.SetQualityLevel(qualityValue);
        PlayerPrefs.SetInt("GraphicsQuality", qualityValue);
    }

    public void SetFPS()
    {
        if (!framesToggle.isOn)
        {
            displayFPS.SetActive(inactive);
        }
        else
        {
            displayFPS.SetActive(active);
        }
    }
}
