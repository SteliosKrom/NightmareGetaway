using UnityEngine;

public enum GameState
{
    playing,
    pause,
    onSettings,
    onMainMenu,
}

class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    [Header("GAME STATES")]
    public GameState currentState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Game object" + name + "is not found");
            return;
        }
    }

    private void Start()
    {
        currentState = GameState.onMainMenu;
    }
}


