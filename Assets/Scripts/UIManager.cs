using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI elements")]
    public GameObject shoppingButton;
    public GameObject assortmentPanel;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void ShowShoppingButton(bool condition)
    {
        shoppingButton.SetActive(condition);
    }

}
