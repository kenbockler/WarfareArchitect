using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private static readonly float ColliderCheckRadius = 0.4f; // Konstant kollisiooni kontrollimiseks
    private Camera cam;

    public Color AllowColor = Color.green;
    public Color DenyColor = Color.red;
    private MeshRenderer[] MeshRenderers;

    public TowerComponentData data;

    public Vector3 position;

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
        MeshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetGameObjectAtPosition();

        // Liiguta ehitise eelvaadet hiirekursori asukohta
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        pos.x = Mathf.Round(pos.x + 10f) - 10f;
        pos.y = Mathf.Round(pos.y + 10f) - 10f;
        pos.z = 0;
        transform.position = pos;

        // Kontrolli, kas saab ehitada
        bool isFree = IsFree();
        Color color = isFree ? AllowColor : DenyColor;

        foreach (MeshRenderer renderer in MeshRenderers)
        {
            renderer.materials[0].color = color;
        }

        // Ehitamine v�i desaktiveerimine
        if (Input.GetMouseButtonDown(0) && isFree)
        {
            Build();
        }
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }
    }

    bool IsFree()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ColliderCheckRadius);

        // Kontrollime kollisioone
        foreach (Collider2D collider in colliders)
        {
            if (!collider.isTrigger && !IsAllowedToBuildOn(collider))
                return false;
        }

        // Kontrollime, kas kursor on �le UI elemendi
        if (EventSystem.current.IsPointerOverGameObject())
            return false;

        return true;
    }

    // Kontrollime, kas saame antud kohta ehitada
    private bool IsAllowedToBuildOn(Collider2D collider)
    {
        if (data is FoundationData && collider.CompareTag("Terrain"))
            return true;

        if (data is StructureData && collider.CompareTag("Foundation"))
            return true;

        if (data is GunBaseData && collider.CompareTag("Structure"))
            return true;

        if (data is GunData && collider.CompareTag("GunBase"))
            return true;

        return false;
    }


    void Build()
    {
        if (data is FoundationData && Foundation == null)
        {
            Instantiate(((FoundationData)data).FoundationPrefab, position, Quaternion.identity);
        }
        else if (data is StructureData && Foundation != null)
        {
            Structure newStructure = Instantiate(((StructureData)data).StructurePrefab, position, Quaternion.identity);
            newStructure.Foundation = Foundation;
        }
        else if (data is GunBaseData && Structure != null)
        {
            GunBase newGunBase = Instantiate(((GunBaseData)data).GunBasePrefab, position, Quaternion.identity);
            newGunBase.Structure = Structure;
        }
        else if (data is GunData && GunBase != null)
        {
            Gun newGun = Instantiate(((GunData)data).GunPrefab, position, Quaternion.identity);
            newGun.GunBase = GunBase;
        }
        // Siia saab lisada teisi komponente...
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
            GameObject hitObject = hit.collider.gameObject;

            position = hit.point;

            // Puhastame eelmised viited
            Foundation = null;
            Structure = null;
            GunBase = null;

            if (hitObject.GetComponent<Foundation>() != null)
            {
                Foundation = hitObject.GetComponent<Foundation>();
            }
            else if (hitObject.GetComponent<Structure>() != null)
            {
                Structure = hitObject.GetComponent<Structure>();
            }
            else if (hitObject.GetComponent<GunBase>() != null)
            {
                GunBase = hitObject.GetComponent<GunBase>();
            }
            // Kui on veel teisi objekte, millele v�ib ehitada, siis lisa siia..
        }
    }
}
