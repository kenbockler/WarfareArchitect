using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int Damage;
    public float FireRate;
    public float Range;

    public float RangeModifier;
    public SphereCollider rangeApplied;

    public Projectile ProjectilePrefab;

    public GunBase GunBase;

    private float SpawnDelay;
    private float NextSpawnTime;

    private List<Health> targets = new List<Health>{};

    public bool Seeking; // Kas kuul püüab vaenlasi targalt
    public bool Piercing; // Kas kuul saab vaenlasest läbi minna
    public bool Persistent; // Kas kuul jääb pärast teekonna lõpetamist alles, kui vaenlast ei taba
    public float Poison; // Vaenlastele on vaja atribuuti, mis iga tiksu järel neid kahjustab
    public float Slow; // Vaenlaste liikumiskiiruse muutmine; negatiivne arv mõjub hirmuefektina

    // Siia juurde v�ib panna teisi laskmisega seotud atribuute, t�ev��rtusi.

    public AudioClipGroup GunAudio;

    // Start is called before the first frame update
    void Start()
    {
        Damage = (int) Mathf.Ceil(Damage * GunBase.DamageModifier * GunBase.Structure.DamageModifier);
        FireRate *= GunBase.FirerateModifier * GunBase.Structure.FirerateModifier;
        Range = GunBase.Structure.Range * RangeModifier;
        SpawnDelay = 1 / FireRate;
        NextSpawnTime = Time.time;

        rangeApplied = gameObject.GetComponent<SphereCollider>();
        rangeApplied.radius = Range;
    }

    // Update is called once per frame
    void Update()
    {
        Health target = GetTarget();
        if(target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Debug.DrawRay(transform.position, direction, Color.yellow);
            transform.eulerAngles = Vector3.up * angle;
        }
        if(NextSpawnTime < Time.time)
        {
            target = GetTarget();
            if(target != null)
            {
                transform.Rotate(0, Vector3.SignedAngle(transform.forward, target.transform.position - transform.position, Vector3.up), 0);
                GunAudio.Play(transform.position);
                Projectile projectile = Instantiate<Projectile>(ProjectilePrefab);
                projectile.Speed *= GunBase.BulletSpeedModifier * GunBase.Structure.BulletSpeedModifier;
                projectile.Seeking = Seeking || GunBase.Structure.Seeking;
                projectile.Piercing = Piercing || GunBase.Structure.Piercing;
                projectile.Persistent = Persistent || GunBase.Structure.Persistent;
                projectile.Poison = GunBase.Structure.Poison;
                projectile.Slow = GunBase.Structure.Slow;

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
