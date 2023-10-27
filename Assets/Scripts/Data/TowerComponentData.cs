using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/TowerComponentData")]
public class TowerComponentData : ScriptableObject
{
    public string DisplayName;
    public List<int> Cost;
    public Sprite IconSprite;
    //public Tower TowerPrefab;
    public KeyCode Hotkey;
}
