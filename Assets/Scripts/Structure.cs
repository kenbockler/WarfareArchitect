using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public float Range;
    public float RangeModifier;
    public float FirerateModifier;
    public float DamageModifier;

    public bool Seeking; // Kas kuul püüab vaenlasi targalt
    public bool Piercing; // Kas kuul saab vaenlasest läbi minna
    public bool Persistent; // Kas kuul jääb pärast teekonna lõpetamist alles, kui vaenlast ei taba
    public float Poison; // Vaenlastele on vaja atribuuti, mis iga tiksu järel neid kahjustab
    public float Slow; // Vaenlaste liikumiskiiruse muutmine; negatiivne arv mõjub hirmuefektina

    public Foundation Foundation;
    public SupportBlock[] SupportBlocks;

    public void ComputeSupportBlocks()
    {
        // Siia tuleb kood, mis tugiplokkide arvu ja omavahelise asetuse põhjal
        // täidab suurema osa struktuuriväljadest.
    }
}
