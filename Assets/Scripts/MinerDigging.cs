using UnityEngine;

public class MinerDigging : MonoBehaviour
{
    [Header("Digging parameteres")]
    public int damageHit = 10; // Урон епт
    public float attackCooldown = 1.0f; // Задержка в ударе

    private InventoryManager inventory;
    private float nextAttackTime = 0f;

    void Start()
    {
        inventory = GetComponent<InventoryManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Block block = other.GetComponent<Block>();

        if (block != null) 
        {
            if (Input.GetMouseButton(0))
            {
                if (Time.time >= nextAttackTime) // Прошла ли задержка?
                {
                    block.TakeDamage(damageHit, inventory); // Наносим урон

                    nextAttackTime = Time.time + attackCooldown; // Время след. удара
                }
            }
        }
    }
}
