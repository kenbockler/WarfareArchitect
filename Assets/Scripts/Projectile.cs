using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public Health Target;
    public Vector3 TargetPos;
    public int Damage = 1;

    public bool Seeking; // Kas kuul püüab vaenlasi targalt
    public bool Piercing; // Kas kuul saab vaenlasest läbi minna
    public bool Persistent; // Kas kuul jääb pärast teekonna lõpetamist alles, kui vaenlast ei taba
    public float Poison; // Vaenlastele on vaja atribuuti, mis iga tiksu järel neid kahjustab
    public float Slow; // Vaenlaste liikumiskiiruse muutmine; negatiivne arv mõjub hirmuefektina
    
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
                    if(Piercing) Seeking = false; // Siin tuleks tegelikult midagi intelligentsemat teha, et ta järgmise vaenlase poole kihutaks.
                    else GameObject.Destroy(gameObject);
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
            if(Vector3.Distance(transform.position, TargetPos) < 0.1 && !Persistent)
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
            if(!Piercing) GameObject.Destroy(gameObject);
            enemy.Damage(Damage);
        }
    }
}
