using UnityEngine;

public class TheEnd : MonoBehaviour
{
    [SerializeField] private GameObject endImage;

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        endImage.SetActive(true);
    }
}
