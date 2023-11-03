using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public KeyValuePair<TowerComponentData, int>[] inventory = new KeyValuePair<TowerComponentData, int>[8];
    public Image[] images = new Image[8];
    public int Selected;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Selected = 0;
        Events.TowerComponentSelected(null);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            Selected = (Selected + 1) % 8;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            Selected = (Selected + 7) % 8;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }
    }

    public void OnMake(TowerComponentData item)
    {
        if(Events.GetStone() - item.Cost[0] >= 0 && Events.GetIron() - item.Cost[1] >= 0 && Events.GetUranium() - item.Cost[2] >= 0)
        {
            int i;
            int nullIndex;

            for(i = 0; i < inventory.Length; i++)
            {
                if(inventory[i].Key == null)
                {
                    nullIndex = i;

                    KeyValuePair<TowerComponentData, int> pair = new KeyValuePair<TowerComponentData, int>(item, 1);
                    inventory[i] = pair;

                    images[i].sprite = item.IconSprite;
                    break;
                }
            }
            if(i > 7) return; //the null index does not exist and all inventory slots are full            

            Events.SetStone(Events.GetStone() - item.Cost[0]);
            Events.SetIron(Events.GetIron() - item.Cost[1]);
            Events.SetUranium(Events.GetUranium() - item.Cost[2]);
        }
    }
}
