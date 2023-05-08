using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{

    public Player_Controller player;
    public bool player_has_weapon = false;
    public Weapon_Func weapon;
    public TextMeshProUGUI weaponText;
    public GameObject Canvas;
    GameObject c;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<Player_Controller>().gameObject != null)
        {
            player = FindObjectOfType<Player_Controller>();
            
            //weapon = player.GetComponent<Player_Controller>().weapon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.has_Weapon)
        {
            if (weapon == null || player.weapon_change)
            {
                weapon = player.weapon;
            }
            else
                weaponText.text = weapon.name + ":" + weapon.current_ammo + " / " + weapon.max_ammo;

            if (weapon.reload_needed)
            {
                if (slider == null)
                {
                    c = Instantiate(Canvas);
                    if (c != null)
                        slider = c.GetComponentInChildren<Slider>();
                }
                else
                    slider.value = Mathf.Clamp01(weapon.reload_speed / weapon.max_reload_speed);
            }
            else
            {

                if (slider != null)
                {
                    Destroy(c);
                    slider = null;
                }
            }
        }
        else 
            weaponText.text = "Weapon - None";
    }
}
