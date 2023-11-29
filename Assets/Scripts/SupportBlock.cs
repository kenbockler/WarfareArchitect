using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportBlock : MonoBehaviour
{
    [Delayed]
    public string id;
    [Space]

    [Range(0.5f, 2)]
    [DelayedAttribute] // Delayed ei tööta hästi! See siin samuti.
    [Tooltip("Additively modifies the tower's base range. For example, 1.2 increases range by 20% of the base.")]
    public float RangeModifier;

    [Range(0.5f, 2)]
    [DelayedAttribute]
    [Tooltip("Additively modifies the tower's base firerate. For example, 1.2 increases firerate by 20% of the base.")]
    public float FirerateModifier;

    [Range(0.5f, 2)]
    [DelayedAttribute]
    [Tooltip("Additively modifies the tower's base damage. For example, 1.2 increases damage by 20% of the base.")]
    public float DamageModifier;

    [Range(0.5f, 2)]
    [DelayedAttribute]
    [Tooltip("Additively modifies the tower's base bullet speed. For example, 1.2 increases bullet speed by 20% of the base.")]
    public float BulletSpeedModifier;
    [Space]
    [Tooltip("Whether bullets move intelligently towards enemies.")]
    public bool Seeking; // Kas kuul p��ab vaenlasi targalt
    [Tooltip("Piercing bullets are not destroyed after hitting an enemy.")]
    public bool Piercing; // Kas kuul saab vaenlasest l�bi minna
    [Tooltip("Persistent bullets linger on the ground like mines.")]
    public bool Persistent; // Kas kuul j��b p�rast teekonna l�petamist alles, kui vaenlast ei taba
    [Space]
    [Range(0, 10)]
    [DelayedAttribute]
    public float Poison; // Vaenlastele on vaja atribuuti, mis iga tiksu j�rel neid kahjustab

    [Range(-1, 1)]
    [Delayed]
    [Tooltip("Modifies hit enemies' moving speed. Negative values cause confusion and fear effects.")]
    public float Slow; // Vaenlaste liikumiskiiruse muutmine; negatiivne arv m�jub hirmuefektina
}
