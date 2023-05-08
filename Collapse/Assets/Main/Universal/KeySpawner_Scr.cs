using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class KeySpawner_Scr : MonoBehaviour
{
    // Start is called before the first frame update.
    public bool spawn_player = false; //the spawner that allows the player to be spawned at.
    public bool spawn_obj = false; //the spawner that spawns the objective (Only for Extraction and Kill Boss)

    public Player_Controller playerPrefab;
    public Extraction_Objective extractionPrefab;
    public Base_Enemy_Actions Range_Boss;
    public Base_Enemy_Actions Melee_Boss;
    public GameObject boss;
    public GameObject obj;
    public GameObject returnPoint;

    GameManager manager;
    QuestManager q_manager;
    SphereCollider sc;
    private Bounds sc_bounds;
    private Vector3 sc_center;


    private void Awake()
    {
        //manager = FindObjectOfType<GameManager>();
        //q_manager = FindObjectOfType<QuestManager>();
        sc = GetComponent<SphereCollider>();
        sc_bounds = sc.bounds;
        sc_center = sc.center;
    }

    

    void Start()
    {
        //if(spawn_player) 
        //{
        //    if(FindObjectOfType<Player_Controller>() == null) 
        //    {
        //        SpawnPlayer();
        //    }
        //}
        //else if(spawn_obj) 
        //{
        //    SpawnObj();
        //}
  

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnPlayer() 
    {
        float randValue = Random.Range(0.0f, 1.0f);


         float random_x = Random.Range((sc_center.x - sc_bounds.extents.x) / 2, (sc_center.x + sc_bounds.extents.x) / 2);
        float random_z = Random.Range((sc_center.z - sc_bounds.extents.z) / 2, (sc_center.z + sc_bounds.extents.z) / 2);


        Vector3 spawn_point;
         Vector3 random_point = transform.position + (Random.insideUnitSphere * 10);
        random_point.y = 1.5f;
        NavMeshHit hit;
       
        if (NavMesh.SamplePosition(random_point, out hit, 10, NavMesh.AllAreas))
        {
            spawn_point = hit.position;

            Instantiate(playerPrefab, spawn_point, Quaternion.identity);
            spawn_point.y += 2;
            Instantiate(returnPoint, spawn_point, Quaternion.identity);

        }
    }

    public void SpawnObj()
    {
        if (QuestManager.instance.mission_type == "Kill Quest")
            return;

        float randValue = Random.Range(0.0f, 1.0f);


        float random_x = Random.Range((sc_center.x - sc_bounds.extents.x) / 2, (sc_center.x + sc_bounds.extents.x) / 2);
        float random_z = Random.Range((sc_center.z - sc_bounds.extents.z) / 2, (sc_center.z + sc_bounds.extents.z) / 2);

        Vector3 spawn_point;
        Vector3 random_point = transform.position + (Random.insideUnitSphere * 10);
        random_point.y = 1;
        NavMeshHit hit;

        

        if (NavMesh.SamplePosition(random_point, out hit, 10, NavMesh.AllAreas))
        {
            spawn_point = hit.position;

            var obj = Instantiate(pullType(), spawn_point, Quaternion.identity);
            obj.GetComponent<Objective_Scr>().isObj = true;
        }

    }

    public GameObject pullType() 
    {
        var q_manager = FindObjectOfType<QuestManager>();
        GameObject type = new GameObject();
        switch (q_manager.mission_type) 
        {
            case "Kill Quest":
                break;
            case "Extraction":
                type = extractionPrefab.gameObject;
                break;
            case "Kill Elite":
                type = bossType().gameObject;
                type.GetComponent<Base_Enemy_Actions>().isQuestObj = true;
                break;
        }

        return type;
    }

    Base_Enemy_Actions bossType() 
    {
        return Melee_Boss;
        //return Random.Range(0.0f, 1.0f) < .50f ? Melee_Boss : Range_Boss;
    }
    
}
