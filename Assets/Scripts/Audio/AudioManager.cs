using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Toggle muteToggle;

    private float lastVolumeLevel = 0.5f; // Eeldefineeritud helitugevuse tase

    private void Start()
    {
        // Seadistage UI elemendid ja lisage kuulajad
        UpdateMuteToggleBasedOnVolume();
        volumeSlider.onValueChanged.AddListener(HandleVolumeChange);
        muteToggle.onValueChanged.AddListener(HandleMuteToggle);
    }

    private void UpdateMuteToggleBasedOnVolume()
    {
        muteToggle.isOn = volumeSlider.value == 0;
    }

    private void HandleVolumeChange(float volume)
    {
        SetVolume(volume); // Uuenda helitugevust AudioMixeris

        if (volume > 0)
        {
            lastVolumeLevel = volume;
            muteToggle.isOn = false;
        }
        else
        {
            muteToggle.isOn = true;
        }
    }

    private void HandleMuteToggle(bool isMuted)
    {
        if (isMuted)
        {
            lastVolumeLevel = volumeSlider.value;
            volumeSlider.value = 0;
            SetVolume(0);
        }
        else
        {
            volumeSlider.value = lastVolumeLevel > 0 ? lastVolumeLevel : 1f;
            SetVolume(volumeSlider.value);
        }
    }

    public void SetVolume(float volume)
    {
        if (volume > 0)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", -80); // Vaigista heli
        }
    }
}
