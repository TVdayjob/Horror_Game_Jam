using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private float damage = 25f;
    [SerializeField] public float pickupRange = 2f;
    [SerializeField] public bool isPickedUp = false;

    private PlayerMovement playerMovement;

    private void Start()
    {
        // Assume the weapon is a child of the player, so we get the PlayerMovement from the parent
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerMovement != null && playerMovement.isAttacking && isPickedUp)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Hit enemy with weapon!");
            }
        }
    }

    public void PickUp()
    {
        isPickedUp = true;
        gameObject.SetActive(false); // Deactivate the weapon in the scene
        Debug.Log("picked up");
    }

    public float GetPickupRange()
    {
        return pickupRange;
    }
}
