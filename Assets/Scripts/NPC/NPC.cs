using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    [HideInInspector]public float health = 100f;
    public string interactText;
    public string[] dialogues;
    public DialogueSystem dialogueSystem;
    [HideInInspector]public bool isStarted = false;
    public float rotationSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Interact(Transform interacterTransform)
    {
        Vector3 direction = (interacterTransform.position - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction * Time.deltaTime * rotationSpeed);

        if (!isStarted)
        {
            dialogueSystem.StartDialogue(dialogues);
            //trigger animation
            // play sound
            isStarted = true;
        }
        else if (!dialogueSystem.isDone)
        {
            dialogueSystem.DisplayNextSentence();
        }
        else
        {
            isStarted = false;
        }

    }

    public string getInteractText()
    {
        return interactText;
    }

    public void EndInteraction()
    {
        isStarted = false;
    }

    public void UpdateDialogues(string[] newDialogues)
    {
        dialogues = newDialogues;
    }
}
