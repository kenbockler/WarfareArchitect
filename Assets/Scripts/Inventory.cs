using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    // Custom data structure for a pair
    public struct Pair<T1, T2>
    {
        public T1 Key;
        public T2 Value;

        public Pair(T1 first, T2 second)
        {
            Key = first;
            Value = second;
        }
    }


    public static Inventory instance;
    public Pair<TowerComponentData, int>[] inventory = new Pair<TowerComponentData, int>[8];
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
            int nullIndex = -1;

            for(int i = inventory.Length - 1; i >= 0; i--)
            {
                //if the descriptions match, increase the counter of the specific slot by 1 (we are putting the item to the same slot)
                if (inventory[i].Key.DisplayName.Equals(item.DisplayName))
                {
                    inventory[i].Value++;
                }
            }
            if(nullIndex == -1) return; //if it's -1, then return (this indicates, that there was no room in the inventory)

            if (nullIndex != -2) //if there was no item of same type in the inventory (index is not -2), but there was room for new item
            {
                Pair<TowerComponentData, int> pair = new(item, 1);
                inventory[nullIndex] = pair;
                images[nullIndex].sprite = item.IconSprite;
            }

            Events.SetStone(Events.GetStone() - item.Cost[0]);
            Events.SetIron(Events.GetIron() - item.Cost[1]);
            Events.SetUranium(Events.GetUranium() - item.Cost[2]);
        }
    }
}
