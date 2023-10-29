using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Terminal : MonoBehaviour
{
    public static Terminal instance;

    public TextMeshProUGUI StoneResourceText;
    public TextMeshProUGUI IronResourceText;
    public TextMeshProUGUI UraniumResourceText;

    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemTypeText;

    public TextMeshProUGUI StatisticsText;

    public TextMeshProUGUI StoneCostText;
    public TextMeshProUGUI IronCostText;
    public TextMeshProUGUI UraniumCostText;

    public TowerComponentData SelectedItem;

    private void Awake()
    {
        Events.OnSetStone += SetStone;
        Events.OnSetIron += SetIron;
        Events.OnSetUranium += SetUranium;
    }

    private void OnDestroy()
    {
        Events.OnSetStone -= SetStone;
        Events.OnSetIron -= SetIron;
        Events.OnSetUranium -= SetUranium;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemSelected(TowerComponentData item)
    {
        SelectedItem = item;

        ItemNameText.text = item.DisplayName;

        if (item is FoundationData)
        {
            ItemTypeText.text = "Foundation";
            FoundationData data = (FoundationData)item;
            StatisticsText.text = data.FoundationPrefab.Limited ? "Limited" : "Unlimited";
        }

        if (item is StructureData)
        {
            ItemTypeText.text = "Structure";
            StructureData data = (StructureData)item;
            StatisticsText.text = "Range: " + data.StructurePrefab.Range;
        }

        if (item is GunBaseData)
        {
            ItemTypeText.text = "Gun base";
            GunBaseData data = (GunBaseData)item;
            StatisticsText.text = "Damage modifier: " + data.GunBasePrefab.DamageModifier + "\nFirerate modifier: " + data.GunBasePrefab.FirerateModifier + "\nBullet speed modifier: " + data.GunBasePrefab.BulletSpeedModifier;
        }

        if (item is GunData)
        {
            ItemTypeText.text = "Gun";
            GunData data = (GunData)item;
            StatisticsText.text = "Damage: " + data.GunPrefab.Damage + "\nFirerate: " + data.GunPrefab.FireRate + "\nRange modifier: " + data.GunPrefab.RangeModifier + (data.GunPrefab.Seeking ? "\nSeeking bullets" : "");
        }

        StoneCostText.text = "Stone: " + item.Cost[0];
        IronCostText.text = "Iron: " + item.Cost[1];
        UraniumCostText.text = "Uranium: " + item.Cost[2];
    }

    public void OnMakePressed()
    {
        print(SelectedItem);
        print(Inventory.instance);
        Inventory.instance.OnMake(SelectedItem);
    }

    void SetStone(float stone)
    {
        StoneResourceText.text = "Stone: " + Mathf.Floor(stone);
    }

    void SetIron(float iron)
    {
        IronResourceText.text = "Iron: " + Mathf.Floor(iron);
    }

    void SetUranium(float uranium)
    {
        UraniumResourceText.text = "Uranium: " + Mathf.Floor(uranium);
    }
}
