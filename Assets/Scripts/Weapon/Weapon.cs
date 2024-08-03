using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private float baseDamage = 25f;
    [SerializeField] public float pickupRange = 2f;
    [SerializeField] public bool isPickedUp = false;
    [SerializeField] public bool isSelected = false;

    public GameObject player;

    public string weaponName;

    public float currentDamage;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        currentDamage = baseDamage; // Initialize current damage
    }

    private void Update()
    {
        // Debug.Log(playerMovement.isAttacking);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerMovement.isAttacking && isPickedUp)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(currentDamage);
                Debug.Log("Hit enemy with weapon!");
            }
        }
    }

    public void PickUp()
    {
        isPickedUp = true;
        gameObject.SetActive(false);
    }

    public float GetPickupRange()
    {
        return pickupRange;
    }

    public void IncreaseDamage(float amount)
    {
        currentDamage += amount;
    }
}
