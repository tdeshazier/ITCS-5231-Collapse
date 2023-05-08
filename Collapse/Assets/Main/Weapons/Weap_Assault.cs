using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weap_Assault : Weapon_Func
{
    protected override void Update() => base.Update();
    protected override void Awake()
    {
        base.Awake ();
        weapon_name = "Assault";
        held_position = new Vector3(0.11f, 0.063f, 0.005f);
        held_rotation = new Vector3(-2.1f, 58, -60);
    }

    public override bool CanShoot() => base.CanShoot();

    public override void Fire_Weapon(GameObject origin)
    {
        base.Fire_Weapon(origin);
    }

    public override void Holster_Weapon()
    {
        var player = GameManager.instance.player.GetComponent<Player_Controller>();
        transform.SetParent(player.transform);
        transform.position = player.holster_back_slot.transform.position;
        transform.rotation = player.holster_back_slot.transform.rotation;
    }
}
