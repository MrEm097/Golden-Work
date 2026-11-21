using UnityEngine;

public class PulseHint : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float amount = 0.15f;
    Vector3 baseScale;

    void Awake() => baseScale = transform.localScale;

    void Update()
    {
        float k = 1f + Mathf.Sin(Time.unscaledTime * speed) * amount;
        transform.localScale = baseScale * k;
    }
}