using System;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Serializable] // Позволяет видеть в инспекторе
    public struct ToolSlot // Струкура инструментов
    {
        public ToolData toolConfig; // Подключаю SO инструмента (проще говоря конкретные статы)

    }

    [Header("List tools")]
    public ToolSlot[] toolSlots; // Список всех подключеных инструментов

    public void BuyTool(ToolData toolSlots)
    {

    }
}

// Тяжело нахуй