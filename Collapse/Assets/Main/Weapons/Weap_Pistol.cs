using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weap_Pistol : Weapon_Func
{
    // Start is called before the first frame update
    
    protected override void Awake()
    {
        base.Awake();
        weapon_name = "Pistol";
        held_position = new Vector3(0.073f, 0.042f, 0.0f);
        held_rotation = new Vector3(-9.4f, 79.9f, -61.55f);
    }

    public override bool CanShoot() => base.CanShoot();

    public override void Fire_Weapon(GameObject origin)
    {
        base.Fire_Weapon(origin);
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update() => base.Update();

    public override void Holster_Weapon()
    {
        var player = GameManager.instance.player.GetComponent<Player_Controller>();
        transform.SetParent(player.transform);
        transform.position = player.holster_hip_slot.transform.position;
        transform.rotation = player.holster_hip_slot.transform.rotation;
    }
}
