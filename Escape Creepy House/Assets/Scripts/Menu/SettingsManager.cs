using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("TYPES")]
    private const string masterVolume = "MasterVolume";
    private const string gameMusicVolume = "GameMusicVolume";
    private const string sfxVolume = "SoundEffectsVolume";

    [Header("CLASSES")]
    [SerializeField] private AudioMixer myAudioMixer;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider mainGameVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    //Values that are displayed at the right of the volume sliders
    [SerializeField] private TextMeshProUGUI masterValueText; 
    [SerializeField] private TextMeshProUGUI gameMusicValueText;
    [SerializeField] private TextMeshProUGUI sfxValueText;  

    public void MasterVolumeSlider(float masterVolume)
    {
        masterVolume = masterVolumeSlider.value;
        masterValueText.text = masterVolume.ToString("0.0");
    }

    public void GameVolumeSlider(float gameVolume)
    {
        gameVolume = mainGameVolumeSlider.value;
        gameMusicValueText.text = gameVolume.ToString("0.0");
    }
}
