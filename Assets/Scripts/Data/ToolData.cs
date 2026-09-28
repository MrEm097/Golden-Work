using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Tool Data")]
public class ToolData : ScriptableObject
{
    public enum ItemType { Damage, Speed, SpeedAttack}

    [Header("Tool parameteres")]
    public ItemType toolType;
    public int priority;
    public int cost;
    public int damage;
    public float speedAttack;
    public float speedMove;
}
