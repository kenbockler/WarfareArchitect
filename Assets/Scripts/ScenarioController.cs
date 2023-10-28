using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScenarioController : MonoBehaviour
{
    public TextMeshProUGUI ResourceText;
    public TextMeshProUGUI LivesText;

    public GameObject EndPanel;
    public TextMeshProUGUI EndText;
    public TextMeshProUGUI FinalScoreText;
    public TextMeshProUGUI ButtonText;

    private float stone = 50;
    private float iron = 0;
    private float uranium = 0;
    private int lives = 4;

    public List<WaveData> Waves;
    private int currentWave = 0;

    private bool Win;

    private void Awake()
    {
        Events.OnSetLives += SetLives;
        Events.OnGetLives += GetLives;
        Events.OnSetStone += SetStone;
        Events.OnGetStone += GetStone;
        Events.OnSetIron += SetIron;
        Events.OnGetIron += GetIron;
        Events.OnSetUranium += SetUranium;
        Events.OnGetUranium += GetUranium;

        Events.OnEndWave += NewWave;
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

        Events.OnEndWave += NewWave;
    }

    // Start is called before the first frame update
    void Start()
    {
        EndPanel.SetActive(false);
        SetLives(GetLives());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLives(int lives)
    {
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
        currentWave += 1;
        if(currentWave >= Waves.Count)
        {
            EndGame(true, 0);
        }
        else
        {
            Events.StartWave(Waves[currentWave]);
        }
    }
}
