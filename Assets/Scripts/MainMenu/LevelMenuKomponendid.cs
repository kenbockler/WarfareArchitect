using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button BackButton;


    public void Awake()
    {
        BackButton.onClick.AddListener(OnBackButtonClicked);
    }

    public void OnBackButtonClicked()
    {
        // todo: unload LevelMenuKomponendid
        // todo: load MainMenuKomponendid
        print("Back button clicked");
    }
}
