using UnityEngine;

public class MinerDigging : MonoBehaviour
{
    [Header("Digging parameteres")]
    public int damageHit; // Урон епт
    public float speedAttack; // Задержка в ударе
    private float nextAttackTime = 0f; // Время след. удара

    private void Start()
    {
        damageHit = G.Instance.Inventory.currentTool.damage;

        speedAttack = G.Instance.Inventory.currentTool.speedAttack;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Block block = other.GetComponent<Block>(); // Получаем информацию блока с которым столкнулись

        if (block != null) // Пустой ли он?
        {
            if (Input.GetMouseButton(0)) // Нажата ли ЛКМ?
            {
                if (Time.time >= nextAttackTime) // Прошла ли задержка?
                {
                    block.TakeDamage(damageHit, G.Instance.Inventory); // Наносим урон

                    nextAttackTime = Time.time + speedAttack; 
                }
            }
        }
    }
}
