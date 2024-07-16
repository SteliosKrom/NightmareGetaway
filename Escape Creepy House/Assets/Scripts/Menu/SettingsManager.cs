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

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider mainGameVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [SerializeField] private TextMeshProUGUI masterValueText; 
    [SerializeField] private TextMeshProUGUI gameMusicValueText;
    [SerializeField] private TextMeshProUGUI sfxValueText;

    private void Start()
    {
        myAudioMixer.SetFloat(masterVol, 0f);
        myAudioMixer.SetFloat(gameMusicVol, 0f);
        myAudioMixer.SetFloat(sfxVol, 0f);
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
}
