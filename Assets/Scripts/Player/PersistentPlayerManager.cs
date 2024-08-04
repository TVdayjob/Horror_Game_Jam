using UnityEngine;

public class PersistentPlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField]private GameObject playerInstance;

    private static PersistentPlayerManager instance;

    public static PersistentPlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PersistentPlayerManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("PersistentPlayerManager");
                    instance = go.AddComponent<PersistentPlayerManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Destroying duplicate PersistentPlayerManager.");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        InitializePlayer();
        DontDestroyOnLoad(playerInstance);
        Debug.Log("PersistentPlayerManager initialized.");
    }

    private void InitializePlayer()
    {
        if (playerInstance == null && playerPrefab != null)
        {
            Debug.Log("Instantiating player prefab.");
            playerInstance = Instantiate(playerPrefab);
            playerInstance.tag = "Player"; // Ensure the player prefab has the "Player" tag
        }
    }

    public GameObject GetPlayerInstance()
    {
        return playerInstance;
    }
}
