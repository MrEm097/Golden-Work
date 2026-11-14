using UnityEngine;

public class MinerDigging : MonoBehaviour
{
    [Header("Digging parameteres")]
    public int damageHit; // Урон епт
    public float speedAttack; // Задержка в ударе
    private float nextAttackTime = 0f; // Время след. удара

    [Header("Animation timing")]
    public float mineClipLength = 0.5f; // Длина клипа анимации добычи

    [Header("Animation limits")]
    public float maxAnimSpeed = 2f;   // выше этой скорости замах плохо читается; подберите на глаз
    float damageCarry;                // накопление дробного урона

    void Update()
    {
        if (PauseMenu.IsPaused) return; // Если игра на паузе, то выходим из метода

        damageHit = G.Instance.Inventory.toolDamage.damage;

        speedAttack = G.Instance.Inventory.toolSpeedAttack.speedAttack;
    }

    private void OnTriggerStay2D(Collider2D other) // Если мы находимся в коллайдере блока
    {
        Block block = other.GetComponent<Block>();
        if (block == null) return;
        if (!Input.GetMouseButton(0)) return; // Если не нажата левая кнопка мыши, то выходим из метода
        if (Time.time < nextAttackTime) return; // Если время следующей атаки еще не наступило, то выходим из метода

        float minSwingTime = mineClipLength / maxAnimSpeed;
        float swingTime = Mathf.Max(speedAttack, minSwingTime);

        nextAttackTime = Time.time + swingTime;

        float animSpeed = Mathf.Clamp(mineClipLength / swingTime, 0.5f, maxAnimSpeed);

        // всё, что «не влезло» в анимацию, превращаем в урон
        float hitMultiplier = speedAttack > 0f ? swingTime / speedAttack : 1f;
        damageCarry += damageHit * hitMultiplier;
        int dmg = Mathf.FloorToInt(damageCarry);
        damageCarry -= dmg;

        MinerAnimation miner = G.Instance.Inventory.minerAnimation;
        Vector3 point = other.bounds.center;

        miner.PlayMine(animSpeed, () => DoHit(block, point, dmg));
    }

    void DoHit(Block block, Vector3 point, int dmg)
    {
        if (block == null) return;

        block.TakeDamage(damageHit, G.Instance.Inventory);

        MinerAnimation miner = G.Instance.Inventory.minerAnimation;
        if (miner.Current != null)
            G.Instance.Audio.PlayRandom(miner.Current.hitSounds, 0.8f);

        GameObject fx = miner.Current != null ? miner.Current.hitEffectPrefab : null;
        if (fx != null)
            Instantiate(fx, point, Quaternion.identity);
    }
}
