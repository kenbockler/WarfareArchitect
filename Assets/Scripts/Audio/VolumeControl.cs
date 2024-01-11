using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle muteToggle;

    private float lastVolumeLevel = 0.5f;

    void Start()
    {
        UpdateMuteToggleBasedOnVolume();

        volumeSlider.onValueChanged.AddListener(HandleVolumeChange);
        muteToggle.onValueChanged.AddListener(HandleMuteToggle);
    }

    void UpdateMuteToggleBasedOnVolume()
    {
        muteToggle.isOn = volumeSlider.value == 0;
    }

    void HandleVolumeChange(float volume)
    {
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

    void HandleMuteToggle(bool isMuted)
    {
        if (isMuted)
        {
            lastVolumeLevel = volumeSlider.value;
            volumeSlider.value = 0;
        }
        else
        {
            volumeSlider.value = lastVolumeLevel > 0 ? lastVolumeLevel : 1f;
        }
    }
}
