using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Func : MonoBehaviour
{
    public ItemData weapon_stats;
    public Bullet_pool bullet_pool;
    public Transform muzzle;
    public Transform Player_Weapon_Slot;
    public Transform Player_Holster_Slot;
    public Vector3 held_position;
    public Vector3 held_rotation;

    public Player_Controller players;

    public string Origin = "N/A";

    public Transform hand;

    public int current_ammo;
    public int max_ammo;
    public bool isEnemy;
    public bool isPlayer;

    public float bullet_speed;
    public float fire_rate;
    public float reload_speed = 3.0f;
    public float max_reload_speed = 3.0f;
    private float player_reload = 3.0f;

    protected float last_shot_taken;
    public string weapon_name = "N/A";
    protected string prefab_name = string.Empty;
    public bool reload_needed = false;
    bool infinite = false;

    

    

    protected virtual void Awake()
    {
        if (GetComponent<Player_Controller>()) 
        {
            isPlayer = true;
            isEnemy = false;
        }

        bullet_pool = FindObjectOfType<Bullet_pool>();

       
    }

    public virtual bool CanShoot() 
    {
        if(Time.time - last_shot_taken >= fire_rate) 
        {
            if(current_ammo > 0 || isEnemy) 
            {
             
                return true;
            }
        }
        return false;
    }

    public virtual void Fire_Weapon(GameObject origin) 
    {
        last_shot_taken = Time.time;

        if(!infinite) 
            current_ammo--;

        GameObject bullet = bullet_pool.getBullet();

        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;

        bullet.GetComponent<Bullet_scr>().origin = origin;
       

        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bullet_speed;
        
    }

   
    // Start is called before the first frame update
    void Start()
    {
   
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (reload_needed) 
        {
            if(reload_speed > 0) 
            {
                reload_speed -= Time.deltaTime;
            }
            else if(reload_speed <= 0) 
            {
                current_ammo = max_ammo;
                reload_needed = false;
                reload_speed = player_reload;
            }
        }
    }

    public string return_PerfName() { return prefab_name; }

    public void SetToPlayer() 
    {
        var player = GameManager.instance.player.GetComponent<Player_Controller>();
        var equip = Player_Equipment.instance;
        EquipWeapon();
        equip.AddWeapon(this);
        player.SetWeapons();

    }

    public void EquipWeapon() 
    {
        var player = GameManager.instance.player.GetComponent<Player_Controller>();
        transform.SetParent(player.hand);
        //transform.position = player.weapon_slot.transform.position;
        //transform.rotation = player.weapon_slot.transform.rotation;

        transform.localPosition = held_position;
        transform.localEulerAngles = held_rotation;
    }

    public virtual void Holster_Weapon() 
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer)
        {
            if (other.tag == "Player")
            {
                if (other.GetComponent<Player_Controller>().weapons.Count < 2)
                {

                    SetToPlayer();

                    
                    //print("Player Pickup");
                    //transform.SetParent(other.transform);
                    //transform.position = other.GetComponent<Player_Controller>().weapon_slot.transform.position;
                    //transform.rotation = other.GetComponent<Player_Controller>().weapon_slot.transform.rotation;
                    ////transform.position = Player_Weapon_Slot.transform.position;
                    ////transform.rotation = Player_Weapon_Slot.transform.rotation;

                    ////Add weapon to player weapon slot;
                    //if (!other.GetComponent<Player_Controller>().weapons.Contains(this))
                    //{

                    //    other.GetComponent<Player_Controller>().weapons.Add(this);
                    //    other.GetComponent<Player_Controller>().has_Weapon = true;
                    //    other.GetComponent<Player_Controller>().weapon_change = true;
                    //}

                    ////Don't swap weapons yet
                    //if (other.GetComponent<Player_Controller>().weapon == null)
                    //{
                    //    other.GetComponent<Player_Controller>().weapon = this;
                    //}
                    //else
                    //    gameObject.SetActive(false);
                }
            }
        }
    }


    #region cheats
    public void InfAmmo() 
    {
        infinite = !infinite;
    }
    #endregion

}
