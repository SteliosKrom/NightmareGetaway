using UnityEngine;
using TMPro;
using System.Timers;

public class FPScounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public GameObject fps;

    private int frameCount = 0;
    private float elapsedTime = 0f;
    private const float updateInterval = 1f;

    void Update()
    {
        CountFPS();
    }

    public void CountFPS()
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
