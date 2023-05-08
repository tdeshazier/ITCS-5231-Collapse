using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner_Extras : MonoBehaviour
{

    GameManager manager;
    QuestManager q_manager;
    SphereCollider sc;
    private Bounds sc_bounds;
    private Vector3 sc_center;
    public Resource_Interact chest;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        sc = GetComponent<SphereCollider>();
        sc_bounds = sc.bounds;
        sc_center = sc.center;
        SpawnChests();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnChests()
    {
        int amount_spawn = Random.Range(5, 7) * (manager.difficulty + 1);

        for (int i = 0; i < amount_spawn; i++)
        {

       
            Vector3 spawn_point;
            Vector3 random_point = Random.insideUnitSphere * sc.radius;
            random_point.y = 1;
            random_point += transform.position;
            NavMeshHit hit;


            if (NavMesh.SamplePosition(random_point, out hit, sc.radius, NavMesh.AllAreas))
            {
                spawn_point = hit.position;

                var obj = Instantiate(chest, spawn_point, Quaternion.identity);
                obj.isChest = true;
            }
        }
        
    }
}
