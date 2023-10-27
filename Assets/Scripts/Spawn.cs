using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public WaypointFollower FollowerPrefab;
    public float SpawnDelay = 5f;

    private float NextSpawnTime;
    private Waypoint waypoint;

    // Start is called before the first frame update
    void Start()
    {
        NextSpawnTime = Time.time;
        waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(NextSpawnTime < Time.time)
        {
            WaypointFollower follower = Instantiate<WaypointFollower>(FollowerPrefab);
            follower.transform.position = transform.position;
            follower.Next = waypoint;
            NextSpawnTime += SpawnDelay;
        }
    }
}
