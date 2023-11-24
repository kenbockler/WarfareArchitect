using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    public int StoneMiningSpeed;
    public int IronMiningSpeed;
    public int UraniumMiningSpeed;

    public AudioClipGroup DrillAudio;

    private bool enable;

    public void Awake()
    {
        Events.OnStartWave += Enable;
        Events.OnEndWave += Disable;
    }

    public void OnDestroy()
    {
        Events.OnStartWave -= Enable;
        Events.OnEndWave -= Disable;
    }

    // Start is called before the first frame update
    void Start()
    {
        enable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enable)
        {
            Events.SetStone(Events.GetStone() + Time.deltaTime * StoneMiningSpeed / 60);
            Events.SetIron(Events.GetIron() + Time.deltaTime * IronMiningSpeed / 60);
            Events.SetUranium(Events.GetUranium() + Time.deltaTime * UraniumMiningSpeed / 60);
            DrillAudio.Play(transform.position);
        }
    }

    void Enable(WaveData data)
    {
        enable = true;
    }

    void Disable(WaveData data)
    {
        enable = false;
    }
}
