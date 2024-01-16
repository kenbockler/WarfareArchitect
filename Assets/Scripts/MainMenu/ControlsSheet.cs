using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsSheet : MonoBehaviour
{
    public static ControlsSheet Instance;
    public Button BackButton;
    public RectTransform ControlsPanel;

    public Button LeftButton;
    public Button RightButton;

    public List<Sprite> ControlsSlides;

    private int Selected = 0;
    public GameObject mainMenuKomponendid;

    public AudioClipGroup SelectedAudio;

    public void Awake()
    {
        gameObject.SetActive(false);

        BackButton.onClick.AddListener(OnBackButtonClicked);
        LeftButton.onClick.AddListener(OnLeftButtonClicked);
        RightButton.onClick.AddListener(OnRightButtonClicked);
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Image background = ControlsPanel.GetComponent<Image>();
        background.sprite = ControlsSlides[Selected];
    }

    public void OnRightButtonClicked()
    {
        SelectedAudio.Play();
        Selected = (Selected + 1) % 2;
        Start();
    }

    public void OnLeftButtonClicked()
    {
        OnRightButtonClicked();
    }

    public void OnBackButtonClicked()
    {
        SelectedAudio.Play();
        Debug.Log("Back Button Pressed");

        // Peida LevelMenuKomponendid
        gameObject.SetActive(false);

        Debug.Log("How to play Components should be hidden now");

        // Aktiveeri MainMenuKomponendid GameObject
        mainMenuKomponendid.SetActive(true);

        Debug.Log("MainMenu Components should be active now");
    }
}
