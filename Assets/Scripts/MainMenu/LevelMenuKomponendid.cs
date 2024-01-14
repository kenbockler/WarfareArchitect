using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public AudioClipGroup SelectedAudio;



    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        gameObject.SetActive(false);

        BackButton.onClick.AddListener(OnBackButtonClicked);
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
        SelectedAudio.Play();
        SelectedScenario = data;
        print("Scenario selected: " + data.PresentedName);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(data.SceneName);
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Events.StartScenario(SelectedScenario);
       //Destroy(gameObject);
    }

    public void OnBackButtonClicked()
    {
        SelectedAudio.Play();
        Debug.Log("Back Button Pressed");

        // Peida LevelMenuKomponendid
        gameObject.SetActive(false);

        Debug.Log("LevelMenu Components should be hidden now");

        // Aktiveeri MainMenuKomponendid GameObject
        mainMenuKomponendid.SetActive(true);

        Debug.Log("MainMenu Components should be active now");
    }


}
