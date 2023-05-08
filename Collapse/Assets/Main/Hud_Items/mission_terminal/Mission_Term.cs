using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Mission_Term : MonoBehaviour, IInteractable
{
    public bool activate_menu = false;
    public Canvas mission_UI;
    public GameManager game_manager;
    private Base_Quest quest = null;
    private List<Base_Quest> quests;
    private List<Button> mission_list;
    private Button[] buttons;

    private void Awake()
    {
        
        buttons = mission_UI.GetComponentsInChildren<Button>();
       
    }

    private void Start()
    {
        Generate_Quest();
        mission_UI.enabled = false;
    }


    public void Interact() 
    {
      
        activate_menu = !activate_menu;
        mission_UI.enabled = activate_menu;
       

        if(game_manager != null) 
        {
            game_manager.gamePaused = !game_manager.gamePaused;
        }

        //buttons[0].GetComponent<Generate_Missions>().DisplayInfo();
   
    }

    private void pull_quests() 
    {
        float chance = Random.Range(0.0f, 1.0f);
        quest = pull_quest_type(chance);
        Debug.Log("Chance: " + chance);
    }

    private Base_Quest pull_quest_type(float chance) 
    {
        //transform into a child type.
        if(chance < .20f) 
        {
            quest = new KB_Quest();
        }
        else if(chance < .35f) 
        {
            quest = new Extract_Quest();
        }
        else
            quest = new KC_Quest();

        return quest;
    }

    void Generate_Quest() 
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            float chance = Random.Range(0.0f, 1.0f);
            Base_Quest temp = pull_quest_type(chance);
            temp.obtain_quest();
            buttons[i].GetComponent<Generate_Missions>().DisplayInfo(temp);
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        Generate_Quest();
    }
    public Base_Quest get_quest() { return quest; }


}
