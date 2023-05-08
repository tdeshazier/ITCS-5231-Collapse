using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet_scr : MonoBehaviour
{
    public int damage;
    public float life_t;
    private float shoot_t;
    bool bullet_hit;

    public GameObject hit_p;

    public GameObject origin;
    public Base_Enemy_Actions enemyDetection;
    private void OnEnable()
    {
        shoot_t = Time.time; 
        
    }

    // Start is called before the first frame update
    void Start()
    {
    
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - shoot_t >= life_t) 
        {
            gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (origin == null)
            return;

        if (other.CompareTag("Player")) 
        {
            if(origin.CompareTag("Enemy"))
            {
                Debug.Log(other.name);
                other.GetComponent<Player_Controller>().damage_taken(damage);
                displayParticle();
                gameObject.SetActive(false);
            }
        }
        else if (other.CompareTag("Enemy")) 
        {
            if (origin.CompareTag("Player"))
            {

                bullet_hit = (other.GetComponent<Collider>().GetType() != typeof(MeshCollider)) ? true : false;

                if (bullet_hit)
                {
                    Debug.Log(other.name);
                    other.GetComponent<Base_Enemy_Actions>().damage_taken(damage);
                    displayParticle();
                    gameObject.SetActive(false);
                }
            }
        }
        else if (other.CompareTag("Object")) 
        {
             Debug.Log(other.name);
            displayParticle();
            gameObject.SetActive(false);
        }

        
    }

    private void displayParticle() 
    {
        GameObject obj = Instantiate(hit_p, transform.position, Quaternion.identity);
        Destroy(obj, 0.5f);
    }


}
