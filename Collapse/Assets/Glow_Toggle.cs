using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow_Toggle : MonoBehaviour
{

    public GameObject glow;
    // Start is called before the first frame update
    void Start()
    {
        glow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleGlow() 
    {
        glow.gameObject.SetActive(true);
    }
}
