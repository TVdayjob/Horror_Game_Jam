using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    public GameObject currentObj;
    public GameObject nextObj;
    public string sceneToLoad;
    public string nextTask;
    public TaskManager taskManager;
    public Animator transition;
    public float transitionTime = 2f;

    public string requiredItem; // Leave empty if no item is required
    public TextMeshProUGUI messageText;
    public string checkpointMessage;

    private void Start()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered checkpoint trigger.");
            DisplayMessage(checkpointMessage);

            Inventory playerInventory = other.GetComponent<Inventory>();
            taskManager = other.GetComponent<TaskManager>();
            if (playerInventory != null)
            {
                bool hasRequiredItem = string.IsNullOrEmpty(requiredItem) || playerInventory.HasItem(requiredItem);

                if (hasRequiredItem)
                {
                    Debug.Log("Player has required item: " + hasRequiredItem);
                    if (string.IsNullOrEmpty(sceneToLoad))
                    {
                        // Set checkpoint only if there's no scene change
                        CheckpointController checkpointCont = other.GetComponent<CheckpointController>();
                        if (checkpointCont != null)
                        {
                            checkpointCont.SetCheckpoint(transform);
                        }
                    }
                    else
                    {
                        StartCoroutine(LoadScene());
                    }

                    // Update the task if TaskManager is assigned
                    if (taskManager != null)
                    {
                        taskManager.UpdateTask(nextTask);
                    }

                    // Disable current artwork
                    if (currentObj != null)
                    {
                        currentObj.SetActive(false);
                    }

                    // Enable next artwork
                    if (nextObj != null)
                    {
                        Debug.Log("Activating next object.");
                        nextObj.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("Next object is not assigned.");
                    }
                }
                else
                {
                    DisplayMessage("You need the " + requiredItem + " to proceed.");
                }
            }
            else
            {
                Debug.LogWarning("Player inventory not found.");
            }
        }
    }

    private void DisplayMessage(string message)
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = message;
            StartCoroutine(ClearMessageAfterDelay(3f)); // Adjust delay if needed
        }
        else
        {
            Debug.LogWarning("messageText is not assigned.");
        }
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (messageText != null)
        {
            messageText.text = "";
            messageText.gameObject.SetActive(false);
        }
    }

    private IEnumerator LoadScene()
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
