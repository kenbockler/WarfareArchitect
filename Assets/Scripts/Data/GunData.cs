using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GunData")]
public class GunData : TowerComponentData
{
    public int Damage;
    public float Firerate;
    public float RangeModifier;
}
