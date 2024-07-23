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

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider mainGameVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [SerializeField] private TextMeshProUGUI masterValueText;
    [SerializeField] private TextMeshProUGUI gameMusicValueText;
    [SerializeField] private TextMeshProUGUI sfxValueText;
    [SerializeField] private TextMeshProUGUI antiAliasingText;

    [SerializeField] private TMP_Dropdown antiAliasingDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        vSyncToggle.isOn = false;

        QualitySettings.antiAliasing = 0;
        antiAliasingDropdown.value = 0;

        QualitySettings.SetQualityLevel(2);
        qualityDropdown.value = 2;

        if (Screen.fullScreen)
        {
            fullscreenToggle.isOn = true;
        }
        else
        {
            fullscreenToggle.isOn = false;
        }
    }

    public void MasterVolumeSlider()
    {
        float masterVolume = masterVolumeSlider.value;
        masterValueText.text = masterVolume.ToString("0.0");
        myAudioMixer.SetFloat(masterVol, Mathf.Log10(masterVolume) * 20);
    }

    public void GameVolumeSlider()
    {
        float gameVolume = mainGameVolumeSlider.value;
        gameMusicValueText.text = gameVolume.ToString("0.0");
        myAudioMixer.SetFloat(gameMusicVol, Mathf.Log10(gameVolume) * 20);
    }

    public void SFXVolumeSlider()
    {
        float sfxVolume = sfxVolumeSlider.value;
        sfxValueText.text = sfxVolume.ToString("0.0");
        myAudioMixer.SetFloat(sfxVol, Mathf.Log10(sfxVolume) * 20);
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
            Debug.Log("Anti is: " + QualitySettings.antiAliasing);
        }
        else if (antiAliasingDropdown.value == 1)
        {
            QualitySettings.antiAliasing = 2;
            Debug.Log("Anti is: " + QualitySettings.antiAliasing);
        }
        else if (antiAliasingDropdown.value == 2)
        {
            QualitySettings.antiAliasing = 4;
            Debug.Log("Anti is: " + QualitySettings.antiAliasing);
        }
        else if (antiAliasingDropdown.value == 3)
        {
            QualitySettings.antiAliasing = 8;
            Debug.Log("Anti is: " + QualitySettings.antiAliasing);
        }
    }

    public void SetGraphicsQuality()
    { 
        if (qualityDropdown.value == 0)
        {
            QualitySettings.SetQualityLevel(0);
        }
        else if (qualityDropdown.value == 1)
        {
            QualitySettings.SetQualityLevel(1);
        }
        else if (qualityDropdown.value == 2)
        {
            QualitySettings.SetQualityLevel(2);
        }
        else if (qualityDropdown.value == 3)
        {
            QualitySettings.SetQualityLevel(3);
        }
        else if (qualityDropdown.value == 4)
        {
            QualitySettings.SetQualityLevel(4);
        }
        else if (qualityDropdown.value == 5)
        {
            QualitySettings.SetQualityLevel(5);
        }
    }
}
