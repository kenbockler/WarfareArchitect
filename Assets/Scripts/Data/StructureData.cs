using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StructureData")]
public class StructureData : ScriptableObject
{
    public string DisplayName;
    //public int Cost;
    public Sprite IconSprite;
    //public Tower TowerPrefab;
    public KeyCode Hotkey;

    public float Range; // Struktuur on laskekauguse peamine m‰‰raja.
    // Samuti vıib struktuur m‰‰rata selle, kui palju tugiplokke mahub ja mis kujuga jne.
}
