using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using UnityEngine.SceneManagement;

public class Generate_Missions : MonoBehaviour
{
    public Base_Quest quest = null;
    public Mission_Term MT;
    public TMP_Text Mission_Type;
    public TMP_Text Stage_Type;
    public TMP_Text Difficulty_Text;
    public TMP_Text Reward_Type;
    public TMP_Text Difficulty_Multiplier;
    public Image MissionBackground = null;
    public Sprite desert;
    public Sprite tropical;
    public Sprite forest;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Execute);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayInfo(Base_Quest quest) 
    {
        this.quest = quest;
        Mission_Type.text = quest.get_name();
        Stage_Type.text = quest.get_stage_name();
        SetBackground(quest.get_quest_stage());
        Difficulty_Text.text = diff_text(quest.get_difficulty());
        Reward_Type.text = quest.get_reward_type();
        Difficulty_Multiplier.text = quest.get_reward_mult().ToString() + "x";
       
    }

    void SetBackground(int stage_int) 
    {
       
        switch (stage_int) 
        {
            case 0:
                MissionBackground.sprite = tropical;
                break;
            case 1:
                MissionBackground.sprite = forest;
                break;
            case 2:
                MissionBackground.sprite = desert;
                break;
        }
    }

    string diff_text(int difficulty) 
    {
        string temp = "";
        switch(difficulty) 
        {
            case 0:
                temp = "Easy";
                break;
            case 1:
                temp = "Medium";
                break;
            case 2:
                temp = "Hard";
                break;
        }

        return temp;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    
        
    }

    public void Execute()
    {
        GameManager.instance.rewardType = quest.get_reward_type();
        quest.Execute();
        MT.mission_UI.enabled = false;
        GameManager.instance.gamePaused = false;
    }
}
