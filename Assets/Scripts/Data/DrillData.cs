using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DrillData")]
public class DrillData : ScriptableObject
{
    public string DisplayName;
    public List<int> Cost;
    public Sprite IconSprite;
    public Drill DrillPrefab;
    public KeyCode Hotkey;
}
