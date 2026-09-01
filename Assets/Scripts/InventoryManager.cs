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
    public ToolData currentTool;

    public void UpgradeTool(ToolData newTool)
    {
        currentTool = newTool;
    }
}
