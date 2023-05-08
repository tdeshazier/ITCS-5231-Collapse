using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class Spawner_Scr : MonoBehaviour
{
    public Enemy_Range range_prefab;
    public Enemy_Melee melee_prefab;
    public float chance_active = 0.5f; // chance that the spawner will be activated (only for very easy mode)
    public float distance_between = 15; // the spawn distance between each enemy
    public float distance_from = 100f; // the distance that the objective at least has to be away from the player.
    public List<Base_Enemy_Actions> enemytrack = new List<Base_Enemy_Actions>();
    bool trackenemies = false;

    public GameManager manager;
    private SphereCollider sc;

    private Bounds sc_bounds;
    private Vector3 sc_center;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        sc = GetComponent<SphereCollider>();
        sc_bounds = sc.bounds;
        sc_center = sc.center;

        if (QuestManager.instance.mission_type != "Kill Quest")
        {
            GetComponent<Objective_Scr>().enabled = false;
            GetComponent<Objective_Scr>().isObj = false;

        }
        else
            GetComponent<Objective_Scr>().isObj = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestManager.instance.mission_type != "Kill Quest")
        {
            //GetComponent<Objective_Scr>().enabled = false;
            //GetComponent<Objective_Scr>().isObj = false;
            return;
        }

        if (trackenemies)
        {
            checkList();
            if (enemytrack.Count <= 0)
            {
                trackenemies = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if(!trackenemies)
                SpawnEnemy();
        }
    }

    void checkList() 
    {
        for (int i = enemytrack.Count - 1; i > -1; i--)
        {

            if (enemytrack[i]== null)
                enemytrack.RemoveAt(i);
        }
    }
    //void SpawnChests() 
    //{
    //    int amount_spawn = Random.Range(5, 7) * (manager.difficulty + 1);

    //    for(int i = 0; i < amount_spawn; i++) 
    //    {
    //        float randValue = Random.Range(0.0f, 1.0f);

    //        float random_x = Random.Range((sc_center.x - sc_bounds.extents.x) / 2, (sc_center.x + sc_bounds.extents.x) / 2);
    //        float random_z = Random.Range((sc_center.z - sc_bounds.extents.z) / 2, (sc_center.z + sc_bounds.extents.z) / 2);

    //        //Vector3 spawn_point = new Vector3(random_x, 1, random_z);
    //        Vector3 spawn_point;
    //        Vector3 random_point = transform.position + (Random.insideUnitSphere * distance_between);
    //        random_point.y = 1;
    //        NavMeshHit hit;
    //        float distance_rad = Vector3.Distance(manager.player.transform.position, random_point);
    //        if (NavMesh.SamplePosition(random_point, out hit, distance_rad, NavMesh.AllAreas)) 
    //        {
    //            spawn_point = hit.position;
    //            if(randValue < .35)
    //        }
    //    }
    //}
    void SpawnEnemy() 
    {
        
        bool isObj = QuestManager.instance.mission_type == "Kill Quest" ? true : false;
        for(int i = 0; i < manager.spawn_amount; i++) 
        {
            float randValue = Random.Range(0.0f, 1.0f);
            
           
            float random_x = Random.Range((sc_center.x - sc_bounds.extents.x) / 2, (sc_center.x + sc_bounds.extents.x) / 2);
            float random_z = Random.Range((sc_center.z - sc_bounds.extents.z) / 2, (sc_center.z + sc_bounds.extents.z) / 2);

            //Vector3 spawn_point = new Vector3(random_x, 1, random_z);
            Vector3 spawn_point;
            Vector3 random_point = transform.position + (Random.insideUnitSphere * distance_between);
            random_point.y = 1;
            NavMeshHit hit;
            float distance_rad = Vector3.Distance(manager.player.transform.position, random_point);
            if(NavMesh.SamplePosition(random_point, out hit,distance_rad, NavMesh.AllAreas))
            {
                spawn_point = hit.position;
                Base_Enemy_Actions enemy;
                if (randValue < .35f)
                {
                    enemy = Instantiate(range_prefab, spawn_point, Quaternion.identity);
                    
                }
                else
                   enemy = Instantiate(melee_prefab, spawn_point, Quaternion.identity);

              
                enemy.isQuestObj = isObj;
                enemytrack.Add(enemy);
            }
                

        }

        trackenemies = true;
    }
}
