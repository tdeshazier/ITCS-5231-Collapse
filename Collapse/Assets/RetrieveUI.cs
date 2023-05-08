using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveUI : MonoBehaviour
{
    [SerializeField] TextDisplay mintext;
    [SerializeField] TextDisplay fueltext;
    [SerializeField] TextDisplay survtext;
    [SerializeField] GameObject extraction_gauge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TextDisplay GetMinText() { return mintext; }
    public TextDisplay GetFuelText() { return fueltext; }
    public TextDisplay GetSurvText() { return survtext; }
    public GameObject GetExtractionGauge() { return extraction_gauge; }
}
