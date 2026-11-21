using UnityEngine;

public class TutorialHints : MonoBehaviour
{
    [SerializeField] GameObject oreCross;
    [SerializeField] GameObject shopArrow;
    [SerializeField] int goldForShopHint = 20;

    bool shopHintShown, shopVisited;

    void OnEnable() => InventoryManager.GoldChanged += OnGoldChanged;
    void OnDisable() => InventoryManager.GoldChanged -= OnGoldChanged;

    void Start()
    {
        oreCross.SetActive(true);
        shopArrow.SetActive(false);
    }

    void OnGoldChanged(int gold)
    {
        // Первое золото значит, что игрок уже ударил по руде
        if (gold > 0 && oreCross) oreCross.SetActive(false);

        if (!shopHintShown && !shopVisited && gold >= goldForShopHint)
        {
            shopHintShown = true;
            shopArrow.SetActive(true);
        }
    }

    // Вызови, когда игрок вошёл в зону магазина или открыл его
    public void OnShopVisited()
    {
        shopVisited = true;
        shopArrow.SetActive(false);
    }
}