using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Quest : MonoBehaviour
{
    protected string quest_name = string.Empty;
    protected string stage_name = string.Empty;
    protected string reward_type = string.Empty;
    protected int Difficulty = 0; // 0 = easy, 1 = med, 2 = hard
    protected int mission_type = 0; // 0 = kill_quest, 1 = extraction, 2 = kill_boss
    protected int reward_mult = 1; // multiplier depending on mission type and difficulty.
    protected int completion_reqs = 0; // the max req to be completed before mission successful;
    protected int quest_stage = 0; // 0 = tropical, 1 = forest, 2 = desert
    protected float high_chance = .45f;
    protected float med_chance = .35f;
    protected float low_chance = .20f;
    protected QuestManager q_manager = null;

    protected virtual void Start()
    {
        q_manager = FindObjectOfType<QuestManager>();

        
        
    }


    public virtual void Execute() { }
   

    protected virtual int Diff_Chance() { return 0; }


    protected virtual void generate_quest() { }


    protected virtual int stage_chance()
    {
        return Random.Range(0, 3);
    }

    protected virtual void set_stage_name(int stage) 
    {
        switch (stage) 
        {
            case 0:
                stage_name = "Tropical";
                break;
            case 1:
                stage_name = "Forest";
                break;
            case 2:
                stage_name = "Desert";
                break;
        }
    }

    public virtual void obtain_quest()
    {
        generate_quest();
    }

    protected virtual void set_reward_type()
    {
        int chance = Random.Range(0, 3);
        switch (chance)
        {
            case 0: // tropical
                reward_type = "Food/Water";
                break;
            case 1: //forest
                reward_type = "Biomass";
                break;
            case 2: // Desert
                reward_type = "Minerals";
                break;
        }
    }
    
    public virtual string get_name() { return quest_name; }
    public virtual string get_stage_name() { return stage_name; }
    public virtual string get_reward_type() { return reward_type; }
    public virtual int get_difficulty() { return Difficulty; }
    public virtual int get_mission_type() { return mission_type; }
    public virtual int get_reward_mult() { return reward_mult; }
    public virtual int get_completion_reqs() { return completion_reqs; }
    public virtual int get_quest_stage() { return quest_stage; }
   

    
}
