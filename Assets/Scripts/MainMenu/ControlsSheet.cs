using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsSheet : MonoBehaviour
{
    public static ControlsSheet Instance;
    public Button BackButton;
    public RectTransform ScenarioPanel;

    public GameObject mainMenuKomponendid;

    public AudioClipGroup SelectedAudio;

    public void Awake()
    {
        gameObject.SetActive(false);

        BackButton.onClick.AddListener(OnBackButtonClicked);
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnBackButtonClicked()
    {
        SelectedAudio.Play();
        Debug.Log("Back Button Pressed");

        // Peida LevelMenuKomponendid
        gameObject.SetActive(false);

        Debug.Log("Controls should be hidden now");

        // Aktiveeri MainMenuKomponendid GameObject
        mainMenuKomponendid.SetActive(true);

        Debug.Log("MainMenu Components should be active now");
    }
}
