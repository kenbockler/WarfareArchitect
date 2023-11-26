using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class ScenarioButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ScenarioData ScenarioData;

    public TextMeshProUGUI NameText;
    private Button button;
    private Image buttonImage;

    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        buttonImage = GetComponent<Image>();
    }

    public void SetData(ScenarioData data)
    {
        ScenarioData = data;
        NameText.text = ScenarioData.PresentedName;

        if (ScenarioData.IsLocked)
        {
            button.interactable = false;
            buttonImage.color = new Color(0.8f, 0.8f, 0.8f, 1);
            NameText.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            button.interactable = true;
            buttonImage.color = Color.white;
            NameText.color = Color.black;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!ScenarioData.IsLocked)
        {
            buttonImage.color = new Color(0.78f, 0.88f, 0.94f, 1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!ScenarioData.IsLocked)
        {
            buttonImage.color = Color.white;
        }
    }

    public void OnClick()
    {
        if (!ScenarioData.IsLocked)
        {
            LevelMenuKomponendid.Instance.ScenarioSelected(ScenarioData);
        }
    }
}
