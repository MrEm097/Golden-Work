using UnityEngine;

public class ScareOn : MonoBehaviour
{
    bool quitting;
    void OnApplicationQuit() => quitting = true;

    void OnDestroy()
    {
        if (quitting || !gameObject.scene.isLoaded) return;
        ScareTrigger.Armed = true;
    }
}
