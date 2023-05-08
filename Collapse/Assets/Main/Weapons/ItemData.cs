using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType 
{
    Resources,
    Equipable,
    Consumable
}

[CreateAssetMenu(fileName = "Equipment", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string display_name;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject prefab;


    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Equipable")]
    public bool isWeapon;
    public bool isArmor;
    public int Equipment_Level;

    public string Upgrade1_label;
    public string Upgrade2_label;
    public string Upgrade3_label;

    public int Upgrade1_Level;
    public int Upgrade2_Level;
    public int Upgrade3_Level;

    public int Fuel_Cost;
    public int Mineral_Cost;

    public int craft_fuel_cost;
    public int craft_mineral_cost;
    public bool crafted = false;

    //if not a resource
    public bool is_crafted(int Fuel_Funds, int Mineral_Funds, bool isUpgrade, int upgrade, int upgrade_level) 
    {
        crafted = false;
        if (isUpgrade)
        {
            //switch(upgrade) 
            //{
            //    case 0:
            //        if (Fuel_Funds >= (craft_fuel_cost * (Upgrade1_Level + 1)) && Mineral_Funds >= (craft_mineral_cost * (Upgrade1_Level + 1)))
            //            crafted = true;
            //       break;
            //    case 1:
            //        if (Fuel_Funds >= (craft_fuel_cost * (Upgrade2_Level + 1)) && Mineral_Funds >= (craft_mineral_cost * (Upgrade2_Level + 1)))
            //            crafted = true;
            //        break;
            //    case 2:
            //        if (Fuel_Funds >= (craft_fuel_cost * (Upgrade3_Level + 1)) && Mineral_Funds >= (craft_mineral_cost * (Upgrade3_Level + 1)))
            //            crafted = true;
            //        break;
            //}

           if (Fuel_Funds >= (craft_fuel_cost * (upgrade_level + 1)) && Mineral_Funds >= (craft_mineral_cost * (upgrade_level + 1)))
                crafted = true;
        }
        else
        {
            if (Fuel_Funds >= Fuel_Cost && Mineral_Funds >= Mineral_Cost)
                crafted = true;
        }

        return crafted;
    }

    //Weapon Functions
    public void set_weaponupgrade1(int upgrade) { Upgrade1_Level = upgrade; }
    public void set_weaponupgrade2(int upgrade) { Upgrade2_Level= upgrade; }
    public void set_weaponupgrade3(int upgrade) { Upgrade3_Level = upgrade; }
    //Armor Functions
    public void set_equipmentLevel(int upgrade) { Equipment_Level = upgrade; }
}
