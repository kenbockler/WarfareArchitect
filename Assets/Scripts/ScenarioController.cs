using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioController : MonoBehaviour
{
    public static ScenarioController Instance;

    public TextMeshProUGUI ResourceText;
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI SelectedText;
    public TextMeshProUGUI WaveInfoText;

    public GameObject EndPanel;
    public TextMeshProUGUI EndText;
    public TextMeshProUGUI FinalScoreText;
    public TextMeshProUGUI ButtonText;

    public bool IsGameOver = false;

    private float stone = 500;
    private float iron = 0;
    private float uranium = 0;
    private int lives = 4;

    public ScenarioData Level;
    private List<WaveData> Waves;
    private int currentWave = 0;

    private bool Win;

    public GameObject OptionsMenuPanel;
    public GameObject MenuPanel;

    public AudioClipGroup SelectedAudio;

    private void Awake()
    {
        Instance = this;

        Events.OnSetLives += SetLives;
        Events.OnGetLives += GetLives;
        Events.OnSetStone += SetStone;
        Events.OnGetStone += GetStone;
        Events.OnSetIron += SetIron;
        Events.OnGetIron += GetIron;
        Events.OnSetUranium += SetUranium;
        Events.OnGetUranium += GetUranium;

        Events.OnTowerComponentSelected += TowerComponentSelected;

        Events.OnStartScenario += StartScenario;

        WaveInfoText.enabled = false;
    }

    private void OnDestroy()
    {
        Events.OnSetLives -= SetLives;
        Events.OnGetLives -= GetLives;
        Events.OnSetStone -= SetStone;
        Events.OnGetStone -= GetStone;
        Events.OnSetIron -= SetIron;
        Events.OnGetIron -= GetIron;
        Events.OnSetUranium -= SetUranium;
        Events.OnGetUranium -= GetUranium;

        Events.OnTowerComponentSelected -= TowerComponentSelected;

        Events.OnStartScenario -= StartScenario;
    }

    // Start is called before the first frame update
    void Start()
    {
        EndPanel.SetActive(false);
        SetLives(GetLives());
        SetStone(GetStone());
        SetIron(GetIron());
        SetUranium(GetUranium());
        Waves = Level.Waves;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindObjectsOfType<Health>().Length == 0)
        {            
            if (IsGameOver)
            {
                WaveInfoText.enabled = false;
            }
            else
            {
                WaveInfoText.enabled = true;
            }
        }
        else
        {
            WaveInfoText.enabled = false;
        }

        if(!IsGameOver && Input.GetKeyDown(KeyCode.Return))
        {
            if(GameObject.FindObjectsOfType<Health>().Length == 0)
            {
                WaveInfoText.enabled=false;
                NewWave(Waves[0]);
            }
        }
    }

    public void StartScenario(ScenarioData data)
    {
        SetLives(data.lives);
        SetStone(data.stone);
        SetIron(data.iron);
        SetUranium(data.uranium);

        /*
        for(int i=0; i < CardPanel.transform.childCount; i++)
        {
            Destroy(CardPanel.transform.GetChild(i).gameObject);
        }

        foreach(var tower in data.Towers)
        {
            TowerCard card = Instantiate<TowerCard>(TowerCardPrefab, CardPanel.transform);
            card.Data = tower;
        }
        */

        Level = data;

        Waves = data.Waves;

        //Events.StartWave(data.Waves[currentWave]);
        //StartTime = Time.time;
        //EndTime = Time.time + data.Waves[currentWave].Count * data.Waves[currentWave].SpawnDelay;
    }

    void SetLives(int lives)
    {
        lives = Mathf.Max(lives, 0);
        this.lives = lives;
        LivesText.text = "Lives: " + lives;
        if(lives < 1)
        {
            EndGame(false, 0);
        }
    }

    int GetLives() => lives;

    void SetStone(float stone)
    {
        this.stone = stone;
        ResourceText.text = "Stone: " + Mathf.Floor(stone) + ", Iron: " + Mathf.Floor(iron) + ", Uranium: " + Mathf.Floor(uranium);
    }

    float GetStone() => stone;

    void SetIron(float iron)
    {
        this.iron = iron;
        ResourceText.text = "Stone: " + Mathf.Floor(stone) + ", Iron: " + Mathf.Floor(iron) + ", Uranium: " + Mathf.Floor(uranium);
    }

    float GetIron() => iron;

    void SetUranium(float uranium)
    {
        this.uranium = uranium;
        ResourceText.text = "Stone: " + Mathf.Floor(stone) + ", Iron: " + Mathf.Floor(iron) + ", Uranium: " + Mathf.Floor(uranium);
    }

    float GetUranium() => uranium;

    public void EndGame(bool win, int score)
    {
        Events.EndGame(win);
        Win = win;
        if (Win)
        {
            EndText.text = "You won!";
            ButtonText.text = "Play again";
        }
        else
        {
            EndText.text = "They've ruined you.";
            ButtonText.text = "Try again";
        }
        EndPanel.SetActive(true);
    }

    public void NewWave(WaveData data)
    {
        if(currentWave >= Waves.Count)
        {
            if(GameObject.FindObjectsOfType<Health>().Length == 0)
                EndGame(true, 0);
        }
        else
        {
            Events.StartWave(Waves[currentWave]);
        }
        currentWave += 1;
    }

    public void Restart()
    {
        SelectedAudio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Win = false;
        StartScenario(Level);
    }

    public void LoadMainMenu()
    {
        SelectedAudio.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        SelectedAudio.Play();
        Application.Quit();
    }


    void TowerComponentSelected(TowerComponentData data)
    {
        SetSelectedText(data);
    }

    public void SetSelectedText(TowerComponentData data)
    {
        if (data == null)
        {
            SelectedText.text = "Empty";
        }
        else
        {
            SelectedText.text = data.DisplayName;
        }
    }

    public void ToggleOptionsPanel()
    {
        SelectedAudio.Play();
        bool isOptionsPanelActive = OptionsMenuPanel.activeSelf;

        // Vahetab options-paneeli olekut
        OptionsMenuPanel.SetActive(!isOptionsPanelActive);

        // Kui options-paneel on aktiivne, peida menüü-paneel ja vastupidi
        MenuPanel.SetActive(isOptionsPanelActive);
    }

    public void ReturnToMenuPanel()
    {
        // Deaktiveeri options-paneel
        OptionsMenuPanel.SetActive(false);

        // Aktiveeri menüü-paneel
        MenuPanel.SetActive(true);
    }

}
