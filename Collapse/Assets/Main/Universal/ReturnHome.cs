using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnHome : MonoBehaviour
{
    public Glow_Toggle glow;
    bool readyToGo = false;
    public string home = "Hub";
    string reward = "RewardScene";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        readyToGo = ok_togo();
        if(readyToGo)
            glow.ToggleGlow();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (!readyToGo)
                Debug.Log("Mission Not Complete");
            else
            {
                
                ReturningHome();
            }
        }
    }

    private void ReturningHome()
    {
        GameManager.instance.GetComponent<LevelControl>().LoadLevel(reward);
    }
    bool ok_togo() { return QuestManager.instance.quest_complete; }
}
