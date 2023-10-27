using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Foundation Foundation;
    public Structure Structure;
    public GunBase[] GunBase; // Mõnel tornil võib ka mitu tulistavat osa olla.
    public Gun[] Gun;
    public SupportBlock[] SupportBlocks;

    public int Damage;
    public float FireRate;
    public float Range;

    // Start is called before the first frame update
    void Start()
    {
        /*foreach(Gun gun in Gun){
            gun.Range = Structure.Range * gun.RangeModifier;
        }
        Range = Structure.Range * Gun.RangeModifier;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
