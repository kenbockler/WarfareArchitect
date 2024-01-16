using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [HideInInspector]
    public WaypointFollower FollowerPrefab = null;
    public float SpawnDelay = 5f;

    private float NextSpawnTime;
    private Waypoint waypoint;

    public WaveData waveData;
    public int count = 0;

    public void Awake()
    {
        Events.OnStartWave += StartWave;
    }
    public void OnDestroy()
    {
        Events.OnStartWave -= StartWave;
    }

    // Start is called before the first frame update
    void Start()
    {
        NextSpawnTime = Time.time;
        waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(NextSpawnTime < Time.time && count > 0)
        {
            WaypointFollower follower = Instantiate<WaypointFollower>(FollowerPrefab);
            follower.transform.position = transform.position;
            follower.Next = waypoint;
            NextSpawnTime += SpawnDelay;
            count -= 1;
        }
        if(count <= 0)
        {
            Events.EndWave(waveData);
        }
    }

    void StartWave(WaveData data)
    {
        waveData = data;
        count = data.Count;
        FollowerPrefab = data.EnemyData.EnemyPrefab;
        SpawnDelay = data.SpawnDelay;
        NextSpawnTime = Time.time;
    }
}
