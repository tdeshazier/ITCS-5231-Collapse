using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Weap_Shotgun : Weapon_Func
{
    private List<GameObject> bullets;
    public int num_spread = 15;
    private int spread_range = 2;
    public float accuracy_spread = 5f;
    private List<float> spread_nums = new List<float> { -15f, -5f, 0, 5f, 15f };

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        weapon_name = "Shotgun";
        held_position = new Vector3(0.11f, 0.063f, 0.005f);
        held_rotation = new Vector3(-2.1f, 58, -60);
    }

    // Update is called once per frame
    protected override void Update() => base.Update();

    public override bool CanShoot() => base.CanShoot();

    public override void Fire_Weapon(GameObject origin) 
    {

        last_shot_taken = Time.time;
        current_ammo--;

        for (int times = 0; times < num_spread / 5; times++)
            for (int i = 0; i < num_spread; i++)
            {
                GameObject bullet = bullet_pool.getBullet();

                bullet.transform.position = muzzle.position;
                bullet.transform.rotation = muzzle.rotation;
                bullet.GetComponent<Bullet_scr>().origin = origin;
                Vector3 bullet_dir = Quaternion.Euler(Random.Range(-spread_range, spread_range), spread_nums[i], Random.Range(-spread_range, spread_range)) * muzzle.forward;
                //Vector3 bullet_dir = GetShotDirection(bullet.transform.position);

                bullet.GetComponent<Rigidbody>().velocity = bullet_dir * bullet_speed;

            }


    }

    Vector3 GetShotDirection(Vector3 position) 
    {
        Vector3 tempDir = position + muzzle.forward;

        tempDir = new Vector3(tempDir.x + Random.Range(-spread_range, spread_range), tempDir.y + Random.Range(-spread_range, spread_range), tempDir.z + Random.Range(-spread_range, spread_range));

        Vector3 dir = tempDir - position;
        return dir.normalized;
    }

    public override void Holster_Weapon()
    {
        var player = GameManager.instance.player.GetComponent<Player_Controller>();
        transform.SetParent(player.transform);
        transform.position = player.holster_back_slot.transform.position;
        transform.rotation = player.holster_back_slot.transform.rotation;
    }
}
