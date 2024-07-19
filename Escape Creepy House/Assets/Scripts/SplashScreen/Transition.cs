using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public GameObject text;

    private void Awake()
    {
        text.SetActive(false);
    }

    private void Start()
    {
        Invoke("LoadText", 7f);
        Invoke("LoadMainGameScene", 10f);
    }

    public void LoadMainGameScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void LoadText()
    {
        text.SetActive(true);
    }
}
