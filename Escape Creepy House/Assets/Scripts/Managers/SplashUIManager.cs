using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashUIManager : MonoBehaviour
{
    [Header("TYPES")]
    private float mainGameSceneDelay = 1f;

    private bool active = true;
    private bool inactive = false;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject gammaPanel;

    [Header("BUTTON")]
    [SerializeField] private Button continueButton;

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
