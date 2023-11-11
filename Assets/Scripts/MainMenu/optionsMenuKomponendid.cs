using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class optionsMenuKomponendid : MonoBehaviour
{
    public static optionsMenuKomponendid Instance;

    public GameObject mainMenuKomponendid;
    public Slider masterVolumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public Toggle muteToggle;
    public Button backButton;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        gameObject.SetActive(false);
    }

    void Start()
    {
        // Seo nuppude sündmused
        backButton.onClick.AddListener(OnBackButtonClicked);
        masterVolumeSlider.onValueChanged.AddListener(delegate { OnMasterVolumeChanged(masterVolumeSlider.value); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(resolutionDropdown.value); });
        muteToggle.onValueChanged.AddListener(delegate { OnMuteChanged(muteToggle.isOn); });
    }

    public void OnMasterVolumeChanged(float volume)
    {
        // Muuda helitugevust
        Debug.Log("Muudetud helitugevus: " + volume);
    }

    public void OnResolutionChange(int resolutionIndex)
    {
        // Muuda eraldusvõimet
        Debug.Log("Valitud eraldusvõime: " + resolutionDropdown.options[resolutionIndex].text);
    }

    public void OnMuteChanged(bool isMuted)
    {
        // Lülita heli välja või sisse
        Debug.Log("Helid " + (isMuted ? "välja lülitatud" : "sisse lülitatud"));
    }

    public void OnBackButtonClicked()
    {
        // Peida Options-menüü ja näita peamenüüd
        gameObject.SetActive(false);
        mainMenuKomponendid.SetActive(true);
    }
}
