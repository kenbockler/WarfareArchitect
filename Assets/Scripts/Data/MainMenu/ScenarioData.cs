using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Scenario")]
public class ScenarioData : ScriptableObject
{
    public string PresentedName;
    public string SceneName;
    public int stone = 0;
    public int iron = 0;
    public int uranium = 0;
    public int lives = 10;

    public bool IsLocked;
    public List<WaveData> Waves;
}
