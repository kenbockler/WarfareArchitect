using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportBlock : MonoBehaviour
{
    public float RangeModifier;
    public float FirerateModifier;
    public float DamageModifier;
    public float BulletSpeedModifier;

    public bool Seeking; // Kas kuul püüab vaenlasi targalt
    public bool Piercing; // Kas kuul saab vaenlasest läbi minna
    public bool Persistent; // Kas kuul jääb pärast teekonna lõpetamist alles, kui vaenlast ei taba
    public float Poison; // Vaenlastele on vaja atribuuti, mis iga tiksu järel neid kahjustab
    public float Slow; // Vaenlaste liikumiskiiruse muutmine; negatiivne arv mõjub hirmuefektina
}
