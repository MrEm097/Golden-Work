using UnityEngine;

[CreateAssetMenu(fileName = "NewOreData", menuName = "Ore Data")]
public class OreData : ScriptableObject
{
    [Header("Ore setup")]
    public string oreName; // Имя епт
    public int maxHealth; // Здоровье
    public int goldReward; // Стоимость
}
