using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint Next;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Waypoint GetNextWaypoint()
    {
        return Next;
    }

    private void OnDrawGizmos()
    {
        if(Next == null) return;
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, Next.transform.position);
    }
}
