using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private static readonly float ColliderCheckRadius = 5f; // Konstant kollisiooni kontrollimiseks
    private Camera cam;

    public Color AllowColor = Color.green;
    public Color DenyColor = Color.red;
    private MeshRenderer MeshRenderer;
    private MeshFilter MeshFilter;

    public TowerComponentData data;
    public Vector3 pos;

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
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetGameObjectAtPosition();
        //MeshRenderers = GetComponentsInChildren<MeshRenderer>();

        // Kontrolli, kas saab ehitada
        bool isFree = IsFree();
        Color color = isFree ? AllowColor : DenyColor;

        if (MeshRenderer.materials[0] != null) MeshRenderer.materials[0].color = color;

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
        Collider[] colliders = Physics.OverlapSphere(transform.position, ColliderCheckRadius);

        // Kontrollime kollisioone
        foreach (Collider collider in colliders)
        {
            if (IsAllowedToBuildOn(collider))
                return true;
        }

        // Kontrollime, kas kursor on �le UI elemendi
        if (EventSystem.current.IsPointerOverGameObject())
            return false;

        return false;
    }

    // Kontrollime, kas saame antud kohta ehitada
    private bool IsAllowedToBuildOn(Collider collider)
    {
        if (data is FoundationData && collider.CompareTag("Terrain"))
        {
            foreach(Collider coll in Physics.OverlapSphere(transform.position, ((FoundationData)data).FoundationPrefab.transform.localScale.x / 2))
            {
                if (coll.GetComponent<Foundation>()) return false;
            }
            // Selle rea lõpus on konstant, mida peab muutma, kui muutub tee sügavus, mis praegu on 30.
            foreach(Collider coll in Physics.OverlapSphere(transform.position, Mathf.Sqrt(((FoundationData)data).FoundationPrefab.transform.localScale.x / 2) + 35))
            {
                if (coll.CompareTag("Path")) return false;
            }
            return true;
        }

        if (data is StructureData && collider.CompareTag("Foundation"))
            return true;

        if (data is GunBaseData && collider.CompareTag("Structure"))
            return true;

        if (data is GunData && collider.CompareTag("GunBase"))
            return true;

        if (data is SupportBlockData && collider.CompareTag("Structure"))
            return true;

        return false;
    }


    void Build()
    {
        if (data is FoundationData && Foundation == null)
        {
            Instantiate(((FoundationData)data).FoundationPrefab, pos, Quaternion.identity);
        }
        else if (data is StructureData && Foundation != null)
        {
            Structure newStructure = Instantiate(((StructureData)data).StructurePrefab, pos, Quaternion.identity);
            newStructure.Foundation = Foundation;
            newStructure.transform.position = Foundation.transform.position + new Vector3(0, (newStructure.transform.localScale.y + Foundation.transform.localScale.y) / 2, 0);
        }
        else if (data is GunBaseData && Structure != null)
        {
            GunBase newGunBase = Instantiate(((GunBaseData)data).GunBasePrefab, pos, Quaternion.identity);
            newGunBase.Structure = Structure;
            newGunBase.transform.position = Structure.transform.position + new Vector3(0, (newGunBase.transform.localScale.y + Structure.transform.localScale.y) / 2, 0);
        }
        else if (data is GunData && GunBase != null)
        {
            Gun newGun = Instantiate(((GunData)data).GunPrefab, pos, Quaternion.identity);
            newGun.GunBase = GunBase;
            newGun.transform.position = GunBase.transform.position + new Vector3(0, (newGun.transform.localScale.y + GunBase.transform.localScale.y) / 2, 0);
        }
        else if (data is SupportBlockData && Structure != null)
        {
            float xd = (pos.x - ((SupportBlockData)data).SupportBlockPrefab.transform.localScale.x / 2) - Structure.transform.position.x;
            float zd = (pos.z - ((SupportBlockData)data).SupportBlockPrefab.transform.localScale.z / 2) - Structure.transform.position.z;
            float d = Mathf.Abs(xd) + Mathf.Abs(zd);
            if(d > 10 && d <= 30 && Mathf.Abs(xd) < 20 && Mathf.Abs(zd) < 20)
            {
                int i = 0;
                if(xd == 15)
                {
                    if(zd == 15) i = 0;
                    else if(zd == 5) i = 1;
                    else if(zd == -5) i = 2;
                    else if(zd == -15) i = 3;
                }
                else if(xd == 5)
                {
                    if(zd == 15) i = 11;
                    else if(zd == -15) i = 4;
                }
                else if(xd == -5)
                {
                    if(zd == 15) i = 10;
                    else if(zd == -15) i = 5;
                }
                else if(xd == -15)
                {
                    if(zd == 15) i = 9;
                    else if(zd == 5) i = 8;
                    else if(zd == -5) i = 7;
                    else if(zd == -15) i = 6;
                }

                if(Structure.SupportBlocks[i] == null)
                {
                    SupportBlock newSupportBlock = Instantiate(((SupportBlockData)data).SupportBlockPrefab, new Vector3(pos.x, Structure.transform.position.y + Structure.transform.localScale.y / 2, pos.z), Quaternion.identity);
                    newSupportBlock.transform.position += new Vector3(-newSupportBlock.transform.localScale.x / 2, newSupportBlock.transform.localScale.y / 2, -newSupportBlock.transform.localScale.z / 2);

                    Structure.SupportBlocks[i] = newSupportBlock;
                }
            }
        }
        // Siia saab lisada teisi komponente...
        //gameObject.SetActive(false);
    }
    
    void SetTowerComponentData(TowerComponentData _data)
    {
        gameObject.SetActive(true);
        data = _data;
        if (data is FoundationData)
        {
            MeshRenderer.materials[0].CopyPropertiesFromMaterial(((FoundationData)data).FoundationPrefab.GetComponent<MeshRenderer>().sharedMaterials[0]);
            MeshFilter = ((FoundationData)data).FoundationPrefab.GetComponent<MeshFilter>();
            transform.localScale = ((FoundationData)data).FoundationPrefab.transform.localScale;
        }

        if (data is StructureData)
        {
            MeshRenderer.materials[0].CopyPropertiesFromMaterial(((StructureData)data).StructurePrefab.GetComponent<MeshRenderer>().sharedMaterials[0]);
            MeshFilter = ((StructureData)data).StructurePrefab.GetComponent<MeshFilter>();
            transform.localScale = ((StructureData)data).StructurePrefab.transform.localScale;
        }

        if (data is GunBaseData)
        {
            MeshRenderer.materials[0].CopyPropertiesFromMaterial(((GunBaseData)data).GunBasePrefab.GetComponent<MeshRenderer>().sharedMaterials[0]);
            MeshFilter = ((GunBaseData)data).GunBasePrefab.GetComponent<MeshFilter>();
            transform.localScale = ((GunBaseData)data).GunBasePrefab.transform.localScale;
        }

        if (data is GunData)
        {
            // MeshRenderer.materials[0].CopyPropertiesFromMaterial(((GunData)data).GunPrefab.GetComponent<MeshRenderer>().sharedMaterials[0]);
            // MeshFilter = ((GunData)data).GunPrefab.GetComponent<MeshFilter>();
            // transform.localScale = ((GunData)data).GunPrefab.transform.localScale;
        }

        if (data is SupportBlockData)
        {
            MeshRenderer.materials[0].CopyPropertiesFromMaterial(((SupportBlockData)data).SupportBlockPrefab.GetComponent<MeshRenderer>().sharedMaterials[0]);
            MeshFilter = ((SupportBlockData)data).SupportBlockPrefab.GetComponent<MeshFilter>();
            transform.localScale = ((SupportBlockData)data).SupportBlockPrefab.transform.localScale;
        }
    }

    void GetGameObjectAtPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            pos = hit.point;
            pos.x = Mathf.Round(pos.x / 10f) * 10f;
            pos.y = Mathf.Round(pos.y / 10f) * 10f;
            pos.z = Mathf.Round(pos.z / 10f) * 10f;
            transform.position = pos;

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
