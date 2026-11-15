using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FloatingTextSpawner : MonoBehaviour
{
    public static FloatingTextSpawner Instance { get; private set; }

    [SerializeField] private FloatingText prefab;

    [Header("Placement")]
    [SerializeField] private float spreadX = 0.5f;
    [SerializeField] private float spreadY = 0.2f;

    [Header("Clutter control")]
    [SerializeField] private int maxActive = 8;

    private ObjectPool<FloatingText> pool;
    private readonly List<FloatingText> active = new List<FloatingText>();
    private float side = 1f; // чередуем стороны: слева / справа

    void Awake()
    {
        Instance = this;
        pool = new ObjectPool<FloatingText>(
            () => Instantiate(prefab, transform),
            t => t.gameObject.SetActive(true),
            t => t.gameObject.SetActive(false),
            t => Destroy(t.gameObject));
    }

    public void Spawn(int amount, int min, int max, Vector3 position)
    {
        // Слишком много на экране: самое старое убираем сразу
        if (active.Count >= maxActive) Despawn(active[0]);

        float t = max > min ? Mathf.InverseLerp(min, max, amount) : 1f;
        bool isMax = max > min && amount >= max;

        side = -side;
        Vector3 offset = new Vector3(
            side * Random.Range(0.3f, 1f) * spreadX,
            Random.Range(-spreadY, spreadY),
            0f);

        FloatingText text = pool.Get();
        active.Add(text);
        text.Init(amount, t, isMax, position + offset, Despawn);
    }

    private void Despawn(FloatingText text)
    {
        active.Remove(text);
        pool.Release(text);
    }
}