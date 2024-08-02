using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public string itemName;

    public void PickUp()
    {
        gameObject.SetActive(false);
    }

    public void Drop(Vector3 dropPosition)
    {
        gameObject.SetActive(true);
        transform.position = dropPosition;
    }

    public void Interact(Transform interacterTransform)
    {
        PickUp();
    }

    public string getInteractText()
    {
        return "Pick up " + itemName;
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }
}
