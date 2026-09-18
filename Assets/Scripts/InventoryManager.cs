using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Amount of gold")]
    public int currentGold = 0; // Сколько изначально золота

    public void AddGold(int amount) // Добавляет в кашелек золото
    {
        currentGold += amount;
        Debug.Log($"Получено {currentGold}");
    }


    [Header("Current tool")]
    public ToolData toolDamage;
    public ToolData toolAttack;
    public ToolData toolSpeed;
    public ToolData currentTool; // Заглушка

    public void UpgradeTool(ToolData newTool)
    {
        currentTool = newTool; // Замена текущего инстркмента на новое
        currentGold -= newTool.cost; // Списывание денег
    }
}
