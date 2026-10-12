using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Data of this ore")]
    public OreData oreConfig;

    [SerializeField] private Sprite[] damageStages;
    [SerializeField] private SpriteRenderer damageRenderer;

    private int currentHealth;

    void Start()
    {
        damageRenderer = GetComponent<SpriteRenderer>();

        currentHealth = oreConfig.maxHealth;

        UpdateSprite();
    }

    public void TakeDamage(int damage, InventoryManager playerInventory)
    {
        currentHealth -= damage;
        UpdateSprite();

        if (currentHealth <= 0)
        {
            playerInventory.AddGold(oreConfig.goldReward);
            Destroy(gameObject);
        }
    }

    private void UpdateSprite() // Обновление спрайта в зависимости от текущего здоровья
    {
        if (damageStages.Length == 0) return;

        if (currentHealth >= oreConfig.maxHealth)
        {
            damageRenderer.sprite = null;
            return;
        }

        float hpPercent = (float)currentHealth / oreConfig.maxHealth;

        int stage = Mathf.FloorToInt((1f - hpPercent) * damageStages.Length);
        stage = Mathf.Clamp(stage, 0, damageStages.Length - 1);

        damageRenderer.sprite = damageStages[stage];
    }
}
