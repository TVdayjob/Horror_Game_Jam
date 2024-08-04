using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public Transform playerSpawnPoint;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded");  
        PositionPlayer();
    }

    void PositionPlayer()
    {
        Debug.Log("Looking for player");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Debug.Log("Found player");
            if (playerSpawnPoint != null)
            {
                Debug.Log("Positioning player");
                player.transform.position = playerSpawnPoint.position;
                player.transform.rotation = playerSpawnPoint.rotation;
            }
            else
            {
                Debug.LogWarning("PlayerSpawnPoint not found.");
            }
        }
        else
        {
            Debug.LogWarning("Player not found.");
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
