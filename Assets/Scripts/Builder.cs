using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private Camera cam;
    public Color AllowColor = Color.green;
    public Color DenyColor = Color.red;
    private SpriteRenderer[] SpriteRenderers;
    //public EventSystem EventSystem;
    public TowerComponentData data;

    public Foundation Foundation;
    public Structure Structure;
    public GunBase GunBase;

   void Awake()
    {
        Events.OnTowerComponentSelected += SetTowerComponentData;
    }

    void OnDestroy()
    {
        Events.OnTowerComponentSelected -= SetTowerComponentData;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetGameObjectAtPosition();

        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        pos.x = Mathf.Round(pos.x + 10f) - 10f;
        pos.y = Mathf.Round(pos.y + 10f) - 10f;
        pos.z = 0;
        transform.position = pos;

        bool isFree = IsFree();
        Color color = isFree ? AllowColor : DenyColor;
        foreach(SpriteRenderer renderer in SpriteRenderers)
        {
            renderer.color = color;
        }
        if(Input.GetMouseButtonDown(0) && isFree)
        {
            Build();
        }
        if(Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }
    }

    bool IsFree()
    {
        bool free = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.4f);
        foreach(Collider2D collider in colliders)
        {
            if(!collider.isTrigger)
                free = false;
        }
        if(EventSystem.current.IsPointerOverGameObject())
        {
            free = false;
        }
        return free;
    }

    void Build()
    {
        if(data is FoundationData)
        {
            Instantiate<Foundation>(((FoundationData)data).FoundationPrefab, transform.position, Quaternion.identity);
        }
        if(data is StructureData)
        {
            Instantiate<Structure>(((StructureData)data).StructurePrefab, transform.position, Quaternion.identity);
        }
        if(data is GunBaseData)
        {
            Instantiate<GunBase>(((GunBaseData)data).GunBasePrefab, transform.position, Quaternion.identity);
        }
        if(data is GunData)
        {
            Instantiate<Gun>(((GunData)data).GunPrefab, transform.position, Quaternion.identity);
        }
        if(data is SupportBlockData)
        {

        }
        gameObject.SetActive(false);
    }

    void SetTowerComponentData(TowerComponentData _data)
    {
        gameObject.SetActive(true);
        data = _data;
    }

    void GetGameObjectAtPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject thing = hit.collider.gameObject;
            print("found " + thing.name + " at distance: " + hit.distance);
            if(thing.GetComponent<Foundation>() != null)
            {
                print("Saving foundation");
                //Foundation = (Foundation) thing;
            }
        }
    }
}
