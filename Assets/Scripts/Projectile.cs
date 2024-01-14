using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.ProBuilder;

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

    public AudioClipGroup ProjectileHittingEnemyAudio;

    [HideInInspector]
    public Vector3 InitialPosition; // only for the laser projectile

    [HideInInspector]
    public Vector3 InitialForward; // only for the laser projectile

    [HideInInspector]
    public LineRenderer laserLine; // only for the laser projectile

    [HideInInspector]
    public float DamageDelay; // only for the laser projectile (small number)
    private float PrevTime;
   
    public bool IsRocket; // only for the explosive projectile
    public ParticleController Particle_Controller; // only for the explosive projectile

    public bool IsIrradiator; // only for irradiator projectile
    public int IrradiatorHitsAllowed; // only for irradiator projectile

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

        if (IsRocket)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        }

        TargetPos = new Vector3(TargetPos.x, TargetPos.y + 20, TargetPos.z);
        if (IsIrradiator)
        {
            TargetPos.y += 25;
            MeshRenderer mr = GetComponent<MeshRenderer>();

            Material originalMat = mr.materials[0];

            Material newMat = new Material(originalMat);

            mr.materials[0] = newMat;
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
                    //print(Target.HealthPoints); // for debugging
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
                    if (IsIrradiator)
                    {
                        Vector3 rot = Quaternion.LookRotation(TargetPos - transform.position).eulerAngles;
                        rot.x += 50;
                        transform.rotation = Quaternion.Euler(rot);

                        float currentScale = transform.localScale.x;
                        currentScale = Mathf.Clamp(currentScale + 100f * Time.deltaTime, 1, 80);
                        transform.localScale = new Vector3(currentScale, 0.1f, currentScale);
                    }
                    if (IsRocket)
                    {
                        Vector3 rot = Quaternion.LookRotation(TargetPos - transform.transform.position).eulerAngles;
                        rot.x -= 90;
                        transform.rotation = Quaternion.Euler(rot);
                    }

                    if (Vector3.Distance(transform.position, TargetPos) < 10)
                    {
                        if (IsRocket)
                        {
                            FindAllEnemisInExplosionRadiusAndDamageThem();
                        }
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
                    if (IsRocket)
                    {                        
                        FindAllEnemisInExplosionRadiusAndDamageThem();
                    }

                    if (IsIrradiator)
                    {
                        //siia tuleb hiljem midagi paremat kirjutada                        
                        if (IrradiatorHitsAllowed <= 0 && !Piercing)
                        {
                            print("DEBUG");
                            GameObject.Destroy(gameObject);
                        }                        
                    }
                    else
                    {
                        GameObject.Destroy(gameObject);
                    }                    
                }
                else
                {
                    if (IsIrradiator)
                    {
                        float currentScale = transform.localScale.x;
                        currentScale = Mathf.Clamp(currentScale + 100f * Time.deltaTime, 1, 80);
                        transform.localScale = new Vector3(currentScale, 0.1f, currentScale);
                    }
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
            if (IsRocket)
            {                
                FindAllEnemisInExplosionRadiusAndDamageThem();
            }

            if (ProjectileHittingEnemyAudio != null)
            {
                ProjectileHittingEnemyAudio.Play(transform.position);
            }            
            
            enemy.poison += Poison;

            WaypointFollower enemyw = collision.GetComponent<WaypointFollower>();
            if(enemyw.Slow > Slow) enemyw.Slow = Slow;
            enemyw.SlowCooldown = Time.time + 5f; // See on konstant: aeglustus kestab 5 sekundit.
            enemy.Damage(Damage);

            if (IsIrradiator)
            {
                IrradiatorHitsAllowed--;

                if (!Piercing)
                {
                    MeshRenderer mr = GetComponent<MeshRenderer>();
                    Material mat = mr.materials[0];
                    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a / 2f);
                }

                if (IrradiatorHitsAllowed <= 0 && !Piercing) GameObject.Destroy(gameObject);
            }
            else
            {
                if (!Piercing) GameObject.Destroy(gameObject);
            }
        }
    }

    private void FindAllEnemisInExplosionRadiusAndDamageThem()
    {
        // Instantiate particelcontroller gameobject
        ParticleController pc = GameObject.Instantiate<ParticleController>(Particle_Controller);
        pc.SetPositionAndPlay(transform.position, transform.localScale);

        float sphereRadius = transform.GetChild(0).GetComponent<SphereCollider>().radius;

        // Find all colliders within the sphere
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius * transform.localScale.x);
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Health>() != null)
            {
                Health enemy = collider.GetComponent<Health>();
                if (enemy != null && !enemy.IsDead)
                {

                    //Let the explosion damage be half the damage of the rocket itself
                    enemy.Damage(Mathf.CeilToInt(Damage/2));
                }
            }
        }
    }
}
