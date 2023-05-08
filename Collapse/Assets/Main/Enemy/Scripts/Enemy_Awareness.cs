using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy_Awareness : MonoBehaviour
{
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<Base_Enemy_Actions>().faceTarget = true;
            GetComponentInParent<Base_Enemy_Actions>().Target = collision.gameObject;
            GetComponentInParent<Base_Enemy_Actions>().chase_timer = GetComponentInParent<Base_Enemy_Actions>().max_chase_time;
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if (!GetComponentInParent<Base_Enemy_Actions>().tookDamage)
            //{
            //    GetComponentInParent<Base_Enemy_Actions>().faceTarget = false;
            //    GetComponentInParent<Base_Enemy_Actions>().Target = null;
            //}
            
        }
    }
}
