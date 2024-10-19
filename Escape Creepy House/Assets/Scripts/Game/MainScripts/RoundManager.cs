using UnityEngine;

public enum GameState
{
    playing,
    pause,
    onSettingsMenu,
    onSettingsGame,
    onMainMenu,
    inIntro
}

public enum EnvironmentState
{
    outdoors,
    indoors,
    inRoom,
    outRoom,
    none
}

public enum KeyState
{
    kidsRoomKey,
    garageKey,
    mainDoorKey,
    none
}

class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    [Header("GAME STATES")]
    public GameState currentGameState;
    public EnvironmentState currentEnvironmentState;
    public KeyState currentKeyState;

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
        currentGameState = GameState.onMainMenu;
        currentEnvironmentState = EnvironmentState.none;
        currentKeyState = KeyState.none;
    }
}


