using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Image soundIcon, musicIcon;
    [SerializeField] private Sprite soundOn, soundOff, musicOn, musicOff;

    private bool soundEnabled = true;
    private bool musicEnabled = true;

    public void ToggleSound()
    {
        soundEnabled = !soundEnabled;
        soundIcon.sprite = soundEnabled ? soundOn : soundOff;
        // TODO: Реализовать логику включения/выключения ЗВУКА в игре
    }

    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        musicIcon.sprite = musicEnabled ? musicOn : musicOff;
        // TODO: Реализовать логику включения/выключения МУЗЫКИ в игре
    }
}
