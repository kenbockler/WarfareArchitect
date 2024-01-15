using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenuKomponendid : MonoBehaviour
{
    public static HelpMenuKomponendid Instance;
    public Button BackButton;
    public RectTransform HelpPanel;


    public Button LeftButton;
    public Button RightButton;

    public List<Sprite> HelpSlides;

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
        Image background = HelpPanel.GetComponent<Image>();
        background.sprite = HelpSlides[Selected];
    }

    public void OnRightButtonClicked()
    {
        SelectedAudio.Play();
        Selected = (Selected + 1) % 7;
        Start();
    }

    public void OnLeftButtonClicked()
    {
        SelectedAudio.Play();
        Selected = (Selected + 6) % 7;
        Start();
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
