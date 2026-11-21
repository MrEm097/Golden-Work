using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Tutorial : MonoBehaviour
{
    [SerializeField] private float showTime = 20f;
    [SerializeField] private float fadeTime = 1f;

    private CanvasGroup group;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 1f; // Показываем туториал сразу при старте
    }

    private IEnumerator Start()
    {
        float timer = 0f;

        while (timer < showTime) // Таймер для показа туториала
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        float t = 0f; // Начинаем процесс исчезновения

        while (t < fadeTime) // Цикл для плавного исчезновения
        {
            t += Time.unscaledDeltaTime;
            group.alpha = 1f - Mathf.Clamp01(t / fadeTime); // Плавное уменьшение прозрачности
            yield return null;
        }

        gameObject.SetActive(false); // Скрываем туториал после времени показа
    }
}
