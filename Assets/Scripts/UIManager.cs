using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI elements")]
    public GameObject shoppingButton;
    public GameObject assortmentPanel;

    private MinerController mController;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        mController = GameObject.Find("Miner").GetComponent<MinerController>();
    }

    public void ShowShoppingButton(bool condition)
    {
        shoppingButton.SetActive(condition);
    }

    public void ShowShoppingPanel()
    {
        shoppingButton.SetActive(false);

        mController.speed = 0f;

        assortmentPanel.SetActive(true);
    }
}
