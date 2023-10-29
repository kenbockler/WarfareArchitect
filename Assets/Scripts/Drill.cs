using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    public int StoneMiningSpeed;
    public int IronMiningSpeed;
    public int UraniumMiningSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Events.SetStone(Events.GetStone() + Time.deltaTime * StoneMiningSpeed / 60);
        Events.SetIron(Events.GetIron() + Time.deltaTime * IronMiningSpeed / 60);
        Events.SetUranium(Events.GetUranium() + Time.deltaTime * UraniumMiningSpeed / 60);
    }
}