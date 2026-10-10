using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [Header("UI elements")]
    public GameObject shoppingButton;
    public GameObject assortmentPanel;
    public GameObject Door;
    

    private MinerController mController;
    [SerializeField] private TextMeshProUGUI goldText;

    void Start()
    {
        mController = GameObject.Find("Miner").GetComponent<MinerController>();
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

        mController.CanMove = false; // Чтобы перс не двигался при включеной панели

        assortmentPanel.SetActive(true); // Показывает панель покупок
    }

    public void CloseShop()
    {
        assortmentPanel.SetActive(false); // Закрывает панель покупок

        mController.CanMove = true; // Возвращает персу движение
    }

    public void ClosePaidButton(GameObject button)
    {
        button.SetActive(false);
    }

    public void UpdateGoldDisplay(int currentGold)
    {
        if (goldText != null)
        {
            goldText.text = currentGold.ToString();
        }
    }
}
