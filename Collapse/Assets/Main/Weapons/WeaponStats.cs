using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int upgrade_index1 = -1;
    public int upgrade_index2 = -1;
    public int upgrade_index3 = -1;

    public int upgrade1_stat = 0;
    public int upgrade2_stat = 0;
    public int upgrade3_stat = 0;

    public bool weapon_crafted1 = true;
    public bool weapon_crafted2 = false;
    public bool weapon_crafted3 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void navigate_upgrade(int index, int upgrade_index) 
    {
        upgrade_index += index;
       
    }

    public virtual double Get_Upgrade1(bool right) { return 0; }
    public virtual double Get_Upgrade2(bool right) { return 0; }
    public virtual double Get_Upgrade3(bool right) { return 0; }

    public void SetUpgradeStat1() { upgrade1_stat = upgrade_index1;  }
    public void SetUpgradeStat2() { upgrade2_stat = upgrade_index2; }

    public void SetUpgradeStat3() { upgrade3_stat = upgrade_index3; }

    public virtual string Get_UpgradeName(int i) { return ""; }
}
