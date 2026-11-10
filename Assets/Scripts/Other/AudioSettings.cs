using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Image soundIcon, musicIcon;
    [SerializeField] private Sprite soundOn, soundOff, musicOn, musicOff;
    [SerializeField] private AudioSource soundSource;
    private AudioSource musicSource;

    private bool soundEnabled = true;
    private bool musicEnabled = true;

    void Start()
    {
        musicSource = FindFirstObjectByType<Music>().GetComponent<AudioSource>(); // Необходимо для свизи между сценами    
    }
    public void ToggleSound()
    {
        soundEnabled = !soundEnabled;
        soundIcon.sprite = soundEnabled ? soundOn : soundOff;
        MineAudio.soundOn = soundEnabled;
    }

    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        musicIcon.sprite = musicEnabled ? musicOn : musicOff;
        musicSource.mute = !musicEnabled; // Вкл/Выкл самой музыки
    }
}
