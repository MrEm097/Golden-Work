using System.Collections;
using UnityEngine;

public class MinerDigging : MonoBehaviour
{
    [Header("Digging parameteres")]
    public int damageHit; // Урон епт
    public float speedAttack; // Задержка в ударе
    private float nextAttackTime = 0f; // Время след. удара

    [Header("Animation timing")]
    public float mineClipLength = 0.5f; // Длина клипа анимации добычи
    public float contactTime = 0.3f; // Время контакта с блоком, после которого можно наносить урон

    const float MineClipLength = 0.5f;


    void Update()
    {
        damageHit = G.Instance.Inventory.toolDamage.damage;

        speedAttack = G.Instance.Inventory.toolSpeedAttack.speedAttack;
    }

    private void OnTriggerStay2D(Collider2D other) // Если мы находимся в коллайдере блока
    {
        Block block = other.GetComponent<Block>();
        if (block == null) return;
        if (!Input.GetMouseButton(0)) return; // Если не нажата левая кнопка мыши, то выходим из метода
        if (Time.time < nextAttackTime) return; // Если время следующей атаки еще не наступило, то выходим из метода

        nextAttackTime = Time.time + speedAttack;

        MinerAnimation miner = G.Instance.Inventory.minerAnimation;
        float animSpeed = speedAttack > 0f ? Mathf.Clamp(mineClipLength / speedAttack, 0.5f, 3f) : 1f;
        miner.PlayMine(animSpeed);

        // урон и эффект произойдут в момент касания, а не в момент клика
        StartCoroutine(HitAfter(block, other.bounds.center, contactTime / animSpeed));
    }

    IEnumerator HitAfter(Block block, Vector3 point, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (block == null) yield break;

        block.TakeDamage(damageHit, G.Instance.Inventory);

        MinerAnimation miner = G.Instance.Inventory.minerAnimation;
        GameObject fx = miner.Current != null ? miner.Current.hitEffectPrefab : null;
        if (fx != null)
            Instantiate(fx, point, Quaternion.identity);
    }
}
