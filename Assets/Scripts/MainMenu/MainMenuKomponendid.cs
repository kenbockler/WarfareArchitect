using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuKomponendid : MonoBehaviour
{
    public TextMeshProUGUI GameName;
    public Button PlayButton;
    public Button HelpButton;
    public Button ControlsButton;
    public Button OptionsButton;
    public Button ExitButton;

    public GameObject levelMenuKomponendid;
    public GameObject helpMenuKomponendid;
    public GameObject controlsSheet;
    public GameObject optionsMenuKomponendid;

    public AudioClipGroup SelectedAudio;

    public void Awake()
    {
        PlayButton.onClick.AddListener(OnPlay);
        HelpButton.onClick.AddListener(OnHelp);
        ControlsButton.onClick.AddListener(OnControls);
        OptionsButton.onClick.AddListener(OnOptions);
        ExitButton.onClick.AddListener(OnExit);
    }

    public void OnPlay()
    {
        // Peida kogu MainMenuKomponendid GameObject
        SelectedAudio.Play();
        gameObject.SetActive(false);

        levelMenuKomponendid.SetActive(true);
    }

    public void OnHelp()
    {
        SelectedAudio.Play();
        gameObject.SetActive(false);
        helpMenuKomponendid.SetActive(true);
    }

    public void OnControls()
    {
        SelectedAudio.Play();
        gameObject.SetActive(false);
        controlsSheet.SetActive(true);
    }

    public void OnOptions()
    {
        SelectedAudio.Play();
        gameObject.SetActive(false);
        optionsMenuKomponendid.SetActive(true);
    }

    public void OnExit()
    {
        SelectedAudio.Play();
        Application.Quit();
    }
}
