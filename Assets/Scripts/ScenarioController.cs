using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// RESSURSSIDELE TULEB NIMED VÄLJA MÕELDA, SIIS SAAB VASTAVAD SÜNDMUSED LUUA.

public class ScenarioController : MonoBehaviour
{
    public TextMeshProUGUI ResourceText;
    public TextMeshProUGUI LivesText;

    private int stone = 50;
    private int iron = 0;
    private int uranium = 0;
    private int lives = 4;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLives(int lives)
    {
        this.lives =lives;
        LivesText.text = "Lives: " + lives;
    }

    int GetLives() => lives;

    void SetStone(int stone)
    {
        this.stone = stone;
        ResourceText.text = "Stone: " + stone + ", Iron: " + iron + ", Uranium: " + uranium;
    }

    int GetStone() => stone;

    void SetIron(int iron)
    {
        this.iron = iron;
        ResourceText.text = "Stone: " + stone + ", Iron: " + iron + ", Uranium: " + uranium;
    }

    int GetIron() => iron;

    void SetUranium(int uranium)
    {
        this.uranium = uranium;
        ResourceText.text = "Stone: " + stone + ", Iron: " + iron + ", Uranium: " + uranium;
    }

    int GetUranium() => uranium;
}
