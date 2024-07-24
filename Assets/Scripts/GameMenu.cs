using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public GameObject pauseMenuUI;

    [HideInInspector] public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("Player not found!");
        }

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false); 
        }

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth reference not set in the Inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void PauseGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; 
        isPaused = true;
    }

    public void RestartFromCheckpoint()
    {
        playerHealth.Die();
    }

    public void SceneChange(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application Exited");
    }
}
