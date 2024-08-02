using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 2f;
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

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
                    if (collider.TryGetComponent(out Item item))
                    {
                        inventory.AddItem(item);
                        Debug.Log("Picked up: " + item.itemName);
                    }
                }
                else if (collider.TryGetComponent(out Weapon weapon))
                {
                    inventory.AddWeapon(weapon);
                    Debug.Log("Picked up: " + weapon.weaponName);
                }
            }
        }
    }

    public IInteractable GetInteractable()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactionDistance);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                return interactable;
            }
        }
        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}