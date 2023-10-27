using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int Damage;
    public float FireRate;
    public float Range;

    public Projectile ProjectilePrefab;
    public float SpawnDelay = 0.5f;

    private float NextSpawnTime;

    private List<Health> targets = new List<Health>{};

    // Siia juurde vıib panna teisi laskmisega seotud atribuute, tıev‰‰rtusi.

    // Start is called before the first frame update
    void Start()
    {
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
                projectile.transform.position = transform.position;
                projectile.Target = target;
                NextSpawnTime += SpawnDelay;
            }
        }
    }

    public Health GetTarget()
    {
        foreach(Health target in targets)
        {
            return target;
        }
        return null;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if(health != null)
        {
            targets.Add(health);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if(health != null)
        {
            targets.Remove(health);
        }
    }
}
