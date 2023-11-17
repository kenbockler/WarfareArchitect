using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public float Range;
    public float RangeModifier;
    public float FirerateModifier;
    public float DamageModifier;
    public float BulletSpeedModifier;

    public bool Seeking; // Kas kuul püüab vaenlasi targalt
    public bool Piercing; // Kas kuul saab vaenlasest läbi minna
    public bool Persistent; // Kas kuul jääb pärast teekonna lõpetamist alles, kui vaenlast ei taba
    public float Poison; // Vaenlastele on vaja atribuuti, mis iga tiksu järel neid kahjustab
    public float Slow; // Vaenlaste liikumiskiiruse muutmine; negatiivne arv mõjub hirmu- või segadusefektina

    public Foundation Foundation;
    public SupportBlock[] SupportBlocks;

    public void ComputeSupportBlocks()
    {
        // Siia tuleb kood, mis tugiplokkide arvu ja omavahelise asetuse põhjal
        // täidab suurema osa struktuuriväljadest.
        float range = Range;
        float firerate = 1;
        float damage = 1;
        float bulletspeed = 1;
        Seeking = false;
        Piercing = false;
        Persistent = false;
        float poison = 0;
        float slow = 1;

        // Kõigepealt arvestame iga tugiploki atribuute eraldi.
        foreach(SupportBlock supportblock in SupportBlocks)
        {
            if(supportblock != null)
            {
                range += supportblock.RangeModifier - 1;
                firerate += supportblock.FirerateModifier - 1;
                damage += supportblock.DamageModifier - 1;
                bulletspeed += supportblock.BulletSpeedModifier - 1;

                Seeking |= supportblock.Seeking;
                Piercing |= supportblock.Piercing;
                Persistent |= supportblock.Persistent;

                poison += supportblock.Poison;
                slow *= supportblock.Slow;
            }
        }
        Persistent &= !Piercing; // Keelab igavesed miinid, vähemalt niisama liitmisest.

        // Vahele kombinatsioonide kood. Kombinatsioon on näiteks: kui tugiplokid üle ühe on tech-tüüpi, tekivad igavesed miinid (praktikas on see näide kole, saab elegantsemalt).

        RangeModifier = range;
        FirerateModifier = firerate;
        DamageModifier = damage;
        BulletSpeedModifier = bulletspeed;
        Poison = poison;
        Slow = slow;
    }
}
