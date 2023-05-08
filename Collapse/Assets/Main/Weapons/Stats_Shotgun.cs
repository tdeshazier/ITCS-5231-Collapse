using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats_Shotgun : WeaponStats
{
    [SerializeField]
    public string[] upgrades = { "Damage", "Accuracy", "Reload Speed" };

    public double[] damage_levels = { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
    public double[] Accuracy_levels = { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
    public double[] Reload_levels = { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override string Get_UpgradeName(int i) { return upgrades[i]; }
    public override double Get_Upgrade1(bool right) // Damage
    {

        if (right)
            upgrade_index1++;
        else
            upgrade_index1--;

        if (upgrade_index1 > 9)
            upgrade_index1 = 0;

        if (upgrade_index1 < 0)
            upgrade_index1 = 9;

        return damage_levels[upgrade_index1];
    }
    public override double Get_Upgrade2(bool right) // Fire Rate
    {
        if (right)
            upgrade_index2++;
        else
            upgrade_index2--;

        if (upgrade_index2 > 9)
            upgrade_index2 = 0;

        if (upgrade_index2 < 0)
            upgrade_index2 = 9;
        return Accuracy_levels[upgrade_index2];
    }
    public override double Get_Upgrade3(bool right) // Reload
    {
        if (right)
            upgrade_index3++;
        else
            upgrade_index3--;

        if (upgrade_index3 > 9)
            upgrade_index3 = 0;

        if (upgrade_index3 < 0)
            upgrade_index3 = 9;

        return Reload_levels[upgrade_index3];
    }
}
