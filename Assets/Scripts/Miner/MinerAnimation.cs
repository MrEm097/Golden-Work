using System.Collections;
using UnityEngine;

public class MinerAnimation : MonoBehaviour
{
    [SerializeField] Rigidbody2D body; // Ссылка на компонент Rigidbody2D, который управляет физикой персонажа
    [SerializeField] float walkThreshold = 0.2f; // Порог скорости, при котором персонаж считается идущим (для воспроизведения анимации ходьбы)
    [SerializeField] float walkRefSpeed = 3f; // Скорость, при которой анимация ходьбы будет воспроизводиться с нормальной скоростью (1x)

    [Header("Tool")]
    [SerializeField] SpriteRenderer toolRenderer;            // Hand_Socket
    [SerializeField] TrailRenderer trail;                    // на ToolTip
    [SerializeField] RuntimeAnimatorController defaultController; // Miner.controller
    public Transform toolTip;                                // ToolTip
    public ToolData Current { get; private set; }

    Animator anim; // Ссылка на компонент Animator, который управляет анимациями персонажа

    static readonly int IsWalking = Animator.StringToHash("isWalking"); // Хэш для параметра анимации "isWalking"
    static readonly int WalkSpeed = Animator.StringToHash("walkSpeed"); // Хэш для параметра анимации "walkSpeed"

    [Header("Swing timing (секунды при скорости 1)")]
    [SerializeField] float trailStart = 0.2f;  // когда включать линию
    [SerializeField] float trailEnd = 0.4f;    // когда выключать
    Coroutine trailRoutine;

    void Awake() { 
        anim = GetComponent<Animator>(); 
        if (trail != null) trail.emitting = false; // Отключаем эмиссию частиц на старте, чтобы они не появлялись до начала движения
    }

    void Update() // Обновляем анимацию в зависимости от скорости персонажа
    {
        if (PauseMenu.IsPaused) return; // Если игра на паузе, то не обновляем анимацию

        float vx = body.linearVelocity.x;
        bool walking = Mathf.Abs(vx) > walkThreshold;

        anim.SetBool(IsWalking, walking); // Устанавливаем параметр анимации "isWalking" в зависимости от того, движется ли персонаж

        anim.SetFloat(WalkSpeed, Mathf.Clamp(Mathf.Abs(vx) / walkRefSpeed, 0.8f, 2f)); // Устанавливаем параметр анимации "walkSpeed" в зависимости от скорости персонажа, ограничивая его в диапазоне от 0.8 до 2

        if (walking) // Если персонаж движется, то поворачиваем спрайт в зависимости от направления движения
        {
            Vector3 s = transform.localScale; // Получаем текущий масштаб объекта
            s.x = -Mathf.Abs(s.x) * Mathf.Sign(vx); // Поворот спрайта в зависимости от направления движения
            transform.localScale = s;
        }
    }

    public void PlayMine(float speed)
    {
        anim.SetFloat("mineSpeed", speed);
        anim.SetTrigger("mine");

        if (trailRoutine != null) StopCoroutine(trailRoutine);
        if (Current != null && Current.showTrail)
            trailRoutine = StartCoroutine(TrailFlash(speed));
    }

    IEnumerator TrailFlash(float speed) // Включаем и выключаем эмиссию частиц на ToolTip в зависимости от времени анимации
    {
        if (trail == null) yield break;
        yield return new WaitForSeconds(trailStart / speed);
        trail.emitting = true;
        yield return new WaitForSeconds((trailEnd - trailStart) / speed);
        trail.emitting = false;
    }

    public void Equip(ToolData tool) // Экипируем инструмент, обновляем спрайт и анимации в зависимости от выбранного инструмента
    {
        if (tool == null || !tool.showOnMiner) return;

        Current = tool;
        toolRenderer.sprite = tool.spriteTool;
        toolRenderer.enabled = tool.spriteTool != null;

        anim.runtimeAnimatorController =
            tool.animations != null ? tool.animations : defaultController;

        trail.startColor = tool.trailColor;
        trail.endColor = new Color(tool.trailColor.r, tool.trailColor.g, tool.trailColor.b, 0f);
    }
}
