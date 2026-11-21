using UnityEngine;

// Код говно если что...

public class G : MonoBehaviour // GameManager
{
    public static G Instance { get; private set; }

    public InventoryManager Inventory {  get; private set; }
    public UIManager UI {  get; private set; }
    public MineAudio Audio { get; private set; }
    public TutorialHints Tutorial { get; private set; }

    private void Awake()
    {
        if (Instance == null) // Проверка на дубликата
        {
            Instance = this;


            Inventory = GetComponent<InventoryManager>();


            UI = GetComponent<UIManager>();


            Audio = GetComponent<MineAudio>();


            Tutorial = GetComponent<TutorialHints>();
        }
        else
        {
            Destroy(gameObject); // Если есть дубликат, то самоуничтожение
        }
    }
}
