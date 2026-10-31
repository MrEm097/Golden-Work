using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] RawImage[] dashes;
    [SerializeField] RectTransform button;
    [SerializeField] string gameScene = "Game";

    void Start()
    {
        HideAll();
    }

    public void OnPointerEnter(PointerEventData e) // При наведении на крестик
    {
        StopAllCoroutines(); // Останавливаем все корутины, чтобы не было наложения анимаций
        StartCoroutine(Walk()); // Запускаем анимацию появляения черточек
        button.localScale = Vector3.one * 1.1f; // Увеличиваем размер кнопки
    }
    public void OnPointerExit(PointerEventData e) // При уходе с крестика
    {
        StopAllCoroutines(); // Останавливаем анимацию появления черточек
        HideAll(); // Скрываем все нахуй
        button.localScale = Vector3.one; // Возвращаем нормальный размер кнопки
    }
    public void OnPointerClick(PointerEventData e) // При клике на крестик
    {
        SceneManager.LoadScene(gameScene); // Загружаем сцену с игрой
    }

    void HideAll() // При срабатывании кнопки "Play" скрываем все полоски
    {
        foreach (RawImage d in dashes)
        {
            d.gameObject.SetActive(false);
        }
    }

    IEnumerator Walk() // Метод для анимации черточек
    {
        foreach (RawImage d in dashes) // Проходимся по всем черточкам
        {
            d.gameObject.SetActive(true); // Делаем полоску видимой
            d.transform.localScale = Vector3.one * 2.5f; // Устанавливаем размер полоски больше нормы

            while (d.transform.localScale.x > 1f) // Пока полоска больще нормы мы его уменьшаем
            {
                d.transform.localScale -= Vector3.one * 8f * Time.deltaTime; // Уменьшаем размер полоски
                yield return null; // Ждем один кадр
            }

            d.transform.localScale = Vector3.one; // Устанавливаем нормальный размер полоски
        }
    }
}

