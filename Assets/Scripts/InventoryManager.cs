using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Amount of gold")]
    public int currentGold = 0;

    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log($"Заебись {currentGold}");
    }
}
