using TMPro;
using UnityEngine;

public class SkipGameIntro : MonoBehaviour
{
    [SerializeField] private MainMenuUIManager mainMenuUIManager;
    private bool active = true;
    private bool inactive = false;

    [Header("AUDIO")]
    [SerializeField] private AudioSource mainGameAudioSource;

    [Header("OTHER")]
    [SerializeField] private GameObject gameIntroPanel;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private TextMeshProUGUI skipText;
    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && RoundManager.instance.currentGameState == GameState.inIntro)
        {

        }
    }
}
