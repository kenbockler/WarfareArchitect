using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameviewInventory : MonoBehaviour
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

    public static GameviewInventory instance;
    public Pair<TowerComponentData>[] inventory = new Pair<TowerComponentData>[8];

    public Image[] images = new Image[8];
    public GameObject[] greenGlows = new GameObject[8];

    public int Selected;    

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {       
        Selected = 0;      
        Events.TowerComponentSelected(null);

        //First, let's set all images and counts inactive (when there's no itme in the slot, let the gameobject be inactive)
        for (int i = 0; i < 8; i++)
        {
            images[i].gameObject.SetActive(false);
            greenGlows[i].SetActive(false);
        }

        InventoryItemSelected(Selected);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            Selected = (Selected + 1) % 8;
            InventoryItemSelected(Selected);
            Events.TowerComponentSelected(inventory[Selected].Key);

            IsNullSetBuilderFalse(inventory[Selected].Key);
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            Selected = (Selected + 7) % 8;
            InventoryItemSelected(Selected);
            Events.TowerComponentSelected(inventory[Selected].Key);

            IsNullSetBuilderFalse(inventory[Selected].Key);
        }

        //Teeme nii, et numbritega saaks ka inventorys ringi käia
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Selected = 0;
            InventoryItemSelected(Selected);
            Events.TowerComponentSelected(inventory[Selected].Key);

            IsNullSetBuilderFalse(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Selected = 1;
            InventoryItemSelected(Selected);
            Events.TowerComponentSelected(inventory[Selected].Key);

            IsNullSetBuilderFalse(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Selected = 2;
            InventoryItemSelected(Selected);
            Events.TowerComponentSelected(inventory[Selected].Key);

            IsNullSetBuilderFalse(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Selected = 3;
            InventoryItemSelected(Selected);
            Events.TowerComponentSelected(inventory[Selected].Key);

            IsNullSetBuilderFalse(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Selected = 4;
            InventoryItemSelected(Selected);
            Events.TowerComponentSelected(inventory[Selected].Key);

            IsNullSetBuilderFalse(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Selected = 5;
            InventoryItemSelected(Selected);
            Events.TowerComponentSelected(inventory[Selected].Key);

            IsNullSetBuilderFalse(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Selected = 6;
            InventoryItemSelected(Selected);
            Events.TowerComponentSelected(inventory[Selected].Key);

            IsNullSetBuilderFalse(inventory[Selected].Key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Selected = 7;
            InventoryItemSelected(Selected);
            Events.TowerComponentSelected(inventory[Selected].Key);

            IsNullSetBuilderFalse(inventory[Selected].Key);
        }
    }

    public void AddItem(TowerComponentData item, int index)
    {
        Pair<TowerComponentData> pair = new Pair<TowerComponentData>(item);
        inventory[index] = pair;

        images[index].gameObject.SetActive(true);

        images[index].sprite = item.IconSprite;

    }

    public void SellItem(TowerComponentData item, int amount, int index)
    {
        // TODO
    }

    public void InventoryItemSelected(int index)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (i == index)
            {
                greenGlows[i].SetActive(true);
            }
            else
            {
                greenGlows[i].SetActive(false);
            }
        }
    }

    public void IsNullSetBuilderFalse(TowerComponentData item)
    {
        if (item == null)
        {
            Builder.Instance.SetGameObjectState(false);
        }
    }

    //eemalda blueprint inventoryst
    public void RemoveItem()
    {
        //TODO
    }
}
