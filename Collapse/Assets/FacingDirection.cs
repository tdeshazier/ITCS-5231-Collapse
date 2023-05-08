using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingDirection : MonoBehaviour
{
    public GameObject target;
    float second = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            transform.position = new Vector3(target.transform.position.x, 0, target.transform.position.z + 2);
      
       
    }
}
