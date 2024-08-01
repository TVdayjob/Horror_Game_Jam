using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public GameObject currentObj;
    public GameObject nextObj;
    public string sceneToLoad;

    public Animator transition;

    private float transitionTime = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered checkpoint trigger.");
            CheckpointController checkpointCont = other.GetComponent<CheckpointController>();
            if (checkpointCont != null)
            {
                if (string.IsNullOrEmpty(sceneToLoad))
                {
                    // Set checkpoint only if there's no scene change
                    checkpointCont.SetCheckpoint(transform);
                }
                else
                {
                    StartCoroutine(LoadScene());
                }
            }
            // Disable current artwork
            if (currentObj != null)
            {
                currentObj.SetActive(false);
            }

            // Enable next artwork
            if (nextObj != null)
            {
                nextObj.SetActive(true);
            }
        }
    }

    private IEnumerator LoadScene()
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime); 

        SceneManager.LoadScene(sceneToLoad);
    }
}
