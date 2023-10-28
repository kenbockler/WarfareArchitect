using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 9;
    public Health Target;
    public int Damage = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            if(Vector3.Distance(transform.position, Target.transform.position) < 10)
            {
                GameObject.Destroy(gameObject);
                Target.Damage(Damage);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * Speed);
            }
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
}
