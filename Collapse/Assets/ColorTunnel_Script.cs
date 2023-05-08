using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTunnel_Script : MonoBehaviour
{
    public ParticleSystem tunnel;
  
    public Color[] colors = { new Color(1, 0, 0), new Color(0, 1, 0) };
    int color_index = 0;
    // Start is called before the first frame update
    void Start()
    {


        if (!GameManager.instance.missionSuccess)
            color_index = 0;
        else
            color_index = 1;

        tunnel.startColor = colors[color_index];
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp("c")) 
        //{
        //    color_index = color_index <= 0 ? 1 : 0;

        //    tunnel.startColor = colors[color_index];
        //}
    }
}
