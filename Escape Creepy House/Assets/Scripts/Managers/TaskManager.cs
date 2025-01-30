using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI taskText;
    private int currentTaskIndex = 0;
    private string[] tasks =
    {
        "Find the key to the locked room.",
        "Something feels off… Get a drink from the kitchen.",
        "Hunger gnaws at you… Find something to eat.",
        "Where’s your phone? Find it and call for help.",
        "The ritual is incomplete… Place the cursed objects.",
        "Find the key to escape. But will it open the door?",
        "Get out… before it’s too late. ",
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
