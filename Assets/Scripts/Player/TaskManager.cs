using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public TextMeshProUGUI taskText;

    public string currentTask;

    void Start()
    {
        // Set the initial task (example)
        UpdateTask("Find the key to unlock the door.");
    }

    public void UpdateTask(string newTask)
    {
        currentTask = newTask;
        taskText.text = "Next Task: " + currentTask;
    }
}
