using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public TowerComponentData[] inventory = new TowerComponentData[8];
    public int Selected;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Selected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            Selected = (Selected + 1) % 8;
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            Selected = (Selected - 1) % 8;
        }
    }

    public void OnMake(TowerComponentData item)
    {
        int i;
        for(i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = item;
                break;
            }
        }
        if(i > 7) return;
        Events.SetStone(Events.GetStone() - item.Cost[0]);
        Events.SetIron(Events.GetIron() - item.Cost[1]);
        Events.SetUranium(Events.GetUranium() - item.Cost[2]);
    }
}
