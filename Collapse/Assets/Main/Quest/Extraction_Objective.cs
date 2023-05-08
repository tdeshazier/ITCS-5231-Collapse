using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Extraction_Objective : MonoBehaviour, IInteractable
{
    public bool objective_done = false;
    public float extraction_time = 120.0f;
    public float max_extraction_time = 0.0f;
    public float time_break = 30.0f;
    public float break_chance = .25f;
    public bool timerOn = false;
    WaveSpawner spawner = new WaveSpawner();
    [SerializeField] RetrieveUI Battle_UI;
    [SerializeField] ParticleSystem beacon;
    [SerializeField] ParticleSystem smoke;
    GameObject timer_gauge;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponentInChildren<WaveSpawner>();
        extraction_time = SetTimer();
        max_extraction_time = extraction_time;
        smoke.Play();
        beacon.Stop();
        Battle_UI = FindObjectOfType<RetrieveUI>();
        if (Battle_UI != null)
        {
            timer_gauge = Battle_UI.GetExtractionGauge();
            timer_gauge.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn) 
        {
            if (extraction_time > 0)
            {
                extraction_time -= Time.deltaTime;
                time_break -= Time.deltaTime;

                if (time_break < 0)
                {
                    ExtractingGood();
                    time_break = 30.0f;
                }
            }
            else
            {

                objective_done = true;
                QuestManager.instance.current_to++;
                timer_gauge.SetActive(false);
                timerOn = false;
                smoke.Stop();
                beacon.Stop();
            }
        }
    }

    float SetTimer() 
    {
        var value = 40.0f * (GameManager.instance.difficulty);
        return value;
    }
    public void Interact() 
    {
        if (!objective_done)
        {

            timerOn = true;
            if (!timer_gauge.active)
                timer_gauge.SetActive(true);
            timer_gauge.GetComponent<Objective_Timing>().countdown = true;
            print(extraction_time);
            beacon.Play();
            smoke.Stop();
            spawner.GenerateWave();
        }
        
    }

    void ExtractingGood() 
    {
        float random = Random.Range(0.0f, 1.0f);
      

        if (random <= break_chance) 
        {
            timerOn = false;
            timer_gauge.GetComponent<Objective_Timing>().countdown = false;
            smoke.Play();
            beacon.Stop();
        }
    }
}
