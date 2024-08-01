using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithKeyInteractable : MonoBehaviour ,IInteractable
{
    public string requiredKey; // The key required to open the door
    private bool isOpen = false;
    public Animator doorKeyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        doorKeyAnimator = GetComponent<Animator>();
    }

    public void Interact(Transform interacterTransform)
    {
        Inventory inventory = interacterTransform.GetComponent<Inventory>();

        if (inventory != null && inventory.HasItem(requiredKey))
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("The door is locked. You need the " + requiredKey + " key.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }

    public string getInteractText()
    {
        return "OPEN DOOR WITH KEY";
    }

    private void OpenDoor()
    {
        if (!isOpen)
        {
            //animation, changing position
            Debug.Log("The door is now open.");
            isOpen = true;
        }
    }

}
