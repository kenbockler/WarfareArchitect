using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ScenarioData")]
public class ScenarioData : MonoBehaviour
{
    public string scenarioName;
    public string SceneName;
    public int stone = 0;
    public int iron = 0;
    public int uranium = 0;
    public int lives = 10;

    public List<WaveData> Waves;
}
