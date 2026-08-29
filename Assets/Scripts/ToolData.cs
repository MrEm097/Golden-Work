using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Tool Data")]
public class ToolData : ScriptableObject
{
    [Header("Tool parameteres")]
    public string toolName;
    public int cost;
    public int damage;
    public float speedAttack;
    public float speedMove;
}
