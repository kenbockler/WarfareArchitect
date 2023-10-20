using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string DisplayName;
    //public int Cost;
    public Sprite IconSprite;
    //public Tower TowerPrefab;
    public KeyCode Hotkey;
}
