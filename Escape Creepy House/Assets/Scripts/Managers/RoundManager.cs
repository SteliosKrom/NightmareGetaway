using System;
using UnityEngine;

public enum GameState
{
    playing,
    pause,
    onSettingsMenu,
    onSettingsGame,
    onMainMenu,
    inIntro,
}

public enum EnvironmentState
{
    outdoors,
    indoors,
    none
}

public enum KeyState
{
    kidsRoomKey,
    garageKey,
    mainDoorKey,
    none
}

public enum KidsDoorState
{
    unlocked,
    locked
}

public enum GarageDoorState
{
    unlocked,
    locked
}

public enum MainDoorState
{
    unlocked,
    locked
}

class RoundManager : MonoBehaviour
{
    [Header("SCRIPT REFERENCES")]
    public static RoundManager instance;

    [Header("GAME STATES")]
    public GameState currentGameState;
    public EnvironmentState currentEnvironmentState;
    public KeyState currentKeyState;
    public KidsDoorState currentKidsDoorState;
    public GarageDoorState currentGarageDoorState;
    public MainDoorState currentMainDoorState;

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

        currentKidsDoorState = KidsDoorState.locked;
        currentGarageDoorState = GarageDoorState.locked;
        currentMainDoorState = MainDoorState.locked;
    }
}


