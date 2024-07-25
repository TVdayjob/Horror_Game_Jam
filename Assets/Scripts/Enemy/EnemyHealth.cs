using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("Health Bar Settings")]
    [SerializeField] private GameObject healthBarPrefab;
    private Transform healthBarTransform;
    private Vector3 initialHealthBarScale;

    void Start()
    {
        currentHealth = maxHealth;
        InitializeHealthBar();
        UpdateHealthBar();
    }

    private void InitializeHealthBar()
    {
        if (healthBarPrefab != null)
        {
            // Instantiate the health bar as a child of the enemy
            GameObject healthBarInstance = Instantiate(healthBarPrefab, transform);
            healthBarTransform = healthBarInstance.transform;

            // Position the health bar above the enemy
            healthBarTransform.localPosition = new Vector3(0, 2, 0); // Adjust as needed

            // Store the initial scale of the health bar
            initialHealthBarScale = healthBarTransform.localScale;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        //Debug.Log(gameObject.name + " took damage: " + damage + ", current health: " + currentHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarTransform != null)
        {
            float healthPercentage = currentHealth / maxHealth;
            Vector3 newScale = initialHealthBarScale;
            newScale.x *= healthPercentage;
            healthBarTransform.localScale = newScale;
            //Debug.Log("Health bar updated. Current health: " + currentHealth);
        }
    }

    private void Die()
    {
        // Handle enemy death here (e.g., play animation, destroy game object)
        //Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}
