using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class Player_Equipment : MonoBehaviour
{
    public static Player_Equipment instance { get; private set; }
    // Start is called before the first frame update

    public List<Weapon_Func> weapons;
    public List<string> weapon_prefs;
    //public Armor Armor
    
    
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
    
    public void SwapWeapons() 
    {
        if (weapons.Count <= 1)
            return; 

        var player = GameManager.instance.player.GetComponent<Player_Controller>();
        var next_index = weapons.IndexOf(player.weapon) > 0 ? 0 : 1;

        //holster and disable
        player.weapon.Holster_Weapon();
        player.weapon.enabled = false;

        //enable next weapon and equip
        weapons[next_index].enabled = true;
        player.weapon = weapons[next_index];
        player.weapon.EquipWeapon();
        player.weapon_change = true;

    }
    public void AddWeapon(Weapon_Func weapon) 
    {
        if (!weapons.Contains(weapon) && weapons.Count < 2)
        {

            weapons.Add(weapon);
            weapon_prefs.Add(weapon.name);

            if(weapons.Count > 1 ) 
            {
                weapon.Holster_Weapon();
                weapon.enabled = false; 

            }
            else 
            {
                var player = GameManager.instance.player.GetComponent<Player_Controller>();
                player.weapon = weapon;
                player.has_Weapon = true;
                player.weapon_change = true;
            }
        }
    }

    public void RemoveWeapon(Weapon_Func weapon) 
    {
        if(weapons.Contains(weapon))
            weapons.Remove(weapon);
    }

    public void restoreLoadOut() 
    {
        if (weapon_prefs.Count <= 0)
            return;

        var player = GameManager.instance.player.GetComponent<Player_Controller>();
        List<string> names = new List<string>(weapon_prefs);

        //clear both lists
        weapon_prefs.Clear();
        weapons.Clear();

        //restore everything
        for (int i = 0; i < names.Count; i++)
        {
            string path = "Prefabs/Weapon/" + names[i];
            var weapon = Resources.Load(path) as GameObject;
            if (weapon != null && weapon.GetComponent<Weapon_Func>() != null)
            {
               
                var toPlayer = GameObject.Instantiate(weapon, transform.position, transform.rotation);
                toPlayer.name = names[i];
                toPlayer.GetComponent<Weapon_Func>().SetToPlayer();
            }
        }

        names.Clear();
    }
}
