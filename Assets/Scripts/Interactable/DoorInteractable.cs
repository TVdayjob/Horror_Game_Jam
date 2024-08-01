using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    public Animator doorAnimator;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        //play animation
    }

    public void Interact(Transform interacterTransform)
    {
        //animate trigger
        //play sound
        Debug.Log("Opened Door");
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }

    public string getInteractText()
    {
        return "INTERACT DOOR";
    }

}
