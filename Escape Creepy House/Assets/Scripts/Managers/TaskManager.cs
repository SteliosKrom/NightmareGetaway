using System.Collections;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("TYPES")]
    public int currentTaskIndex = 0;

    private bool active = true;
    private bool inactive = false;

    private float taskDelay = 5f;

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

    [Header("OTHER")]
    [SerializeField] private Animator taskAnimator;

    private void Start()
    {
        taskText.text = tasks[currentTaskIndex];
    }

    public void CompleteTask()
    {
        if (currentTaskIndex < tasks.Length)
        {
            StartCoroutine(TaskDelay());
        }
    }

    public IEnumerator TaskDelay()
    {
        taskAnimator.SetBool("FadeOut", active);
        taskAnimator.SetBool("FadeIn", inactive);

        yield return new WaitForSeconds(taskDelay);

        currentTaskIndex++;
        taskText.text = tasks[currentTaskIndex];

        taskAnimator.SetBool("FadeIn", active);
        taskAnimator.SetBool("FadeOut", inactive);
    }
}
