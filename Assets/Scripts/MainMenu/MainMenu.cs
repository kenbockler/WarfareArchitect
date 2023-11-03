using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPresenter : MonoBehaviour
{
    public static MenuPresenter Instance;
    public Button ExitButton;
    public RectTransform ScenarioPanel;

    /*
    public ScenarioPresenter ScenarioPresenterPrefab;

    [HideInInspector]
    public ScenarioData SelectedScenario;
    public List<ScenarioData> Scenarios;
    */
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        ExitButton.onClick.AddListener(OnExit);
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    /*
     * public void Start()
     *    {
     *           foreach (ScenarioData data in Scenarios)
     *                  {
     *                             ScenarioPresenter presenter = GameObject.Instantiate(ScenarioPresenterPrefab, ScenarioPanel);
     *                                        presenter.SetData(data);
     *                                               }
     *                                                  }
     

    public void ScenarioSelected(ScenarioData data)
    {
        SelectedScenario = data;
        SceneManager.LoadScene(SelectedScenario.SceneName);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0) return;
        Events.StartLevel(SelectedScenario);
        gameObject.SetActive(false);
    }
    */


    public void OnExit()
    {
        Application.Quit();
    }

}
