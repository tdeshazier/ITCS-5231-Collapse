using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FaceCamera : MonoBehaviour
{
    [SerializeField] GameObject camera;
    float second = 0.05f;
    public float constraint = 0.05f;
    public float y_offset = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.gameObject;
        second = constraint;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = GameManager.instance.player.transform.position;
        position.y = position.y + y_offset;
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
        
        //if (second <= 0.0f)
        //{

        //    transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
        //    second = constraint;
        //}
        //else
        //    second -= Time.deltaTime;
    }
}
