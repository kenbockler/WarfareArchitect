using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public Health Target;
    public Vector3 TargetPos;
    public int Damage = 1;
    public bool Seeking;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Seeking)
        {
            if(Target != null)
            {
                TargetPos = Target.transform.position;
                if(Vector3.Distance(transform.position, TargetPos) < 10)
                {
                    GameObject.Destroy(gameObject);
                    Target.Damage(Damage);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, TargetPos, Time.deltaTime * Speed);
                }
            }
            else
            {
                Seeking = false;
            }
        }
        else
        {
            if(Vector3.Distance(transform.position, TargetPos) < 0.1)
            {
                GameObject.Destroy(gameObject);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, TargetPos, Time.deltaTime * Speed);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if(enemy != null)
        {
            GameObject.Destroy(gameObject);
            enemy.Damage(Damage);
        }
    }
}
