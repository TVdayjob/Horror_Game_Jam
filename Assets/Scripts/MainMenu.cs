using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;

    private float transitionTime = 1f;

    public void SceneChange(string newScene)
    {
        StartCoroutine(loadScene(newScene));
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application Exited");
    }

    public void StartNewGame()
    {
        StartCoroutine(loadScene("Prologue"));
    }

    IEnumerator loadScene(string newScene)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(newScene);
    }
}
