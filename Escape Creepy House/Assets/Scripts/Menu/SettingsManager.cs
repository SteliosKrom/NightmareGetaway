using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("TYPES")]
    private const string masterVol = "MasterVolume";
    private const string gameMusicVol = "GameMusicVolume";
    private const string sfxVol = "SoundEffectsVolume";

    [Header("CLASSES")]
    [SerializeField] private AudioMixer myAudioMixer;

    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vSyncToggle;
    [SerializeField] private Toggle framesToggle;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider mainGameVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [SerializeField] private TextMeshProUGUI masterValueText;
    [SerializeField] private TextMeshProUGUI gameMusicValueText;
    [SerializeField] private TextMeshProUGUI sfxValueText;
    [SerializeField] private TextMeshProUGUI antiAliasingText;
    [SerializeField] private TextMeshProUGUI framesText;

    [SerializeField] private TMP_Dropdown antiAliasingDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [SerializeField] private GameObject displayFPS;

    private void Start()
    {
        LoadSettings();
        InitializeFPS();
        InitializeVsyncAndAA();
        InitialiazeFullscreen();
    }

    public void InitializeFPS()
    {
        framesToggle.isOn = false;
        displayFPS.SetActive(false);
    }

    public void InitialiazeFullscreen()
    {
        if (Screen.fullScreen)
        {
            fullscreenToggle.isOn = true;
        }
        else
        {
            fullscreenToggle.isOn = false;
        }
    }

    public void InitializeVsyncAndAA()
    {
        QualitySettings.vSyncCount = 0;
        vSyncToggle.isOn = false;

        QualitySettings.antiAliasing = 0;
        antiAliasingDropdown.value = 0;
    }

    public void LoadSettings()
    {
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        float savedGameVolume = PlayerPrefs.GetFloat("GameVolume");
        float savedSfxVolume = PlayerPrefs.GetFloat("sfxVolume");

        int savedQualitySettings = PlayerPrefs.GetInt("GraphicsQuality");

        myAudioMixer.SetFloat(masterVol, Mathf.Log10(savedMasterVolume) * 20);
        myAudioMixer.SetFloat(gameMusicVol, Mathf.Log10(savedGameVolume) * 20);
        myAudioMixer.SetFloat(sfxVol, Mathf.Log10(savedSfxVolume) * 20);

        masterVolumeSlider.value = savedMasterVolume;
        mainGameVolumeSlider.value = savedGameVolume;
        sfxVolumeSlider.value = savedSfxVolume;
        qualityDropdown.value = savedQualitySettings;
    }

    public void MasterVolumeSlider()
    {
        float masterVolume = masterVolumeSlider.value;
        masterValueText.text = masterVolume.ToString("0.0");
        myAudioMixer.SetFloat(masterVol, Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }

    public void GameVolumeSlider()
    {
        float gameVolume = mainGameVolumeSlider.value;
        gameMusicValueText.text = gameVolume.ToString("0.0");
        myAudioMixer.SetFloat(gameMusicVol, Mathf.Log10(gameVolume) * 20);
        PlayerPrefs.SetFloat("GameVolume", gameVolume);
    }

    public void SFXVolumeSlider()
    {
        float sfxVolume = sfxVolumeSlider.value;
        sfxValueText.text = sfxVolume.ToString("0.0");
        myAudioMixer.SetFloat(sfxVol, Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
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

    [System.Obsolete]
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
            displayFPS.SetActive(false);
        }
        else
        {
            displayFPS.SetActive(true);
        }
    }
}
