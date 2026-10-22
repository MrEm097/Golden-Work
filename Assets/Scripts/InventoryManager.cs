using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class ShopSlot
    {
        public ToolData toolData; // Сам предмет
        public Button buyButton; // Кнопка покупки
        public GameObject purchasedUI; // Картинка
    }
    [Header("Shop Setting")]
    public List<ShopSlot> shopSlots;

    [Header("Amount of gold")]
    public int currentGold = 0; // Сколько изначально золота

    [Header("Miner")]
    public MinerAnimation minerAnimation; // Анимация шахтера

    void Start()
    {
        G.Instance.UI.UpdateGoldDisplay(G.Instance.Inventory.currentGold);
    }

    public void AddGold(int amount) // Добавляет в кашелек золото
    {
        currentGold += amount;
        G.Instance.UI.UpdateGoldDisplay(currentGold);
    }


    [Header("Current tool")]
    public ToolData toolDamage;
    public ToolData toolSpeedAttack;
    public ToolData toolSpeed;

    // Главная функция покупки, которую мы привяжем к кнопкам
    public void BuyTool(ToolData selectedTool)
    {
        // 1. Проверяем баланс
        if (currentGold < selectedTool.cost)
        {
            return;
        }

        // 2. Списываем золото
        currentGold -= selectedTool.cost;

        // 3. Сохраняем купленный предмет в зависимости от его типа
        SaveEquippedTool(selectedTool);

        minerAnimation.Equip(selectedTool); // Обновляем анимацию шахтера и меняем спрайт руки на новый инструмент

        // 4. Обновляем UI всего магазина (скрываем старое, показываем купленное)
        UpdateShopUI();

        // 5. Обновляем счетчик золота в углу экрана
        G.Instance.UI.UpdateGoldDisplay(currentGold);
    }

    // Вспомогательный метод: сохраняет предмет по его типу (из твоего первого скрипта)
    private void SaveEquippedTool(ToolData tool)
    {
        if (tool.toolType == ToolData.ItemType.Damage) toolDamage = tool;
        else if (tool.toolType == ToolData.ItemType.Speed) toolSpeed = tool;
        else if (tool.toolType == ToolData.ItemType.SpeedAttack) toolSpeedAttack = tool;
    }

    // Вспомогательный метод: возвращает текущий купленный предмет по типу
    private ToolData GetCurrentToolByType(ToolData.ItemType type)
    {
        if (type == ToolData.ItemType.Damage) return toolDamage;
        if (type == ToolData.ItemType.Speed) return toolSpeed;
        if (type == ToolData.ItemType.SpeedAttack) return toolSpeedAttack;
        return null;
    }

    private void UpdateShopUI()
    {
        foreach (ShopSlot slot in shopSlots)
        {
            // Получаем текущий купленный инструмент ТАКОГО ЖЕ типа, как в этом слоте
            ToolData currentEquipped = GetCurrentToolByType(slot.toolData.toolType);

            if (currentEquipped != null)
            {
                // Если этот предмет УЖЕ куплен ИЛИ его приоритет меньше/равен купленному
                if (slot.toolData == currentEquipped || slot.toolData.priority <= currentEquipped.priority)
                {
                    slot.buyButton.gameObject.SetActive(false); // Прячем кнопку (нажать больше нельзя)
                    if (slot.purchasedUI != null)
                    {
                        slot.purchasedUI.SetActive(true);      // Показываем картинку "Куплено"
                    }
                }
            }
        }
    }
}
