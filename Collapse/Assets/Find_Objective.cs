using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class Find_Objective : MonoBehaviour
{

    public Objective_Scr obj;
    public ReturnHome returnPoint;
    public float turn_speed = 3.0f;
    public GameManager manager;
    bool madeit = false;
    bool meshOff = false;
    public MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
       manager = FindObjectOfType<GameManager>();
       returnPoint = FindObjectOfType<ReturnHome>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.inHub)
            return;
        if (!QuestManager.instance.quest_complete)
        {
            SetObjective();

            if(obj != null)
            point_the_way(obj.gameObject);
        }
        else if (QuestManager.instance.quest_complete)
        {
            point_the_way(returnPoint.gameObject);
        }

        //if (Vector3.Distance(obj.transform.position, manager.player.transform.position) <= 8)
        //    madeit = true;
       
        //if(obj != null && !madeit) 
        //{
        //    if (meshOff)
        //        meshOff = false;
        //    var lookPos = obj.transform.position - transform.position;
        //    lookPos.y = 0;
        //    var rotation = Quaternion.LookRotation(lookPos);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turn_speed);
           
        //}
        //else if (madeit) 
        //{
        //    if (!meshOff) 
        //    {
        //        meshOff = true;
        //        mesh.enabled = false;
        //    }
        //}
        //else
        //    madeit = false;

        transform.position = manager.player.transform.position;
    }

    public void point_the_way(GameObject obj) 
    {
        if (Vector3.Distance(obj.transform.position, manager.player.transform.position) <= 8)
            madeit = true;
        else
            madeit = false;

        if (obj != null && !madeit)
        {
            if (meshOff)
            {
                meshOff = false;
                mesh.enabled = true;
            }
            var lookPos = obj.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turn_speed);

        }
        else if (madeit)
        {
            if (!meshOff)
            {
                meshOff = true;
                mesh.enabled = false;
            }
        }

    }
    public void SetObjective()
    {


        if (QuestManager.instance.mission_type == "Kill Quest")
        {

            var allObjects = FindObjectsByType<Objective_Scr>(FindObjectsInactive.Exclude
             , FindObjectsSortMode.None);
            float nearestDist = 10000;

            for (int i = 0; i < allObjects.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, allObjects[i].transform.position);

                if (distance < nearestDist)
                {
                    obj = allObjects[i];
                    nearestDist = distance;
                }
            }
        }
        else
        {
            if (obj != null)
                return;

            var allObjects = FindObjectsByType<Objective_Scr>(FindObjectsInactive.Exclude
             , FindObjectsSortMode.None);

            for(int i = 0; i < allObjects.Length; i++) 
            {
                if (allObjects[i].isObj)
                    obj = allObjects[i];
            }
        }

    }
}
