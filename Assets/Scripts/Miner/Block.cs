using UnityEngine;

public class Block : MonoBehaviour
{
    const float MinGoldPercent = 0.5f; // нижняя граница = 50% от потолка руды

    [Header("Data of this ore")]
    public OreData oreConfig;

    [SerializeField] private Sprite[] damageStages;
    [SerializeField] private SpriteRenderer damageRenderer;

    private int currentHealth;

    void Start()
    {
        currentHealth = oreConfig.maxHealth;
        UpdateSprite();
    }

    public void TakeDamage(int damage, InventoryManager playerInventory, Vector3 hitPoint)
    {
        currentHealth -= damage;
        UpdateSprite();

        // Число выпадает один раз: его же начисляем и показываем
        GetGoldRange(out int min, out int max);
        int gold = Random.Range(min, max + 1); // у int верхняя граница не включается

        playerInventory.AddGold(gold);
        FloatingTextSpawner.Instance.Spawn(gold, min, max, hitPoint);

        if (currentHealth <= 0) Destroy(gameObject);
    }

    // Потолок берём из SO руды, нижняя граница — фиксированная доля от него
    private void GetGoldRange(out int min, out int max)
    {
        max = oreConfig.goldReward;
        min = Mathf.Min(max, Mathf.Max(1, Mathf.RoundToInt(max * MinGoldPercent)));
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