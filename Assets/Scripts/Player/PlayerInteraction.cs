using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactionDistance);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(transform);
                }
            }
        }
    }

    public IInteractable getNPC()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactionDistance);
        foreach (Collider collider in colliderArray) {
            if(collider.TryGetComponent(out IInteractable interactable))
            {
                return interactable;
            }
        }
        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Camera.main.transform.position, interactionDistance);
    }
}