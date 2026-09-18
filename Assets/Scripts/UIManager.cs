using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI elements")]
    public GameObject shoppingButton;
    public GameObject assortmentPanel;
    public GameObject Door;


    private MinerController mController;
    private float currentSpeed; 

    void Start()
    {
        mController = GameObject.Find("Miner").GetComponent<MinerController>();
        currentSpeed = mController.speed; // Запоминаем текущую скорость
    }

    public void ShowShoppingDoor(bool condition)
    {
        Door.SetActive(condition);
    }
    public void ShowShoppingButton(bool condition)
    {
        shoppingButton.SetActive(condition); // Показывает кнопку Shopping при столкновении с магазом
    }

    public void ShowShoppingPanel()
    {
        shoppingButton.SetActive(false); // Скрывает кнопку Shopping

        mController.speed = 0f; // Чтобы перс не двигался при включеной панели

        assortmentPanel.SetActive(true); // Показывает панель покупок
    }

    public void CloseShop()
    {
        assortmentPanel.SetActive(false); // Закрывает панель покупок

        mController.speed = currentSpeed; // Возвращает персу движение
    }
}
