using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashUIManager : MonoBehaviour
{
    [Header("SCRIPT REFERENCES")]
    public static SplashUIManager instance;

    [Header("TYPES")]
    private float mainGameSceneDelay = 1f;

    private bool active = true;
    private bool inactive = false;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject gammaPanel;

    [Header("UI")]
    [SerializeField] private Slider gammaSlider;
    [SerializeField] private TextMeshProUGUI gammaValueText;
    [SerializeField] private Button continueButton;

    [Header("POST PROCESSING")]
    [SerializeField] private PostProcessProfile splashProfile;
    [SerializeField] private PostProcessProfile mainMenuProfile;
    [SerializeField] private PostProcessProfile mainGameProfile;

    [SerializeField] private PostProcessLayer splashLayer;
    [SerializeField] private PostProcessLayer mainMenuLayer;
    [SerializeField] private PostProcessLayer mainGameLayer;

    private ColorGrading colorGrading;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("There is no instance of this object");
        }
    }

    private void Start()
    {
        InitializeGamma();
    }

    public void InitializeGamma()
    {
        splashProfile.TryGetSettings(out colorGrading);
        mainMenuProfile.TryGetSettings(out colorGrading);
        mainGameProfile.TryGetSettings(out colorGrading);
    }

    public void ContinueButton()
    {
        StartCoroutine(LoadMainGameDelay());
    }

    public IEnumerator LoadMainGameDelay()
    {
        yield return new WaitForSeconds(mainGameSceneDelay);
        SceneManager.LoadScene("MainGameScene");
    }
}
