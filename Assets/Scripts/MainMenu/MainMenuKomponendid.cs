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
    public Button OptionsButton;
    public Button ExitButton;

    public GameObject levelMenuKomponendid;
    public GameObject optionsMenuKomponendid;

    public void Awake()
    {
        PlayButton.onClick.AddListener(OnPlay);
        OptionsButton.onClick.AddListener(OnOptions);
        ExitButton.onClick.AddListener(OnExit);
    }

    public void OnPlay()
    {
        // Peida kogu MainMenuKomponendid GameObject
        gameObject.SetActive(false);

        levelMenuKomponendid.SetActive(true);
    }

    public void OnOptions()
    {
        gameObject.SetActive(false);
        optionsMenuKomponendid.SetActive(true);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
