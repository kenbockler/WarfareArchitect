using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class optionsMenuKomponendid : MonoBehaviour
{
    public static optionsMenuKomponendid Instance;

    public GameObject mainMenuKomponendid;
    public Slider masterVolumeSlider;
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
        // Seo nuppude s�ndmused
        backButton.onClick.AddListener(OnBackButtonClicked);
        masterVolumeSlider.onValueChanged.AddListener(delegate { OnMasterVolumeChanged(masterVolumeSlider.value); });

        muteToggle.onValueChanged.AddListener(delegate { OnMuteChanged(muteToggle.isOn); });
    }

    public void OnMasterVolumeChanged(float volume)
    {
        // Muuda helitugevust
        Debug.Log("Muudetud helitugevus: " + volume);
    }



    public void OnMuteChanged(bool isMuted)
    {
        // L�lita heli v�lja v�i sisse
        Debug.Log("Helid " + (isMuted ? "v�lja l�litatud" : "sisse l�litatud"));
    }

    public void OnBackButtonClicked()
    {
        // Peida Options-men�� ja n�ita peamen��d
        gameObject.SetActive(false);
        mainMenuKomponendid.SetActive(true);
    }
}
