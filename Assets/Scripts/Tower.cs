using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public FoundationData Foundation;
    public StructureData Structure;
    public GunBaseData[] GunBase; // Mõnel tornil võib ka mitu tulistavat osa olla.
    public GunData[] Gun;
    public SupportBlockData[] SupportBlocks;

    public Gun[] GunScript;

    public int Damage;
    public float FireRate;
    public float Range;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
