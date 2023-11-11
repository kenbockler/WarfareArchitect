using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ScenarioButton : MonoBehaviour
{
    public ScenarioData ScenarioData;

    public TextMeshProUGUI NameText;
    private Button button;

    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void SetData(ScenarioData data)
    {
        ScenarioData = data;
        NameText.text = ScenarioData.PresentedName;
    }

    public void OnClick()
    {
        LevelMenuKomponendid.Instance.ScenarioSelected(ScenarioData);
    }


}
