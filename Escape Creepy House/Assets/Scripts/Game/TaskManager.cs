using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI taskText;
    private int currentTaskIndex = 0;
    private string[] tasks =
    {
        "Find the room key!",
        "Go to the kitchen and drink water!",
        "Search for food in the kitchen!",
        "Find out your mobile phone! First, look for the garage key outdoors.",
        "Search for the main door key!",
        "Escape the house!",
    };

    private void Start()
    {
        UpdateTaskUI();
    }

    public void CompleteTask()
    {
        currentTaskIndex++;

        if (currentTaskIndex < tasks.Length)
        {
            UpdateTaskUI();
        }
        else
        {
            Debug.Log("All tasks completed!");
            taskText.text = "All tasks completed!";
        }
    }

    public void UpdateTaskUI()
    {
        taskText.text = tasks[currentTaskIndex];
    }
}
