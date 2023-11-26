using System;
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

    public float DamageModifier;
    public float FirerateModifier;

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

    public Vector3 InitialRotation = Vector3.zero;

    public void Awake()
    {
        Events.OnPlaceSupportBlock += PlaceSupportBlock;
    }
    public void OnDestroy()
    {
        Events.OnPlaceSupportBlock -= PlaceSupportBlock;
    }

    // Start is called before the first frame update
    void Start()
    {
        Damage = (int) Mathf.Ceil(DamageModifier * GunBase.DamageModifier * GunBase.Structure.DamageModifier);
        FireRate = FirerateModifier * GunBase.FirerateModifier * GunBase.Structure.FirerateModifier;
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
            //Vector3 direction = target.transform.position - transform.position;
            //float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //Debug.DrawRay(transform.position, direction, Color.yellow);
            //Vector3 korrutatav = new (-1, 1, 0);
            //transform.GetChild(0).eulerAngles = korrutatav * angle;
            //transform.GetChild(2).eulerAngles = Vector3.up * angle;

            Transform child0 = transform.GetChild(0);
            Transform child2 = transform.GetChild(2);

            Vector3 vec = new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, target.transform.position.z - transform.position.z);

            Quaternion targetRotation;
            targetRotation = vec == Vector3.zero ? Quaternion.Euler(vec) : Quaternion.LookRotation(vec);

            //Quaternion targetRotation = Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, target.transform.position.z - transform.position.z));
            child0.transform.rotation = targetRotation;

            Vector3 eulerA = targetRotation.eulerAngles;
            eulerA.x = 0;
            eulerA.z = 0;
            child2.transform.rotation = Quaternion.Euler(eulerA);
        }
        else
        {
            //transform.GetChild(0).eulerAngles = Vector3.zero;
            //transform.GetChild(2).eulerAngles = Vector3.zero;

            float rotationSpeed = 3.0f;

            Transform child0 = transform.GetChild(0);
            Transform child2 = transform.GetChild(2);

            child0.rotation = Quaternion.Lerp(child0.rotation, Quaternion.Euler(InitialRotation), Time.deltaTime * rotationSpeed);
            child2.rotation = Quaternion.Lerp(child2.rotation, Quaternion.Euler(InitialRotation), Time.deltaTime * rotationSpeed);
        }
        if(NextSpawnTime < Time.time)
        {
            target = GetTarget();
            if(target != null && !target.IsDead)
            {
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
            if(target != null && !target.IsDead) return target;
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

    private void PlaceSupportBlock(bool b)
    {
        Start();
    }
}
