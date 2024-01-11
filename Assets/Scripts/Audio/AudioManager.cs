using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Toggle muteToggle;

    private void Start()
    {
        float volume = volumeSlider.value;
        bool isMuted = muteToggle.isOn;

        SetVolume(volume);
        Mute(isMuted);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void Mute(bool isMuted)
    {
        if (isMuted)
        {
            audioMixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            float volume = volumeSlider.value;
            SetVolume(volume);
            SetVolume(volume);
        }
    }
}
