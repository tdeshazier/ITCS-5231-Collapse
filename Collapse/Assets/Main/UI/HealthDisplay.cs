using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthDisplay : MonoBehaviour
{
    public Player_Controller player;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<Player_Controller>().gameObject != null) 
        {
            player = FindObjectOfType<Player_Controller>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            healthText.text = "Health: " + player.hp + " / " + player.max_hp;
        }
    }
}
