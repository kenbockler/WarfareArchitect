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

    public bool Seeking; // Kas kuul p��ab vaenlasi targalt
    public bool Piercing; // Kas kuul saab vaenlasest l�bi minna
    public bool Persistent; // Kas kuul j��b p�rast teekonna l�petamist alles, kui vaenlast ei taba
    public float Poison; // Vaenlastele on vaja atribuuti, mis iga tiksu j�rel neid kahjustab
    public float Slow; // Vaenlaste liikumiskiiruse muutmine; negatiivne arv m�jub hirmu- v�i segadusefektina

    public Foundation Foundation;
    public SupportBlock[] SupportBlocks;

    public void ComputeSupportBlocks()
    {
        // Siia tuleb kood, mis tugiplokkide arvu ja omavahelise asetuse p�hjal
        // t�idab suurema osa struktuuriv�ljadest.
        float range = Range;
        float firerate = 1;
        float damage = 1;
        float bulletspeed = 1;
        Seeking = false;
        Piercing = false;
        Persistent = false;
        float poison = 0;
        float slow = 1;

        // K�igepealt arvestame iga tugiploki atribuute eraldi.
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
        Persistent &= !Piercing; // Keelab igavesed miinid, v�hemalt niisama liitmisest.

        // Vahele kombinatsioonide kood. Kombinatsioon on n�iteks: kui tugiplokid �le �he on tech-t��pi, tekivad igavesed miinid (praktikas on see n�ide kole, saab elegantsemalt).
        if(OverEvery(1, "TechBlock"))
        {
            Piercing = true;
            Persistent = true;
        }
        if(OverEvery(1, "SpeedBlock"))
        {
            FirerateModifier *= 5;
            BulletSpeedModifier *= 5;
        }
        if(OverEvery(1, "PowerBlock"))
        {
            DamageModifier *= 5;
            BulletSpeedModifier *= 3;
        }

        RangeModifier = range;
        FirerateModifier = firerate;
        DamageModifier = damage;
        BulletSpeedModifier = bulletspeed;
        Poison = poison;
        Slow = slow;
    }

    private bool All(string id) // See peaks v6rduma k2suga OverEvery(1, id).
    {
        foreach(SupportBlock supportblock in SupportBlocks)
        {
            if(supportblock == null || supportblock.id != id) return false;
        }
        return true;
    }

    private bool OverEvery(int n, string id) // 12 ploki puhul huvitavad meid n väärtused 1, 2, 3, 4, 6.
    {
        int i = 0;
        bool[] b = new bool[n];
        for(int j = 0; j < n; j += 1) b[j] = true;
        foreach(SupportBlock supportblock in SupportBlocks){
            for(int j = 0; j < n; j += 1)
            {
                if(i % n == j && (supportblock == null || supportblock.id != id)) b[j] = false;
            }
            i += 1;
        }
        foreach(bool answer in b)
        {
            if(answer) return answer;
        }
        return false;
    }
}
