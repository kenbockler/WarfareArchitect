using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public Waypoint Next;

    public float Speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Next != null)
        {
            if(Vector3.Distance(transform.position, Next.transform.position) < 0.1)
            {
                Next = Next.GetNextWaypoint();
                if(Next == null) Destroy(gameObject);
            }
            transform.position = Vector3.MoveTowards(transform.position, Next.transform.position, Time.deltaTime * Speed);
        }
    }
}
