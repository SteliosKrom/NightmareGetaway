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

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider mainGameVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [SerializeField] private TextMeshProUGUI masterValueText;
    [SerializeField] private TextMeshProUGUI gameMusicValueText;
    [SerializeField] private TextMeshProUGUI sfxValueText;

    private void Start()
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

    public void MasterVolumeSlider()
    {
        float masterVolume = masterVolumeSlider.value;
        masterValueText.text = masterVolume.ToString("0.0");
        myAudioMixer.SetFloat(masterVol, Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat(masterVol, masterVolume);
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
}
