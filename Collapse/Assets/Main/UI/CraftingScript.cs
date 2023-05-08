using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingScript : MonoBehaviour
{
    public Button armor_menu_button;
    public Button weapon_menu_button;
    public Canvas WeaponMenu;
    public Canvas ArmorMenu;
    public TMP_Text fuel_amount;
    public TMP_Text survival_amount;
    public TMP_Text mineral_amount;

    public Resource_Handler rh;

    public int fuel;
    public int survival;
    public int minerals;

    public bool update_resource;


    // Start is called before the first frame update
    void Start()
    {
        armor_menu_button.onClick.AddListener(ActivateArmorMenu);
        weapon_menu_button.onClick.AddListener(ActivateWeaponMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if(update_resource) 
        {
            update_resource_text();
        }
    }

    private void update_resource_text()
    {
        fuel = rh.GetFuel();
        survival = rh.GetSurvival();
        minerals = rh.GetMinerals();

        fuel_amount.text = fuel.ToString();
        survival_amount.text = survival.ToString();
        mineral_amount.text = minerals.ToString();

        update_resource = false;
    }

    void OnEnable()
    {
        update_resource= true;
    }
    void ActivateWeaponMenu() 
    { 
        WeaponMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void ActivateArmorMenu()
    {
        ArmorMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
