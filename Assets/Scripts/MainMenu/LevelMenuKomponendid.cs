using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenuKomponendid : MonoBehaviour
{
    public static LevelMenuKomponendid Instance;
    public Button BackButton;
    public RectTransform ScenarioPanel;


    public ScenarioButton ScenarioButtonPrefab;

    [HideInInspector]
    public ScenarioData SelectedScenario;
    public List<ScenarioData> Scenarios;

    public GameObject mainMenuKomponendid;



    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        gameObject.SetActive(false);

        BackButton.onClick.AddListener(OnBackButtonClicked);
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Start()
    {
        foreach (ScenarioData data in Scenarios)
        {
            ScenarioButton button = Instantiate(ScenarioButtonPrefab, ScenarioPanel);
            button.SetData(data);
        }
    }

    public void ScenarioSelected(ScenarioData data)
    {
        SelectedScenario = data;
        print("Scenario selected: " + data.PresentedName);
        SceneManager.LoadScene(data.SceneName);
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back Button Pressed");

        // Peida LevelMenuKomponendid
        gameObject.SetActive(false);

        Debug.Log("LevelMenu Components should be hidden now");

        // Aktiveeri MainMenuKomponendid GameObject
        mainMenuKomponendid.SetActive(true);

        Debug.Log("MainMenu Components should be active now");
    }


}
