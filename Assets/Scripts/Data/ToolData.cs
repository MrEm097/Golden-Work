using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Tool Data")]
public class ToolData : ScriptableObject
{
    public enum ItemType { Damage, Speed, SpeedAttack} // 

    [Header("Tool parameteres")]
    public ItemType toolType; // Тип инструмента (урон, скорость атаки, скорость движения)
    public int priority; // Приоритет инструмента (чем выше, тем лучше)
    public int cost; // Цена инструмента
    public int damage; // Урон с этим инструментом
    public float speedAttack; // Скорость атаки с этим инструментом
    public float speedMove; // Скорость движения с этим инструментом


    [Header("Вид на персонаже")]
    public bool showOnMiner;                      // true только для того, что держим в руке
    public Sprite handSprite;                     // как выглядит в руке
    public AnimatorOverrideController animations; // пусто = стандартные анимации
    public Color trailColor = Color.white;        // цвет линий замаха
    public GameObject hitEffectPrefab;            // частицы при ударе
}
