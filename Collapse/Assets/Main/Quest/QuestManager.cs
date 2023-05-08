using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance = null;
    public GameManager GameManager = null;
    public Base_Quest quest; // the quest being executed;
    public ToMission teleporter;

    [Header("Mission UI")]
    public Canvas Mission_UI = null;
    
    [Header("Mission Info")]
    public int current_to = 0; // current goal until completion.
    public int completion_req = 0;// the completed goal requirements
    public int difficulty = 0; // the difficulty of the quest
    public  string stage = "";
    public string mission_type = "N/A"; // the mission type of the executed quest;
    public string mission_description = "N/A";
    public string goal = "N/A";
    public string reward_type = "N/A";
    public int to_reward = 0; // the multiplier that is going to be used to give reward
    public bool quest_complete = false;


    private void Start()
    {
        if(GameManager == null) 
            GameManager = FindAnyObjectByType<GameManager>();

        
    }
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this);
        }

    }

    public string confirm() { return "I'm here"; }
    public void set_quest(int dif, int m_type, int reward, int complete_req, int stage_int)
    {
        difficulty = dif;
        mission_type = recieve_quest(m_type);
        mission_description = quest_description(m_type);
        to_reward = calculate_reward(dif, m_type);
        completion_req = complete_req;
        stage = set_stage(stage_int);

     
        //stage = "Battle_Forest";
        //mission_type = "Extraction";
        if (teleporter == null)
            teleporter = FindObjectOfType<ToMission>();
        teleporter.stage = stage;
        teleporter.readyToGo = true;
        //if (GameManager != null)
        //{
        //    if(GameManager.player.GetComponent<Player_Controller>().weapons.Count > 0)
        //        GameManager.saveLoadout();
        //    GameManager.GetComponent<LevelControl>().LoadLevel(stage);
        //}
    }
    private void Update()
    {
        if (!GameManager.instance.inHub)
        {

            quest_complete = check_complete();
            update_goal();

            if (quest_complete)
            {
                GameManager.missionSuccess = true;
                GameManager.SetCompleteTarget();
                GameManager.cacheReward(to_reward);
                
            }
        }
    }

    bool check_complete()
    {
        return current_to >= completion_req;
    }

    void display_reward()
    {

    }
    void give_reward()
    {

    }

    int calculate_reward(int diff, int m_type)
    {

        return (m_type + 1) * (diff + 1);
    }

    string recieve_quest(int type)
    {
        string m_type = "";
        switch (type)
        {
            case 0:
                m_type = "Kill Quest";
                break;
            case 1:
                m_type = "Extraction";
                break;
            case 2:
                m_type = "Kill Elite";
                break;
        }

        return m_type;
    }

    string quest_description(int m_type) 
    {
        string temp = "N/A";
        switch (m_type) 
        {
            case 0:
                temp = "Hunt";
                break;
            case 1:
                temp = "Extract Mineral";
                break;
            case 2:
                temp = "Eliminate Elite";
                break;
        }

        return temp;
    }

    void set_description(string description)
    {
        mission_description = description;
    }

    private void update_goal()
    {
        goal = current_to.ToString() + " / " + completion_req.ToString();
    }

    string set_stage(int stage_int) 
    {
        string temp = "";

        switch (stage_int) 
        {
            case 0:
                temp = "Battle_Tropical";
                break;
            case 1:
                temp = "Battle_Forest";
                break;
            case 2:
                temp = "Battle_Desert";
                break;
    
        }
        return temp;
    }

    public void ResetQuests()
    {
        current_to = 0; 
        completion_req = 0;
        difficulty = 0;
        stage = "";
        mission_type = "N/A"; 
        mission_description = "N/A";
        goal = "N/A";
        to_reward = 0; 
        quest_complete = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        teleporter = FindObjectOfType<ToMission>();
    }

    #region cheats
    public void cheat_complete() 
    {
        current_to = completion_req;
    }
    #endregion
}
