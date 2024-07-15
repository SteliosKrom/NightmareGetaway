using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;


public class MainMenuUIManager : MonoBehaviour
{
    private readonly float movementSpeed = 1f;
    private readonly float movementRange = 0.5f; 
    private Vector3 initialPos;

    [Header("CLASSES")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button audioCategoryButton;
    [SerializeField] private Button videoCategoryButton;
    [SerializeField] private Button graphicsCategoryButton;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject audioButton;
    [SerializeField] private GameObject videoButton;
    [SerializeField] private GameObject graphicsButton;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject videoMenu;
    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject backToMenu;
    [SerializeField] private GameObject backToGame;
    [SerializeField] private GameObject dot;

    [SerializeField] private Camera secondaryCamera;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private AudioSource hoverAudioSource;
    [SerializeField] private AudioSource mainMenuAudioSource;
    [SerializeField] private AudioSource MainGameAudioSource;
    [SerializeField] private AudioClip hoverAudioClip;


    private void Start()
    {
        Time.timeScale = 1f;
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        audioMenu.SetActive(false);
        videoMenu.SetActive(false);
        graphicsMenu.SetActive(false);
        secondaryCamera.enabled = true;
        mainCamera.enabled = false;
        mainMenuAudioSource.Play();
        MainGameAudioSource.Stop();
        dot.SetActive(false);
        initialPos = secondaryCamera.transform.position;
    }

    private void Update()
    {
        if (secondaryCamera.enabled)
        {
            float offsetX = Mathf.Sin(Time.time * movementSpeed) * movementRange;
            float offsetY = Mathf.Sin(Time.time * movementSpeed) * movementRange;
            float offsetZ = Mathf.Sin(Time.time * movementSpeed) * movementRange;
            secondaryCamera.transform.position = initialPos + new Vector3(-offsetX, -offsetY, offsetZ);
        }
    }

    public void PlayButton()
    {
        Time.timeScale = 1f;
        mainMenu.SetActive(false);
        secondaryCamera.enabled = false;
        mainCamera.enabled = true;
        MainGameAudioSource.Play();
        mainMenuAudioSource.Stop();
        MainGameAudioSource.volume = 0.2f;
        dot.SetActive(true);
        RoundManager.instance.currentState = GameState.playing;
    }

    public void ControlsButton()
    {
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
        RoundManager.instance.currentState = GameState.onMainMenu;
    }

    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
        audioButton.SetActive(true);
        videoButton.SetActive(true);
        graphicsButton.SetActive(true);
        audioMenu.SetActive(false);
        videoMenu.SetActive(false);
        graphicsMenu.SetActive(false);
        mainMenu.SetActive(false);
        backToGame.SetActive(false);
        backToMenu.SetActive(true);
        RoundManager.instance.currentState = GameState.onSettings;
    }

    public void ExitButton()
    {
        Application.Quit();
        EditorApplication.ExitPlaymode();
    }

    public void BackToMenu()
    {
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        audioButton.SetActive(false);
        videoButton.SetActive(false);
        graphicsButton.SetActive(false);
        RoundManager.instance.currentState = GameState.onMainMenu;
    }

    public void BackToPrevious()
    {
        audioButton.SetActive(true);
        videoButton.SetActive(true);
        graphicsButton.SetActive(true);
        mainMenu.SetActive(false) ;
        audioMenu.SetActive(false);
        videoMenu.SetActive(false);
        graphicsMenu.SetActive (false);
        RoundManager.instance.currentState = GameState.onMainMenu;
    }

    public void AudioCategoryButton()
    {
        audioMenu.SetActive(true);
        audioButton.SetActive(false);
        videoButton.SetActive(false);
        graphicsButton.SetActive(false);
    }

    public void VideoCategoryButton()
    {
        videoMenu.SetActive(true);
        videoButton.SetActive(false);
        graphicsButton.SetActive(false);
        audioButton.SetActive(false);
    }

    public void GraphicsCategoryButton()
    {
        graphicsMenu.SetActive(true);
        graphicsButton.SetActive(false);
        videoButton.SetActive(false);
        audioButton.SetActive(false);
    }

    public void HoverSoundEffect()
    {
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }
}
