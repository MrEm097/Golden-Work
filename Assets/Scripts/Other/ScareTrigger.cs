using System.Collections;
using UnityEngine;

public class ScareTrigger : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioClip scream;
    [SerializeField] GameObject scareImage;
    [SerializeField] float scareDuration = 5f;
    [SerializeField] float musicFade = 4f;

    bool played;

    public static bool Armed;

    void Awake() => Armed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!Armed || played || !other.CompareTag("Player")) return;
        played = true;
        StartCoroutine(Scare());
    }

    IEnumerator Scare()
    {
        float originalVolume = music.volume;
        music.volume = 0f;

        sfx.PlayOneShot(scream);
        scareImage.SetActive(true);
        yield return new WaitForSeconds(scareDuration);
        scareImage.SetActive(false);

        for (float t = 0; t < musicFade; t += Time.deltaTime)
        {
            music.volume = Mathf.Lerp(0f, originalVolume, t / musicFade);
            yield return null;
        }
        music.volume = originalVolume;
    }
}
