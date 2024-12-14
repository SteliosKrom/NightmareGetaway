using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private bool active = true;
    private bool inactive = false;

    private void Update()
    {
        switch (RoundManager.instance.currentGameState)
        {
            case GameState.playing:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = inactive;
                break;

            case GameState.onMainMenu:
            case GameState.onSettingsMenu:
            case GameState.onSettingsGame:
            case GameState.pause:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = active;
                break;
        }
    }
}
