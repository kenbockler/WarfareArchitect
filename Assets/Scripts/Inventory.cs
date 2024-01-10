using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    // Custom data structure for a pair
    public struct Pair<T1>
    {
        public T1 Key;

        public Pair(T1 first)
        {
            Key = first;
        }
    }


    public static Inventory instance;
    public Pair<TowerComponentData>[] inventory = new Pair<TowerComponentData>[8];

    public Image[] images = new Image[8];

    public int Selected;

    public int CurrentGameviewIndex = 0;
    public TowerComponentData CurrentBuilderItem = null;

    // Start is called before the first frame update
    void Start()
    {

        instance = this;
        Selected = 0;
        Events.TowerComponentSelected(null);

        //First, let's set all images and counts inactive (when there's no itme in the slot, let the gameobject be inactive)
        for (int i = 0; i < 8; i++)
        {
            images[i].gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta.y < 0)
        {
            Selected = (Selected + 1) % 8;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }
        if(Input.mouseScrollDelta.y > 0)
        {
            Selected = (Selected + 7) % 8;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }

        //Teeme nii, et numbritega saaks ka inventorys ringi käia
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {            
            Selected = 0;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Selected = 1;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Selected = 2;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Selected = 3;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Selected = 4;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Selected = 5;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Selected = 6;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Selected = 7;
            Events.TowerComponentSelected(inventory[Selected].Key);
        }

        CurrentBuilderItem = inventory[CurrentGameviewIndex].Key;
    }


    public void OnMake(TowerComponentData item)
    {
        //When nothing is selected and make is pressed, then it won't throw an exception
        if (item == null)
        {
            return;
        }

        //if (Events.GetStone() - item.Cost[0] >= 0 && Events.GetIron() - item.Cost[1] >= 0 && Events.GetUranium() - item.Cost[2] >= 0)
        //{
            int nullIndex = -1;

            for(int i = inventory.Length - 1; i >= 0; i--)
            {

                if (inventory[i].Key == null)
                {
                    nullIndex = i;
                }
                else
                {
                    //if the descriptions match, increase the counter of the specific slot by 1 (we are putting the item to the same slot)
                    if (inventory[i].Key.DisplayName.Equals(item.DisplayName))
                    {
                        nullIndex = -2;

                        //print("Value: " + inventory[i].Value);

                        GameviewInventory.instance.AddItem(item, i);

                        break;
                    }
                }
            }
            if(nullIndex == -1) return; //if it's -1, then return (this indicates, that there was no room in the inventory)

            if (nullIndex != -2) //if there was no item of same type in the inventory (index is not -2), but there was room for new item
            {
                //print("Null index: " + nullIndex);
                   
                Pair<TowerComponentData> pair = new(item);
                inventory[nullIndex] = pair;

                images[nullIndex].gameObject.SetActive(true);
                images[nullIndex].sprite = item.IconSprite;

                GameviewInventory.instance.AddItem(item, nullIndex);
            }

            // Kui blueprint-süsteem ei tööta, kommenteerida sisse ja võtta Builderi Build-meetodist maha.
            //Events.SetStone(Events.GetStone() - item.Cost[0]);
            //Events.SetIron(Events.GetIron() - item.Cost[1]);
            //Events.SetUranium(Events.GetUranium() - item.Cost[2]);   
        //}
    }

    public void DecrementBuymenuItemQuantity(int index)
    {
        //TODO
    }
}