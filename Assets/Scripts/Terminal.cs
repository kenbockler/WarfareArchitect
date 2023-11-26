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
    public GameObject IconObject;
    public Image IconSprite;

    public TextMeshProUGUI StatisticsText;

    public TextMeshProUGUI StoneCostText;
    public TextMeshProUGUI IronCostText;
    public TextMeshProUGUI UraniumCostText;

    public TowerComponentData SelectedItem;

    public AudioClipGroup SelectedAudio;

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

        IconObject.SetActive(false);

        Events.SetStone(Events.GetStone());
        Events.SetIron(Events.GetIron());
        Events.SetUranium(Events.GetUranium());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemSelected(TowerComponentData item)
    {
        SelectedAudio.Play();
        SelectedItem = item;

        ItemNameText.text = item.DisplayName;

        //print(item.DisplayName);

        if (item.IconSprite != null)
        {
            IconObject.SetActive(true);
            IconSprite.sprite = item.IconSprite;
        }
        else
        {
            IconObject.SetActive(false);
        }

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

        if (item is SupportBlockData)
        {
            ItemTypeText.text = "Support Block";
            SupportBlockData data = (SupportBlockData)item;
            StatisticsText.text = "Range modifier: " + data.SupportBlockPrefab.RangeModifier +
                                  "\nFirerate modifier: " + data.SupportBlockPrefab.FirerateModifier +
                                  "\nDamage modifier: " + data.SupportBlockPrefab.DamageModifier +
                                  "\nBullet speed modifier: " + data.SupportBlockPrefab.BulletSpeedModifier +
                                  (data.SupportBlockPrefab.Seeking ? "\nSeeking bullets" : "") +
                                  (data.SupportBlockPrefab.Piercing ? "\nPiercing bullets" : "") +
                                  (data.SupportBlockPrefab.Persistent ? "\nPersistent bullets" : "") +
                                  (data.SupportBlockPrefab.Poison > 0 ? "\nPoisonous bullets " + data.SupportBlockPrefab.Poison : "") +
                                  (data.SupportBlockPrefab.Slow != 1f ? "\nSlowing bullets " + data.SupportBlockPrefab.Slow : "");
        }

        if (item is DrillData)
        {
            ItemTypeText.text = "Drill";
            DrillData data = (DrillData)item;
            StatisticsText.text = "Stone mining speed: " + data.DrillPrefab.StoneMiningSpeed + "/min\nIron mining speed: " + data.DrillPrefab.IronMiningSpeed + "/min\nUranium mining speed: " + data.DrillPrefab.UraniumMiningSpeed + "/min";
        }

        StoneCostText.text = "Stone: " + item.Cost[0];
        IronCostText.text = "Iron: " + item.Cost[1];
        UraniumCostText.text = "Uranium: " + item.Cost[2];
    }

    public void OnMakePressed()
    {
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
