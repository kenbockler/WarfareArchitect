using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/FoundationData")]
public class FoundationData : ScriptableObject
{
    public string DisplayName;
    //public int Cost;
    public Sprite IconSprite;
    //public Tower TowerPrefab;
    public KeyCode Hotkey;

    public bool Limited; // Piirab tugiplokkide arvu ja võib-olla veel midagi.
}
