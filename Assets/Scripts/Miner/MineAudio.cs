using UnityEngine;

public class MineAudio : MonoBehaviour
{
    [SerializeField] AudioSource source;

    public static bool soundOn = true;

    public void Play(AudioClip clip, float volume = 1f)
    {
        if (!soundOn || clip == null) return;
        source.PlayOneShot(clip, volume);
    }

    public void PlayRandom(AudioClip[] clips, float volume = 1f)
    {
        if (clips == null || clips.Length == 0) return;
        Play(clips[Random.Range(0, clips.Length)], volume);
    }
}
