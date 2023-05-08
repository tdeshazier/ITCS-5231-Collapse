using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ResourceDisplay : MonoBehaviour
{
    public Resource_Handler rh;
    public TextMeshProUGUI resourceText;
    public bool isFuel = false;
    public bool isSurvival = false;
    public bool isMineral = false;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<Resource_Handler>().gameObject != null)
        {
            rh = FindObjectOfType<Resource_Handler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rh != null)
        {
            if (isFuel)
                resourceText.text = "Biomatter: " + rh.GetFuel();
            if(isSurvival)
                resourceText.text = "Food/Water: " + rh.GetSurvival();
            if (isMineral)
                resourceText.text = "Mineral: " + rh.GetMinerals();
           
        }
    }
}
