using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_IUI : MonoBehaviour
{
    public GameObject interact_ui;
    GameObject c;
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
            if (c == null)
                c = Instantiate(interact_ui);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(c != null)
                Destroy(c);

        }
    }
}
