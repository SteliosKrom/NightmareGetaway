using UnityEngine;
using TMPro;

public class FPScounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public GameObject fps;

    public int frameCount = 0;
    public float elapsedTime = 0f;
    private const float updateInterval = 1f;

    void Update()
    {
        CountFPS();
    }

    public void CountFPS()
    {
        switch (RoundManager.instance.currentGameState)
        {
            case GameState.playing:
                FPSCalculation();
                break;

            case GameState.pause:
                FPSCalculation();
                break;

            case GameState.onMainMenu:
                FPSCalculation();
                break;

            case GameState.onSettingsMenu:
                FPSCalculation();
                break;

            case GameState.onSettingsGame:
                FPSCalculation();
                break;
        }
    }

    public void FPSCalculation()
    {
        frameCount++;
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= updateInterval)
        {
            float fps = frameCount / elapsedTime;
            fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();

            frameCount = 0;
            elapsedTime = 0f;
        }
    }
}
