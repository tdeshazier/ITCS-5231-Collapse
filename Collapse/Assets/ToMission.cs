using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMission : MonoBehaviour
{
    public Glow_Toggle glow;
    public bool readyToGo = false;
    public string stage = "N/A";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        if (readyToGo)
            glow.ToggleGlow();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!readyToGo)
                Debug.Log("Select a mission");
            else
            {
                EnteringMission();
            }
        }
    }

    private void EnteringMission()
    {

        if (GameManager.instance.player.GetComponent<Player_Controller>().weapons.Count > 0)
            GameManager.instance.saveLoadout();
        GameManager.instance.GetComponent<LevelControl>().LoadLevel(stage);
    }
  
}
//if (GameManager != null)
//{
//    if (GameManager.player.GetComponent<Player_Controller>().weapons.Count > 0)
//        GameManager.saveLoadout();
//    GameManager.GetComponent<LevelControl>().LoadLevel(stage);
//}
