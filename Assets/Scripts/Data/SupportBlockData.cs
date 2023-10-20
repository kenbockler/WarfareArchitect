using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SupportBlockData")]
public class SupportBlockData : ScriptableObject
{
    public string DisplayName;
    //public int Cost;
    public Sprite IconSprite;
    //public Tower TowerPrefab;
    public KeyCode Hotkey;
}
