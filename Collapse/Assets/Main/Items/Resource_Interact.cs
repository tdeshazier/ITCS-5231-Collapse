using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Resource_Interact : MonoBehaviour, IInteractable
{
    public Resource_Handler rh;
    public bool isFuel;
    public bool isMineral;
    public bool isSurvival;
    public bool isChest;
    public MeshFilter current_skin;
    public MeshRenderer current_renderer;
    public MeshCollider current_collider;
    public Material current_material;
    public MeshFilter current_lid;
    public MeshRenderer current_lid_renderer;
    public Material current_lid_material;

    public GameObject lid;
    public List<GameObject> chests_lookalike = new List<GameObject>();

    [SerializeField] TextDisplay mintext;
    [SerializeField] TextDisplay fueltext;
    [SerializeField] TextDisplay survtext;
    [SerializeField] RetrieveUI BattleUI;


    public int survtextamount = 0;
    public int mintextamount = 0;
    public int fueltextamount = 0;

    public bool chest_opened = false;

    // Start is called before the first frame update
    void Start()
    {
        if(current_skin == null)
            current_skin = GetComponent<MeshFilter>();

        if(current_renderer == null)
            current_renderer = GetComponent<MeshRenderer>();

        if(current_collider == null)
            current_collider = GetComponent<MeshCollider>();

        if(current_material == null)
            current_material = GetComponent<Material>();
        
        if (FindObjectOfType<Resource_Handler>().gameObject != null)
        {
            rh = FindObjectOfType<Resource_Handler>();
        }

        CreateChest();
        BattleUI = FindObjectOfType<RetrieveUI>();
        if(BattleUI != null) 
        {
            mintext = BattleUI.GetMinText();
            fueltext= BattleUI.GetFuelText();
            survtext= BattleUI.GetSurvText();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Giving Resource");
        provide_resource();

    }

    void CreateChest() 
    {

        int randomchest = Random.Range(0, chests_lookalike.Count);

        current_skin.mesh = chests_lookalike[randomchest].GetComponent<MeshFilter>().sharedMesh;
        current_renderer.materials = chests_lookalike[randomchest].GetComponent<MeshRenderer>().sharedMaterials;
        current_collider.sharedMesh = chests_lookalike[randomchest].GetComponent<MeshCollider>().sharedMesh;
        current_material = chests_lookalike[randomchest].GetComponent<Material>();

        if (chests_lookalike[randomchest].transform.childCount > 0)
        {
            Transform CopyLid = chests_lookalike[randomchest].transform.GetChild(0);

            lid.GetComponent<MeshFilter>().mesh = CopyLid.GetComponent<MeshFilter>().sharedMesh;
            lid.GetComponent<MeshRenderer>().materials = CopyLid.GetComponent<MeshRenderer>().sharedMaterials;
            current_lid_material = lid.GetComponent<Material>();
            current_lid_material = CopyLid.GetComponent<Material>();
            lid.transform.localPosition = CopyLid.localPosition;
        }
    }
    void provide_resource() 
    {
        int amount = 0;
        int chest_iter = 0;

        survtextamount = 0;
        mintextamount = 0;
        fueltextamount = 0;
        if (isFuel) 
        {
            amount = Random.Range(10, 50);
            rh.SetFuelResource(amount);
            rh.SetUpdateResource(true);
        }

        if(isMineral) 
        {
            amount = Random.Range(10, 50);
            rh.SetMineralsResource(amount);
            rh.SetUpdateResource(true);
        }

        if(isSurvival) 
        {
            amount = Random.Range(10, 50);
            rh.SetSurvivalResource(amount);
            rh.SetUpdateResource(true);
        }

        if(isChest && !chest_opened) 
        {

            chest_iter = Random.Range(5, 10);
            Debug.Log(chest_iter + " times the chest will spit out rewards");
            for(int i = 0; i < chest_iter; i++) 
            {
                amount = Random.Range(10, 50);
                int type_chance = Random.Range(1, 30);

                if (type_chance >= 1 && type_chance <= 9)
                {
                    fueltextamount += amount;
                    rh.SetFuelResource(amount);
                }
                else if (type_chance >= 10 && type_chance <= 19)
                {
                    mintextamount += amount;
                    rh.SetMineralsResource(amount);
                }
                else if (type_chance >= 20 && type_chance <= 30)
                {
                    survtextamount += amount;
                    rh.SetSurvivalResource(amount);
                }

            }
            chest_opened = true;
            rh.SetUpdateResource(true);
            SetTextNeeded();
        }


        
    }

    void SetTextNeeded() 
    {
        if(fueltextamount > 0) 
        {
            fueltext.amount = fueltextamount;
            rh.SetFuelTextAmount(fueltextamount);
            fueltext.fadingIn = true;
        }

        if(mintextamount > 0) 
        {
            mintext.amount = mintextamount;
            rh.SetMineralTextAmount(mintextamount);
            mintext.fadingIn = true;
        }

        if(survtextamount > 0) 
        {
            survtext.amount = survtextamount;
            rh.SetSurvialTextAmount(survtextamount);
            survtext.fadingIn = true;
        }
    }

   

   
}
