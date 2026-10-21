using UnityEngine;

public class MinerController : MonoBehaviour
{
    [Header("Move Settings")]
    public float stopZone = 0.3f;
    public float acceleration = 50f; // Сила разгона

    public bool CanMove { get; set; } = true;

    private Camera mainCamera;
    private Rigidbody2D rb;
    private float maxSpeed; // Это твоя скорость из инвентаря
    private Vector3 targetMousePos;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        maxSpeed = G.Instance.Inventory.toolSpeed.speedMove;
        targetMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void UpdateAnimation()
    {

    }

    void FixedUpdate()
    {
        if (!CanMove)
        {
            // Тормозим, если ходить нельзя (открыт магазин)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.8f, rb.linearVelocity.y);
            return;
        }

        float directionX = targetMousePos.x - transform.position.x;

        if (Mathf.Abs(directionX) > stopZone)
        {
            float moveSign = Mathf.Sign(directionX);

            // Рассчитываем, сколько скорости нам нужно добавить до максимума
            float speedError = (moveSign * maxSpeed) - rb.linearVelocity.x;

            // Прикладываем силу, пропорциональную этой разнице
            float force = speedError * acceleration;
            rb.AddForce(new Vector2(force, 0f), ForceMode2D.Force);
        }
        else
        {
            // Естественное торможение у курсора без рывков
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.7f, rb.linearVelocity.y);
        }
    }
}
