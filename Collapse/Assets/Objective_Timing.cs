using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective_Timing : MonoBehaviour
{
    [SerializeField] Extraction_Objective obj;
    [SerializeField] Slider slider;
    public bool countdown = false;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>(); 
        this.gameObject.SetActive(false);
        obj = FindObjectOfType<Extraction_Objective>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown) 
        {
            float value = obj.extraction_time / obj.max_extraction_time;
            slider.value = Mathf.Clamp01(value);
        }
    }


    private void OnEnable()
    {
        countdown = true;
    }
}
