using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    public GameManager manager;
    public List<Base_Enemy_Actions> enemies = new List<Base_Enemy_Actions>();
    public List<Base_Enemy_Actions> spawnEnemies = new List<Base_Enemy_Actions>();
    public Enemy_Melee meleePrefab;
    public Enemy_Range rangePrefab;
    public int currWave;
    public int waveValue;
    public int y_offset = 1;
    public int radius_spawn = 15;

    public Vector3 spawnLocation;
    public int waveDuration;
    float waveTimer;
    float spawnInterval;
    float spawnTimer;

    SphereCollider sc;
    private Bounds sc_bounds;
    private Vector3 sc_center;

    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<SphereCollider>();
        sc_bounds = sc.bounds;
        sc_center = transform.TransformPoint(sc.center);
        manager = FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer <= 0) 
        {
            if(spawnEnemies.Count > 0) 
            {
                
                var tempEnemy = Instantiate(spawnEnemies[0], pickLocation(), Quaternion.identity);
                tempEnemy.Target = FindObjectOfType<Player_Controller>().gameObject;
                tempEnemy.faceTarget = true;
                tempEnemy.WaveAttack = true;
                spawnEnemies.RemoveAt(0);
                spawnTimer = spawnInterval;
            }

        }
        else 
        {
            spawnTimer -= Time.deltaTime;
            waveTimer -= Time.deltaTime; 
        }
    }

    public void GenerateWave() 
    {
        waveValue = currWave * 10;
        GenerateEnemies();

        spawnInterval = waveDuration / spawnEnemies.Count;
        waveTimer = waveDuration;
    }

    public void GenerateEnemies() 
    {
        List<Base_Enemy_Actions> tempEnemies = new List<Base_Enemy_Actions>();

        while(waveValue > 0) 
        {
            int randomEnemy = Random.Range(0, enemies.Count);
            int enemyCost = enemies[randomEnemy].Cost;
            Debug.Log(randomEnemy);
            if (waveValue - enemyCost >= 0)
            {
                if (enemies[randomEnemy] == meleePrefab.GetComponent<Enemy_Melee>())
                    tempEnemies.Add(meleePrefab);
                else if (enemies[randomEnemy] == rangePrefab.GetComponent<Enemy_Range>())
                    tempEnemies.Add(rangePrefab);

                waveValue -= enemyCost;
            }
            else if (waveValue <= 0)
                break;
        }

        spawnEnemies.Clear();
        spawnEnemies = tempEnemies;
    }

    public Vector3 pickLocation()
    {

        float randValue = Random.Range(0.0f, 1.0f);
        float random_x = Random.Range((sc_center.x - sc_bounds.extents.x) / 2, (sc_center.x + sc_bounds.extents.x) / 2);
        float random_z = Random.Range((sc_center.z - sc_bounds.extents.z) / 2, (sc_center.z + sc_bounds.extents.z) / 2);
        Vector3 spawn_point = new Vector3();
        // Vector3 random_point = transform.position + (Random.insideUnitSphere * 60);
        Vector3 random_point = RandomCircle(sc_center, radius_spawn);
        random_point.y = 1;
        NavMeshHit hit;
        float distance_rad = Vector3.Distance(manager.player.transform.position, random_point);
        if (NavMesh.SamplePosition(random_point, out hit, distance_rad, NavMesh.AllAreas))
        {
            spawn_point = hit.position;
        }

        return spawn_point;
    }

    Vector3 RandomCircle(Vector3 center, float radius) 
    {
        float ang = Random.value * 360;
        Vector3 pos;

        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = y_offset;
        pos.z = center.z;
        return pos;
    }
}
