using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KB_Quest : Base_Quest
{
    // Start is called before the first frame update
    protected override void Start() => base.Start();
  
    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void generate_quest()
    {
        quest_name = "Eliminate Elite";
        Difficulty = Diff_Chance();
        completion_reqs = 1;
        reward_mult = 1 + Difficulty;
        quest_stage = stage_chance();
        set_stage_name(quest_stage);
        set_reward_type();
    }
    public override void Execute()
    {
        QuestManager temp = FindObjectOfType<QuestManager>();
        temp.set_quest(Difficulty, 2, reward_mult, completion_reqs, quest_stage);
        
    }
    protected override int Diff_Chance()
    {
        float chance = Random.Range(0.0f, 1.0f);
        int diff = 0;

        if (chance <= high_chance)
        {
            diff = 1;
        }
        else if (chance <= (med_chance + high_chance))
        {
            diff = 0;
        }
        else
            diff = 2;

        return diff;
    }
}
