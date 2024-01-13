using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public Health Target;
    public Vector3 TargetPos;
    public int Damage;

    public bool Seeking; // Kas kuul p��ab vaenlasi targalt
    public bool Piercing; // Kas kuul saab vaenlasest l�bi minna
    public bool Persistent; // Kas kuul j��b p�rast teekonna l�petamist alles, kui vaenlast ei taba
    public int Poison; // Vaenlastele on vaja atribuuti, mis iga tiksu j�rel neid kahjustab
    public float Slow; // Vaenlaste liikumiskiiruse muutmine; negatiivne arv m�jub hirmuefektina

    public AudioClipGroup ExplosionAudio;

    [HideInInspector]
    public Vector3 InitialPosition; // only for the laser projectile

    [HideInInspector]
    public Vector3 InitialForward; // only for the laser projectile

    [HideInInspector]
    public LineRenderer laserLine; // only for the laser projectile

    [HideInInspector]
    public float DamageDelay; // only for the laser projectile (small number)
    private float PrevTime;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>(); // only for the laserline, for the others, it will simply stay null

        if (laserLine != null)
        {
            InitialPosition.y += 14.5f;

            laserLine.SetPosition(0, InitialPosition);
            laserLine.SetPosition(1, InitialPosition);

            PrevTime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (laserLine != null)
        {
            if (Target != null)
            {                
                laserLine.enabled = true;

                Vector3 changedPos = TargetPos;
                changedPos.y += 35;

                laserLine.SetPosition(1, changedPos);
                if (Time.time > PrevTime + DamageDelay)
                {
                    Target.Damage(Damage);
                    print(Target.HealthPoints);
                    PrevTime = Time.time;
                }
                //Target.Damage(Damage);
            }
            else
            {
                laserLine.enabled = false;
                PrevTime = Time.time;
            }            
        }

        else
        {
            if (Seeking)
            {
                if (Target != null)
                {
                    TargetPos = Target.transform.position;
                    if (Vector3.Distance(transform.position, TargetPos) < 10)
                    {
                        if (Piercing) Seeking = false; // Siin tuleks tegelikult midagi intelligentsemat teha, et ta j�rgmise vaenlase poole kihutaks.
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
                if (Vector3.Distance(transform.position, TargetPos) < 0.1 && !Persistent)
                {
                    GameObject.Destroy(gameObject);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, TargetPos, Time.deltaTime * Speed);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if(enemy != null)
        {
            if (ExplosionAudio != null)
            {
                ExplosionAudio.Play(transform.position);
            }            

            if(!Piercing) GameObject.Destroy(gameObject);
            enemy.poison += Poison;

            WaypointFollower enemyw = collision.GetComponent<WaypointFollower>();
            if(enemyw.Slow > Slow) enemyw.Slow = Slow;
            enemyw.SlowCooldown = Time.time + 5f; // See on konstant: aeglustus kestab 5 sekundit.
            enemy.Damage(Damage);
        }
    }
}
