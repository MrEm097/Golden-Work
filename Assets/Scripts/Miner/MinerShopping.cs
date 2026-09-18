using UnityEngine;

public class MinerShopping : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        G.Instance.UI.ShowShoppingDoor(!collision.CompareTag("Shop")); // Открытие двери
        G.Instance.UI.ShowShoppingButton(collision.CompareTag("Shop")); // Если перс наткнулся на магаз то всплывает кнопка
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Shop"))
        {
            G.Instance.UI.ShowShoppingDoor(true); // Закрытие двери
            G.Instance.UI.ShowShoppingButton(false);
        }

    }
}
