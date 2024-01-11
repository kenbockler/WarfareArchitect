using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Kuna need väärtused saadakse koodi sees, ei peaks neid editoris näitama
    [HideInInspector]
    public int Damage;

    [HideInInspector]
    public float FireRate;

    [HideInInspector]
    public float Range;

    [HideInInspector]
    public SphereCollider rangeApplied;

    [HideInInspector] // Seda pole hetkel vaja editoris näidata
    public Vector3 InitialRotation = Vector3.zero;

    // Kuna see lisatakse läbi builderi (ehk ka koodi sees), siis pole seda vaja editoris näidata
    [HideInInspector]
    public GunBase GunBase;

    [Tooltip("Name of the gun for the animating part")]
    public String Name;

    [Space]
    [Header("Multiplicative Tower Modifiers From This Gun's Prefab")]
    [Space]

    [Range(0.01f, 100)]
    [Tooltip("Multiplicatively increases the tower's RANGE. For example setting this value from 1 to 2 will double the tower's RANGE.")]
    public float RangeModifier;

    [Range(0.01f, 100)]
    [Tooltip("Multiplicatively increases the tower's DAMAGE. For example setting this value from 1 to 2 will double the tower's DAMAGE.")]
    public float DamageModifier;

    [Range(0.01f, 100)]
    [Tooltip("Multiplicatively increases the tower's FIRERATE. For example setting this value from 1 to 2 will double the tower's FIRERATE.")]
    public float FirerateModifier;

    [Space]
    [Space]
    [Header("Tower Effects From This Gun's Prefab")]
    [Space]

    [Tooltip("If set to true, the projectile will chase the enemy.")]
    public bool Seeking; // Kas kuul püüab vaenlasi targalt
    [Tooltip("If set to true, the projectile can go through the enemies (won't be destroyed by hitting the first enemy).")]
    public bool Piercing; // Kas kuul saab vaenlasest läbi minna
    [Tooltip("If set to true, the projectile will stay on the ground and damage enemies who step on it.")]
    public bool Persistent; // Kas kuul jääb pärast teekonna lõpetamist alles, kui vaenlast ei taba
    [Range(0,10)]
    [Tooltip("If set to higher than 0, does this damage to the enemy after the projectile hit each second.")]
    public float Poison; // Vaenlastele on vaja atribuuti, mis iga tiksu järel neid kahjustab
    [Range(-1, 1)]
    [Tooltip("If set below than 1, the projectile hit will make the enemy move slower. Negative values can be used for confusion and fear effects.")]   
    public float Slow; // Vaenlaste liikumiskiiruse muutmine; negatiivne arv mõjub hirmuefektina
    public int Targets;

    // Siia juurde v�ib panna teisi laskmisega seotud atribuute, t�ev��rtusi.

    [Space]
    [Space]
    [Header("Prefabs Used for This Gun's Prefab")]
    [Space]

    [Tooltip("Projectile prefab used for this gun's prefab.")]    
    public Projectile ProjectilePrefab;
    [Tooltip("Audio (audio clip group) used for this gun's shooting.")]
    public AudioClipGroup GunAudio;   

    // Privaatsed isendiväljad
    private float SpawnDelay;
    private float NextSpawnTime;
    private List<Health> targets = new List<Health> { };

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

        Seeking = GunBase.Structure.Seeking;
        Piercing = GunBase.Structure.Piercing;
        Persistent = GunBase.Structure.Persistent;

        Targets = GunBase.Structure.Targets;
        SpawnDelay = 1 / FireRate;
        NextSpawnTime = Time.time;

        rangeApplied = gameObject.GetComponent<SphereCollider>();
        rangeApplied.radius = Range;
    }

    // Update is called once per frame
    void Update()
    {
        Health[] targets = GetTargets();
        if(targets[0] != null)
        {
            //Vector3 direction = target.transform.position - transform.position;
            //float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //Debug.DrawRay(transform.position, direction, Color.yellow);
            //Vector3 korrutatav = new (-1, 1, 0);
            //transform.GetChild(0).eulerAngles = korrutatav * angle;
            //transform.GetChild(2).eulerAngles = Vector3.up * angle;

            //Animeerimiseks
            switch (name)
            {
                case "MachineGun":
                    Transform child0 = transform.GetChild(0);
                    Transform child2 = transform.GetChild(2);

                    Vector3 vec = new Vector3(targets[0].transform.position.x - transform.position.x, targets[0].transform.position.y - transform.position.y, targets[0].transform.position.z - transform.position.z);

                    Quaternion targetRotation;
                    targetRotation = vec == Vector3.zero ? Quaternion.Euler(vec) : Quaternion.LookRotation(vec);

                    //Quaternion targetRotation = Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, target.transform.position.z - transform.position.z));
                    child0.transform.rotation = targetRotation;

                    Vector3 eulerA = targetRotation.eulerAngles;
                    eulerA.x = 0;
                    eulerA.z = 0;
                    child2.transform.rotation = Quaternion.Euler(eulerA);
                    break;

                case "Irradiator":
                    break;

                case "RocketLauncher":
                    break;

                case "LaserGun":
                    break;

                default:
                    break;
            }
        }
        else
        {
            //transform.GetChild(0).eulerAngles = Vector3.zero;
            //transform.GetChild(2).eulerAngles = Vector3.zero;

            float rotationSpeed = 3.0f;

            //Animeerimiseks
            switch (name)
            {
                case "MachineGun":
                    Transform child0 = transform.GetChild(0);
                    Transform child2 = transform.GetChild(2);

                    child0.rotation = Quaternion.Lerp(child0.rotation, Quaternion.Euler(InitialRotation), Time.deltaTime * rotationSpeed);
                    child2.rotation = Quaternion.Lerp(child2.rotation, Quaternion.Euler(InitialRotation), Time.deltaTime * rotationSpeed);
                    break;

                case "Irradiator":
                    break;

                case "RocketLauncher":
                    break;

                case "LaserGun":
                    break;

                default:
                    break;
            }
        }
        if(NextSpawnTime < Time.time)
        {
            targets = GetTargets();
            if(targets.Length > 0)
            {
                foreach(Health target in targets)
                {
                    if(target != null && !target.IsDead)
                    {
                        GunAudio.Play(transform.position);
                        Projectile projectile = Instantiate<Projectile>(ProjectilePrefab);
                        projectile.Damage = Damage;
                        projectile.Speed *= GunBase.BulletSpeedModifier * GunBase.Structure.BulletSpeedModifier;
                        projectile.Seeking = Seeking || GunBase.Structure.Seeking;
                        projectile.Piercing = Piercing || GunBase.Structure.Piercing;
                        projectile.Persistent = Persistent || GunBase.Structure.Persistent;
                        projectile.Poison = (int)GunBase.Structure.Poison;
                        projectile.Slow = GunBase.Structure.Slow;

                        projectile.transform.position = transform.position;
                        projectile.Target = target;
                        projectile.TargetPos = target.transform.position;
                        NextSpawnTime = Time.time + SpawnDelay;
                    }
                }
            }
            else
            {
                NextSpawnTime = Time.time;
            }
        }
        CleanTargets();
    }

    public Health[] GetTargets()
    {
        Health[] targetArray = new Health[Math.Max(targets.Count, Targets)];
        int targetNumber = Targets;
        for(int i = 0; i < targets.Count; i++)
        {
            if(targetNumber > 0)
            {
                targetArray[i] = targets[i];
                targetNumber--;
            }
        }
        return targetArray;
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

    private void CleanTargets()
    {
        foreach(Health t in targets)
        {
            if(t == null)
            {
                targets.Remove(t);
            }
        }
    }

    private void PlaceSupportBlock(bool b)
    {
        Start();
    }
}
