using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Temr : MonoBehaviour, IInteractable
{
    public Canvas crafting_UI;
    public bool activate_menu = false;
    public GameManager game_manager;

    public void Start()
    {
        crafting_UI.enabled = false;
    }

    public void Interact()
    {
        Debug.Log("Hi i'm the equipment terminal");
        activate_menu = !activate_menu;
        crafting_UI.enabled = activate_menu;
        

        if (game_manager != null)
        {
            game_manager.gamePaused = !game_manager.gamePaused;
        }
    }

  
}
