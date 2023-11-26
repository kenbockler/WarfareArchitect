using System;
using System.Collections;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public Waypoint Next;

    public float Speed = 50f;

    public Animator animator;

    private Health h;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        h = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Next != null && !h.IsDead)
        {
            if(Vector3.Distance(transform.position, Next.transform.position) < 0.1)
            {
                Next = Next.GetNextWaypoint();
                if(Next == null && !h.IsDead)
                {
                    //After playing the death animation, we should destroy the gameobject
                    //animator.SetTrigger("Death");
                    //Destroy(gameObject);
                    //Events.SetLives(Events.GetLives() - 1);
                    h.IsDead = true;
                    StartCoroutine(WaitForDeathAnimation());
                }
            }
            if (Next != null && !h.IsDead) { 
                transform.position = Vector3.MoveTowards(transform.position, Next.transform.position, Time.deltaTime * Speed);

                Vector3 vec = new Vector3(Next.transform.position.x - transform.position.x, 0, Next.transform.position.z - transform.position.z);
                Quaternion rot;
                rot = vec == Vector3.zero ? Quaternion.Euler(vec) : Quaternion.LookRotation(vec);

                //Quaternion rot = Quaternion.LookRotation(new Vector3(Next.transform.position.x - transform.position.x, 0, Next.transform.position.z - transform.position.z));
                transform.rotation = rot;
            }
        }        
    }

    IEnumerator WaitForDeathAnimation()
    {
        // Play the death animation
        animator.SetTrigger("Death");

        // Wait for the length of the death animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destroy the GameObject after the animation has finished
        Destroy(gameObject);

        // Update other game-related events, e.g., decrement lives
        Events.SetLives(Events.GetLives() - 1);
    }
}
