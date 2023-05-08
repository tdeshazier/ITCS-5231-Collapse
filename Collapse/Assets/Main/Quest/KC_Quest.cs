using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KC_Quest : Base_Quest
{
    // Start is called before the first frame update
    protected override void Start() => base.Start();

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void generate_quest()
    {
        quest_name = "Exterminate";
        Difficulty = Diff_Chance();
        completion_reqs = kill_amount(Difficulty);
        reward_mult = 1 + Difficulty;
        quest_stage = stage_chance();
        set_stage_name(quest_stage);
        set_reward_type();
    }
    public override void Execute()
    {
        QuestManager temp = FindObjectOfType<QuestManager>();
        temp.set_quest(Difficulty, 0, reward_mult, completion_reqs, quest_stage);
    }
    protected override int Diff_Chance()
    {
        float chance = Random.Range(0.0f, 1.0f);
        int diff = 0;

        if (chance <= high_chance)
        {
            diff = 0;
        }
        else if (chance <= (med_chance + high_chance))
        {
            diff = 1;
        }
        else
            diff = 2;

        return diff;
    }

    protected int kill_amount(int diff) 
    {
        int diff_c = diff + 1;
        int amount = diff_c * 30;

        return Random.Range(amount - 10, amount);

    } 
}
