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
    public int Targets; // Sihtmärkide arv, keda korraga lasta saab.

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
        int targets = 1;

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
        // KAKS TEINETEISE VASTAS
        if(OverEvery(6, "TechBlock"))
        {
            // Ei anna midagi.
        }
        if(OverEvery(6, "SpeedBlock"))
        {
            firerate *= 1.05f;
        }
        if(OverEvery(6, "PowerBlock"))
        {
            damage *= 1.05f;
        }
        if(OverEvery(6, "TechBlock") && OverEvery(6, "PowerBlock"))
        {
            slow = 0.8f;
        }
        if(OverEvery(6, "TechBlock") && OverEvery(6, "SpeedBlock"))
        {
            range *= 1.1f;
        }
        if(OverEvery(6, "PowerBlock") && OverEvery(6, "SpeedBlock"))
        {
            range *= 1.1f;
        }
        if(OverEvery(6, "TechBlock") && OverEvery(6, "PowerBlock") && OverEvery(6, "SpeedBlock"))
        {
            targets += 1;
        }

        // IGA NELJAS - KOLMNURK
        if(OverEvery(4, "TechBlock"))
        {
            range *= 1.2f;
        }
        if(OverEvery(4, "SpeedBlock"))
        {
            firerate *= 1.15f;
        }
        if(OverEvery(4, "PowerBlock"))
        {
            damage *= 1.15f;
        }
        if(OverEvery(4, "TechBlock") && OverEvery(4, "PowerBlock"))
        {
            damage *= 1.1f;
            slow *= 0.75f;
        }
        if(OverEvery(4, "TechBlock") && OverEvery(4, "SpeedBlock"))
        {
            firerate *= 1.1f;
            range *= 1.1f;
        }
        if(OverEvery(4, "PowerBlock") && OverEvery(4, "SpeedBlock"))
        {
            range *= 1.2f;
        }
        if(OverEvery(4, "TechBlock") && OverEvery(4, "PowerBlock") && OverEvery(4, "SpeedBlock"))
        {
            targets += 2;
        }

        // IGA KOLMAS - RUUT
        if(OverEvery(3, "TechBlock"))
        {
            Persistent = true;
            Piercing = false;
        }
        if(OverEvery(3, "SpeedBlock"))
        {
            firerate *= 1.25f;
        }
        if(OverEvery(3, "PowerBlock"))
        {
            damage *= 1.25f;
        }
        if(OverEvery(3, "TechBlock") && OverEvery(3, "PowerBlock"))
        {
            slow = 0.5f;
        }
        if(OverEvery(3, "TechBlock") && OverEvery(3, "SpeedBlock"))
        {
            firerate *= 1.25f;
            bulletspeed *= 2;
        }
        if(OverEvery(3, "PowerBlock") && OverEvery(3, "SpeedBlock"))
        {
            range *= 1.25f;
        }
        if(OverEvery(3, "TechBlock") && OverEvery(3, "PowerBlock") && OverEvery(3, "SpeedBlock"))
        {
            targets = 1000;
        }

        // IGA TEINE
        if(OverEvery(2, "TechBlock"))
        {
            bulletspeed *= 2;
        }
        if(OverEvery(2, "SpeedBlock"))
        {
            firerate *= 1.35f;
        }
        if(OverEvery(2, "PowerBlock"))
        {
            damage *= 1.35f;
        }
        if(OverEvery(2, "TechBlock") && OverEvery(2, "PowerBlock"))
        {
            Slow *= -1f;
        }
        if(OverEvery(2, "TechBlock") && OverEvery(2, "SpeedBlock"))
        {
            firerate *= 1.2f;
            Piercing = true;
        }
        if(OverEvery(2, "PowerBlock") && OverEvery(2, "SpeedBlock"))
        {
            range *= 1.5f;
            firerate *= 1.1f;
            damage *= 1.1f;
            //bulletspeed *= 4;
        }

        // KÕIK
        if(OverEvery(1, "TechBlock"))
        {
            Piercing = true;
            Persistent = true;
        }
        if(OverEvery(1, "SpeedBlock"))
        {
            firerate *= 1.45f;
        }
        if(OverEvery(1, "PowerBlock"))
        {
            damage *= 1.45f;
        }

        RangeModifier = range;
        FirerateModifier = firerate;
        DamageModifier = damage;
        BulletSpeedModifier = bulletspeed;
        Poison = poison;
        Slow = slow;
        Targets = targets;
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
