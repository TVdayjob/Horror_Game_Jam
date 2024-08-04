using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public Transform playerSpawnPoint;

    private static SpawnManager instance;

    public static SpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpawnManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("SpawnManager");
                    instance = go.AddComponent<SpawnManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Destroying duplicate SpawnManager.");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        PositionPlayer();
    }

    void PositionPlayer()
    {
        
        GameObject player = PersistentPlayerManager.Instance.GetPlayerInstance();

        if (player != null)
        {
            Debug.Log("Player found.");
            if (playerSpawnPoint == null)
            {
                playerSpawnPoint = GameObject.Find("PlayerSpawnPoint")?.transform;
            }

            if (playerSpawnPoint != null)
            {
                player.transform.position = playerSpawnPoint.position;
                player.transform.rotation = playerSpawnPoint.rotation;
                Debug.Log("Player positioned at: " + player.transform.position);
            }
            else
            {
                Debug.LogWarning("PlayerSpawnPoint not found in the scene.");
            }
        }
        else
        {
            Debug.LogWarning("Player object not found.");
        }
    }
}