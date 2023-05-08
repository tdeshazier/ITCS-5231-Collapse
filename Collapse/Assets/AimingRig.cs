using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimingRig : MonoBehaviour
{
    public Rig rig;
    public GameObject target;
    private float targetWeight;
    public Base_Enemy_Actions action;
    
    // Start is called before the first frame update
    void Start()
    {
      
        targetWeight = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (action.get_target)
        {
            target.transform.position = action.GetTarget().transform.position;
            rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * 10f);
        }
    }
}
