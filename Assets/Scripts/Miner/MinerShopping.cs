using UnityEngine;

public class MinerShopping : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Shop")) return;

        G.Instance.UI.ShowShoppingDoor(true); // Открытие двери
        G.Instance.UI.ShowShoppingButton(true); // Если перс наткнулся на магаз то всплывает кнопка
        G.Instance.Tutorial.OnShopVisited();

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Shop")) return;

        G.Instance.UI.ShowShoppingDoor(false); // Закрытие двери
        G.Instance.UI.ShowShoppingButton(false);

    }
}
