using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;
    public float parallaxEffect; // Значение от 0 до 1

    void Start()
    {
        startPos = transform.position.x;

        // Автоматически берем ширину картинки для бесшовного повторения
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            length = spriteRenderer.bounds.size.x;
        }
    }

    void FixedUpdate()
    {
        if (cam == null) return;

        // Рассчитываем виртуальное смещение для проверки границ зацикливания
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        // Рассчитываем реальное смещение объекта фона
        float dist = (cam.transform.position.x * parallaxEffect);

        // Двигаем фон только по горизонтали (X) вслед за камерой
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        // Мгновенная телепортация фона влево/вправо, когда камера доходит до края спрайта
        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
