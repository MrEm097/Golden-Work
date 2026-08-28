using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Data of this ore")]
    public OreData oreConfig;

    private int currentHealth;

    void Start()
    {
        currentHealth = oreConfig.maxHealth;
    }

    public void TakeDamage(int damage, InventoryManager playerInventory)
    {
        currentHealth -= damage;
        Debug.Log($"Руда получила урон, ее HP {currentHealth}");
        
        if (currentHealth <= 0)
        {
            playerInventory.AddGold(oreConfig.goldReward);
            Destroy(gameObject);
        }
    }
}
