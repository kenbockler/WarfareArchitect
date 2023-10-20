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

    private int resource1 = 50;
    private int lives = 4;

    private void Awake()
    {
        Events.OnSetLives += SetLives;
        Events.OnGetLives += GetLives;
    }

    private void OnDestroy()
    {
        Events.OnSetLives -= SetLives;
        Events.OnGetLives -= GetLives;
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
}
