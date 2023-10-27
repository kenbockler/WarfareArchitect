using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StructureData")]
public class StructureData : TowerComponentData
{
    public float Range; // Struktuur on laskekauguse peamine määraja.
    // Samuti võib struktuur määrata selle, kui palju tugiplokke mahub ja mis kujuga jne.

    public Structure StructurePrefab;
}
