using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GunBaseData")]
public class GunBaseData : ScriptableObject
{
    public string DisplayName;
    public List<int> Cost;
    public Sprite IconSprite;
    //public Tower TowerPrefab;
    public KeyCode Hotkey;

    public float DamageModifier;
    public float FirerateModifier;
}
