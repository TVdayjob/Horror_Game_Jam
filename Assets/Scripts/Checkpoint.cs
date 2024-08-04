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
    public string checkpointMessage;

    public NPC npc;
    public string[] newDialogues;

    private CheckpointController checkpointController;

    private void Start()
    {
        // Ensure the messageText is hidden at the start
        CheckpointController checkpointController = FindObjectOfType<CheckpointController>();
        if (checkpointController != null && checkpointController.checkpointMessageText != null)
        {
            checkpointController.checkpointMessageText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered checkpoint trigger.");

            checkpointController = other.GetComponent<CheckpointController>();
            if (checkpointController != null)
            {
                // Set the checkpoint and message
                checkpointController.SetCheckpoint(transform, checkpointMessage);
            }

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
                        // No scene change, just set the checkpoint
                        if (checkpointController != null)
                        {
                            checkpointController.SetCheckpoint(transform, checkpointMessage);
                            StartCoroutine(ClearMessageAfterDelay(3f));
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

                    if (npc != null)
                    {
                        npc.UpdateDialogues(newDialogues);
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
        if (checkpointController != null && checkpointController.checkpointMessageText != null)
        {
            checkpointController.checkpointMessageText.text = message;
            //checkpointController.checkpointMessageText.gameObject.SetActive(true);
            StartCoroutine(ClearMessageAfterDelay(3f));
        }
        else
        {
            Debug.LogWarning("CheckpointController or checkpointMessageText is not assigned.");
        }
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        Debug.Log("Clearing message!!!!!");
        yield return new WaitForSeconds(delay);
        if (checkpointController != null && checkpointController.checkpointMessageText != null)
        {
            checkpointController.checkpointMessageText.text = "";
            checkpointController.checkpointMessageText.gameObject.SetActive(false);
        }
    }

    private IEnumerator LoadScene()
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
