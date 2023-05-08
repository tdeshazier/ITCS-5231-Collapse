using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;


public class Camera_Movement : MonoBehaviour
{
    
    
    public Transform camTarget;
    public Transform camPosition;
    Vector3 offset;
    Ray Camera_To_Player;
    public GameObject tree_1;
    public GameObject tree_2;
    public Material tree_material;
    public Material tree2_material;
    public Terrain terrain;

    private float _rotationY;
    Vector3 old_Position = Vector3.zero;
    float distance = 0.0f;

    float alpha_out = 0.3f;
    float alpha_in = 1.0f;
    float[] distances = new float[32];
    Camera camera;
    float[,,] map;

    GameObject obj; 
    bool alpha_changed = false;
    private void Start()
    {

        //offset = transform.position - camTarget.position;

        //distance = Vector3.Distance(transform.position, camTarget.position);
        
        //old_Position = transform.position;

        //obj = new GameObject();
        //Camera_To_Player = new Ray(transform.position, camTarget.position);
        //camera = GetComponent<Camera>();
        //map = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight, 2];
    }
    // Update is called once per frame
    void Update()
    {
      
        transform.position = camTarget.position + offset;
        
        RaycastHit hit;
        Camera_To_Player = new Ray(transform.position, camTarget.position);
        
        if (Physics.Raycast(Camera_To_Player, out hit, distance))
        {
      
            if(hit.collider.GetType() == typeof(TerrainCollider)) 
            {
                TerrainData td = new TerrainData();
                TerrainCollider terraindata = hit.collider as TerrainCollider;

               
            }


            if(hit.collider.TryGetComponent<Terrain>(out Terrain terrain)) 
            {
                
            }

            

            alpha_changed= true;
              
            
        }
        else 
        {
            if(obj != null && alpha_changed) 
            {
                //Color currentColor = obj.GetComponent<Renderer>().material.color;
                //obj.GetComponent<Renderer>().material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1.0f);

                //alpha_changed = false;
                //obj = null;
            }
        }
     

        //transform.position = camTarget.position - transform.forward * distance;


    }

    public void SetVars() 
    {
        offset = transform.position - camTarget.position;

        distance = Vector3.Distance(transform.position, camTarget.position);

        old_Position = transform.position;

        obj = new GameObject();
        Camera_To_Player = new Ray(transform.position, camTarget.position);
        camera = GetComponent<Camera>();
        //map = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight, 2];
    }
    private void FixedUpdate()
    {
        Vector3 CameraPosition = camTarget.position + offset;
        camPosition.position = CameraPosition;
    }
}

#region maybecode

/*
 * 
 *       obj = hit.collider.gameObject;
          if (hit.collider.gameObject.CompareTag("Object") || hit.collider.gameObject.CompareTag("Enviornment"))
            {
                var parts = obj.GetComponentsInChildren<MeshRenderer>();

           
                alpha_changed= true;
              
            }
 * 
 *      for(int i = 0; i < parts.Length; i++) 
                {
                    var deb = parts[i].GetComponent<MeshRenderer>().materials;

                    for(int j = 0; j < deb.Length; j++)
                    {
                        //Debug.Log(deb[j].GetColor("_ColorTint"));
                       // Color currentColor = deb[j].GetColor("_ColorTint");
                       // Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0.3f);
                       // deb[i].SetColor("_ColorTint", newColor);
                    }
                    //Color currentColor = parts[i].GetComponent<MeshRenderer>().material.color;
                   // parts[i].GetComponent<MeshRenderer>().material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.3f);
                }
                //Color currentColor = obj.GetComponent<MeshRenderer>().material.color;
                //obj.GetComponent<Renderer>().material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.3f);

     //Destroy(hit.collider.gameObject);   
               // box.a = 0.3f;
               // Debug.Log(box);
               // hit.collider.gameObject.GetComponent<Renderer>().material.color = box;

     if(obj != null && alpha_changed) 
            {
                Color currentColor = obj.GetComponent<Renderer>().material.color;
                obj.GetComponent<Renderer>().material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1.0f);

                alpha_changed = false;
                obj = null;
            }
 */
#endregion
