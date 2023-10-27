using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbProjectile : MonoBehaviour
{
    public float Speed = 9;
    public Vector3 Target;
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
            if(Vector3.Distance(transform.position, Target) < 0.1)
            {
                GameObject.Destroy(gameObject);
            }
            else
            {
                 transform.position = Vector3.MoveTowards(transform.position, Target, Time.deltaTime * Speed);
            }
        }
        else
        {
            GameObject.Destroy(gameObject);
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
