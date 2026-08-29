using UnityEngine;

public class MinerController : MonoBehaviour
{
    [Header("Move")]
    public float speed = 1f; // Скорость епт
    public float stopZone = 0.3f; // Зона на которой если курсор находится то игрок стоит

    private Camera mainCamera;
    private Rigidbody2D rb;

    void Start()
    {
        mainCamera = Camera.main;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint( Input.mousePosition ); // Позиция мыши на экране

        float direction = mousePos.x - transform.position.x; // Разница между позицией мыши и игрока по X

        if ( Mathf.Abs(direction) <= stopZone ) // Если курсор на персонаже
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Останавливаем перса
        }
        else
        {
            float moveDirection = Mathf.Sign(direction); // Определяет напровление своего движения

            rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y); // "Движение - это жизнь"
        }
    }
}
