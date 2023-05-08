using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_PlayerSM : MonoBehaviour
{
    public Player_Controller player;
    public Animator animator;
    public string current_weapon;
    public string prev_weapon;
    public bool change_stance = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.anim_change)
        //    StateMachine();
    }

    private void FixedUpdate()
    {
        if (player.anim_change)
            StateMachine();
    }

    void StateMachine() 
    {
        if (!player.anim_change)
            return;

        prev_weapon = current_weapon;
        deactivate_weight(prev_weapon);
        current_weapon = player.weapon.name;
        activate_weight(current_weapon);
        player.anim_change = false;
   
    }

    void deactivate_weight(string prev) 
    {
        switch (prev)
        {
            case "Pistol":
                animator.SetLayerWeight(1, 0);
                break;
            case "Shotgun":
                animator.SetLayerWeight(2, 0);
                break;
            case "Assault":
                animator.SetLayerWeight(3, 0);
                break;
            default:
                animator.SetLayerWeight(0, 0);
                break;
        }
    }
    private void activate_weight(string current)
    {
        switch (current)
        {
            case "Pistol":
                animator.SetLayerWeight(1, 1);
                break;
            case "Shotgun":
                animator.SetLayerWeight(2, 1);
                break;
            case "Assault":
                animator.SetLayerWeight(3, 1);
                break;
            default:
                animator.SetLayerWeight(0, 1);
                break;
        }
    }
}
