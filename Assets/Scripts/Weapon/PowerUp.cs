using UnityEngine;

public class PowerUp : MonoBehaviour, IInteractable
{
    public string powerUpName;
    public float damageBoost = 10f; // Amount of damage increase

    public void Interact(Transform interacterTransform)
    {
        //PlayerMovement playerMovement = interacterTransform.GetComponent<PlayerMovement>();
        //if (playerMovement != null)
        //{
        //    playerMovement.ApplyPowerUp(this);
        //    Destroy(gameObject); 
        //}
    }

    public string getInteractText()
    {
        return "Pick up " + powerUpName;
    }

    public void PickUp()
    {
        gameObject.SetActive(false); 
    }

    public void EndInteraction()
    {
        // Not implemented
    }
}
