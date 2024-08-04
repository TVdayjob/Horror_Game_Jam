using UnityEngine;

public class PersistentPlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private static GameObject playerInstance;

    void Awake()
    {
        if (FindObjectsOfType<PersistentPlayerManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab);
            playerInstance.tag = "Player"; 
        }
        else
        {
            // Optionally
        }
    }
}
