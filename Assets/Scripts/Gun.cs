using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int Damage;
    public float FireRate;
    public float Range;

    public float RangeModifier;

    public Projectile ProjectilePrefab;

    public GunBase GunBase;

    private float SpawnDelay;
    private float NextSpawnTime;

    private List<Health> targets = new List<Health>{};

    public bool Seeking;

    // Siia juurde v�ib panna teisi laskmisega seotud atribuute, t�ev��rtusi.

    // Start is called before the first frame update
    void Start()
    {
        Damage = (int) Mathf.Ceil(Damage * GunBase.DamageModifier);
        FireRate *= GunBase.FirerateModifier;
        Range = GunBase.Structure.Range * RangeModifier;
        SpawnDelay = 1 / FireRate;
        NextSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(NextSpawnTime < Time.time)
        {
            Health target = GetTarget();
            if(target != null)
            {
                Projectile projectile = Instantiate<Projectile>(ProjectilePrefab);
                projectile.Speed *= GunBase.BulletSpeedModifier;
                projectile.Seeking = Seeking;
                projectile.transform.position = transform.position;
                projectile.Target = target;
                projectile.TargetPos = target.transform.position;
                NextSpawnTime += SpawnDelay;
            }
            else
            {
                NextSpawnTime = Time.time;
            }
        }
    }

    public Health GetTarget()
    {
        foreach(Health target in targets)
        {
            if(target != null) return target;
        }
        return null;
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        Health health = collision.GetComponent<Health>();
        if(health != null)
        {
            targets.Add(health);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Health health = collision.GetComponent<Health>();
        if(health != null)
        {
            targets.Remove(health);
        }
    }
}
