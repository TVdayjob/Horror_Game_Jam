using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject floorPrefab; // Assign the floor prefab in the Inspector
    public Transform player; // Assign the player transform in the Inspector
    public float floorHeight; // Height of each floor
    private GameObject[] floors = new GameObject[3];
    private int currentFloorIndex;

    void Start()
    {
        // Initialize two floors: one at the player's level and one above
        floors[0] = Instantiate(floorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        floors[1] = Instantiate(floorPrefab, new Vector3(0, floorHeight, 0), Quaternion.identity);
        currentFloorIndex = 0;
    }

    void Update()
    {
        float playerY = player.position.y;
        float playerX = player.position.x;

        // Check if the player moved up a floor
        if (playerY > floors[currentFloorIndex].transform.position.y + floorHeight/2 && playerX > 1.3)
        {
            MoveFloorsUp();
        }
        // Check if the player moved down a floor
        else if (floors[0] != null && currentFloorIndex > 0 && playerY < floors[currentFloorIndex-1].transform.position.y + floorHeight && playerX <= 1.3)
        {
            MoveFloorsDown();
        }
        // Debug.Log("below floor height:" + floors[currentFloorIndex - 1].transform.position.y);
    }

    void MoveFloorsUp()
    {
        // Destroy the bottom floor if it exists
        if (currentFloorIndex == 0)
        {
            floors[2] = Instantiate(floorPrefab, new Vector3(0, floors[1].transform.position.y + floorHeight, 0), Quaternion.identity);
            currentFloorIndex++;
        }
        else
        {
            Destroy(floors[0]);

            // Move the middle floor to the bottom position
            floors[0] = floors[1];
            floors[1] = floors[2];

            // Create a new floor above the current top floor
            Vector3 newPosition = floors[2].transform.position + new Vector3(0, floorHeight, 0);
            floors[2] = Instantiate(floorPrefab, newPosition, Quaternion.identity);
        }
    }

    void MoveFloorsDown()
    {
        // Destroy the top floor if it exists
        Destroy(floors[2]);

        // Move the middle floor to the top position
        floors[2] = floors[1];
        floors[1] = floors[0];

        // Check if the new floor's Y position is non-negative before creating it
        Vector3 newPosition = floors[0].transform.position - new Vector3(0, floorHeight, 0);
        if (newPosition.y >= 0)
        {
            floors[0] = Instantiate(floorPrefab, newPosition, Quaternion.identity);
        }
        else
        {
            floors[0] = null; // No floor below
        }
    }
}