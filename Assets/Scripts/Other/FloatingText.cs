using System;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    [Header("Motion")]
    [SerializeField] private float lifetime = 0.7f;
    [SerializeField] private float riseDistance = 1.2f; // сколько единиц мира пролетает вверх
    [SerializeField][Range(0f, 1f)] private float fadeStart = 0.5f; // с какой доли жизни начинает таять

    [Header("Color (всегда зелёный)")]
    [SerializeField] private Color darkGreen = new Color(0.05f, 0.45f, 0.12f);
    [SerializeField] private Color lightGreen = new Color(0.75f, 1f, 0.75f);

    [Header("Size")]
    [SerializeField] private float minScale = 0.8f; // для самого маленького числа
    [SerializeField] private float maxScale = 1.4f; // для самого большого

    [Header("Pop")]
    [SerializeField] private float popDuration = 0.12f;
    [SerializeField] private float normalPunch = 0.1f; // небольшой «хлопок» у обычных чисел
    [SerializeField] private float maxPunch = 0.4f;    // сильный «хлопок» у максимума

    private float age;
    private float baseScale;
    private float punch;
    private Vector3 startPos;
    private Color baseColor;
    private Action<FloatingText> onFinished;

    // t: 0 = минимум, 1 = потолок руды
    public void Init(int amount, float t, bool isMax, Vector3 position, Action<FloatingText> finishedCallback)
    {
        onFinished = finishedCallback;
        age = 0f;
        startPos = position;
        transform.position = position;

        baseColor = Color.Lerp(darkGreen, lightGreen, t);
        baseScale = Mathf.Lerp(minScale, maxScale, t);
        punch = isMax ? maxPunch : normalPunch;

        label.text = "+" + amount;
        label.color = baseColor;
        transform.localScale = Vector3.one * (baseScale * 0.5f);
    }

    void Update()
    {
        if (PauseMenu.IsPaused) return; // не нужно, если пауза у тебя через timeScale = 0

        age += Time.deltaTime;
        float k = Mathf.Clamp01(age / lifetime);

        // Быстрый «выстрел» вверх и замедление к концу
        float eased = 1f - (1f - k) * (1f - k);
        transform.position = startPos + Vector3.up * (riseDistance * eased);

        // Появление: растёт от 50% до 100% с кратким «хлопком» поверх
        float pop = Mathf.Clamp01(age / popDuration);
        float overshoot = 1f + punch * Mathf.Sin(pop * Mathf.PI);
        transform.localScale = Vector3.one * (baseScale * Mathf.Lerp(0.5f, 1f, pop) * overshoot);

        // Первую половину жизни непрозрачное, потом тает
        Color c = baseColor;
        c.a = 1f - Mathf.InverseLerp(fadeStart, 1f, k);
        label.color = c;

        if (k >= 1f) onFinished(this);
    }
}