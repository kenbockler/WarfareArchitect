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

    private float stone = 50;
    private float iron = 0;
    private float uranium = 0;
    private int lives = 4;

    public ScenarioData Level;
    private List<WaveData> Waves;
    private int currentWave = 0;

    private bool Win;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Win = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
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
}
