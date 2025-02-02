using System.Collections;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("TYPES")]
    public int currentTaskIndex = 0;

    public string[] tasks =
    {
        "Find the key to the locked room.",
        "Something feels off… Get a drink from the kitchen.",
        "Hunger gnaws at you… Find something to eat.",
        "Where’s your phone? Find it and call for help.",
        "the ritual is incomplete… Place the cursed objects. (   / 3)",
        "Find the key to escape. But will it open the door?",
        "Get out… before it’s too late.",
    };

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI taskText;

    private void Start()
    {
        taskText.text = tasks[currentTaskIndex];
    }

    public void CompleteTask()
    {
        if (currentTaskIndex < tasks.Length)
        {
            UpdateTaskUI();
        }
    }

    public void UpdateTaskUI()
    {
        currentTaskIndex++;
        taskText.text = tasks[currentTaskIndex];
    }
}
