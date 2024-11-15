using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SettingsManager : MonoBehaviour
{
    [Header("TYPES")]
    private const string masterVol = "MasterVolume";
    private const string sfxVol = "SoundEffectsVolume";
    private const string menuVol = "MenuVolume";

    private bool active = true;
    private bool inactive = false;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject displayFPS;

    [Header("UI")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vSyncToggle;
    [SerializeField] private Toggle framesToggle;

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
        InitializeQualitySettings();
        InitializeFPS();
        InitializeVsyncAndAA();
        InitialiazeFullscreen();
        InitializeResolution();
        LoadSettings();
    }

    public void InitializeResolution()
    {
        int screenHeight = Screen.currentResolution.height;
        int screenWidth = Screen.currentResolution.width;
        Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreen);
    }

    public void InitializeFPS()
    {
        framesToggle.isOn = inactive;
        displayFPS.SetActive(inactive);
    }

    public void InitialiazeFullscreen()
    {
        if (Screen.fullScreen)
        {
            fullscreenToggle.isOn = active;
        }
        else
        {
            fullscreenToggle.isOn = inactive;
            fullscreenToggle.isOn = inactive;
        }
    }

    public void InitializeVsyncAndAA()
    {
        QualitySettings.vSyncCount = 0;
        vSyncToggle.isOn = inactive;

        QualitySettings.antiAliasing = 0;
        antiAliasingDropdown.value = 0;
    }

    public void InitializeQualitySettings()
    {
        QualitySettings.SetQualityLevel(4);
        qualityDropdown.value = 4;
    }

    public void LoadSettings()
    {
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        float savedSfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        float savedMenuVolume = PlayerPrefs.GetFloat("menuVolume");
         
        int savedQualitySettings = PlayerPrefs.GetInt("GraphicsQuality");

        myAudioMixer.SetFloat(masterVol, Mathf.Log10(savedMasterVolume) * 20);
        myAudioMixer.SetFloat(sfxVol, Mathf.Log10(savedSfxVolume) * 20);
        myAudioMixer.SetFloat(menuVol, Mathf.Log10(savedMenuVolume) * 20);

        masterVolumeSlider.value = savedMasterVolume;
        sfxVolumeSlider.value = savedSfxVolume;
        menuVolumeSlider.value = savedMenuVolume;
        qualityDropdown.value = savedQualitySettings;
    }

    public void MasterVolumeSlider()
    {
        float masterVolume = masterVolumeSlider.value;
        masterValueText.text = masterVolume.ToString("0.0");
        myAudioMixer.SetFloat(masterVol, Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
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
        menuValueText.text = menuVolume.ToString("");
        myAudioMixer.SetFloat(menuVol, Mathf.Log10(menuVolume) * 20);
        PlayerPrefs.SetFloat("menuVolume", menuVolume);
    }

    public void SetFullscreen()
    {
        int screenWidth;
        int screenHeight;

        if (fullscreenToggle.isOn)
        {
            screenWidth = Screen.currentResolution.width;
            screenHeight = Screen.currentResolution.height;
            Screen.fullScreen = fullscreenToggle.isOn;
            Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.ExclusiveFullScreen);
        }
        else
        {
            screenWidth = 1280;
            screenHeight = 720;
            Screen.fullScreen = !fullscreenToggle.isOn;
            Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.Windowed);
        }
    }


    public void SetVSync()
    {
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
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
