using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ScenarioButton : MonoBehaviour
{
    // TODO
    // public ScenarioData ScenaroData;

    public TextMeshProUGUI NameText;
    private Button button;

    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void Start()
    {
        // TODO
        // NameText.text = ScenarioData.PresentedName;
    }

    public void OnClick()
    {
        print("Card pressed");
    }


}
