using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using Unity.VisualScripting;

public class Weapon_Craft : MonoBehaviour
{
    public Resource_Handler rh;
    public Player_Equipment pe;
    public Stats_Pistol ps;
    public Stats_Shotgun ss;
    public Stats_Assault a_s;

    public Image weapon_upgrade_menu;
    public Image weapon_crafting_menu;
  
    public ItemData pistol;
    public ItemData Shotgun;
    public ItemData Assault;

    public List<ItemData> weapons_items;

    public List <WeaponStats> weapons;
    public List<GameObject> weapon_models;

    public Button LeftNavi;
    public Button RightNavi;

    public Button U1_LeftNavi;
    public Button U2_LeftNavi;
    public Button U3_LeftNavi;

    public Button U1_RightNavi;
    public Button U2_RightNavi;
    public Button U3_RightNavi;

    public Button Craft_Button;
    public Button Upgrade_Button;

    public TextMeshProUGUI upgrade1_name;
    public TextMeshProUGUI upgrade2_name;
    public TextMeshProUGUI upgrade3_name;

    public TextMeshProUGUI upgrade1_value;
    public TextMeshProUGUI upgrade2_value;
    public TextMeshProUGUI upgrade3_value;

    public Button Exit;
    public TextMeshProUGUI item_name;
    public Image weapon_position;

    public RectTransformUtility convertItemPosition;

    bool update_menu = false;

    public string[] weaponNames = { "Pistol", "Assault", "Shotgun" };

    public Vector2 rotation;

    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        LeftNavi.onClick.AddListener(ScrollLeft);
        RightNavi.onClick.AddListener(ScrollRight);

        U1_LeftNavi.onClick.AddListener(() => { UpgradeScrollLeft(0, false);  });
        U2_LeftNavi.onClick.AddListener(() => { UpgradeScrollLeft(1, false); });
        U3_LeftNavi.onClick.AddListener(() => { UpgradeScrollLeft(2, false); });

        U1_RightNavi.onClick.AddListener(() => { UpgradeScrollRight(0, true); });
        U2_RightNavi.onClick.AddListener(() => { UpgradeScrollRight(1, true); });
        U3_RightNavi.onClick.AddListener(() => { UpgradeScrollRight(2, true); });

        Craft_Button.onClick.AddListener(CreateWeapon);

        if(ps != null)
            weapons.Add(ps);
        
        if(a_s != null)
            weapons.Add(a_s);

        if(ss != null)
            weapons.Add(ss);

        if (pistol != null)
            weapons_items.Add(pistol);
        if (Shotgun != null)
            weapons_items.Add(Shotgun);
        if (Assault != null)
            weapons_items.Add(Assault);

    }

    // Update is called once per frame
    void Update()
    {
        if(update_menu) 
        {
            UpdateMenu();
        }

        if (Input.GetButton("Fire2")) 
        {
            rotation.x += Input.GetAxis("Mouse X") + 0.5f;

            weapon_models[index].transform.localRotation = Quaternion.Euler(0, rotation.x, 0);
        }
    }

    private void OnEnable()
    {
        update_menu= true;

        upgrade1_value.text = "1  " + "-> " + "1.1";
        upgrade2_value.text = "1  " + "-> " + "1.1";
        upgrade3_value.text = "1  " + "-> " + "1.1";

    }


    void UpdateMenu() 
    {
      
        
        item_name.text = weapons_items[index].display_name;

        if (!weapons_items[index].crafted)
        {
            if(weapon_upgrade_menu.IsActive())
                weapon_upgrade_menu.gameObject.SetActive(false);

            if(!weapon_crafting_menu.IsActive())
                weapon_crafting_menu.gameObject.SetActive(true);
        }
        else
        {
            if (weapon_crafting_menu.IsActive())
                weapon_crafting_menu.gameObject.SetActive(false);

            if (!weapon_upgrade_menu.IsActive())
                weapon_upgrade_menu.gameObject.SetActive(true);

            upgrade1_name.text = weapons_items[index].Upgrade1_label;
            upgrade2_name.text = weapons_items[index].Upgrade2_label;
            upgrade3_name.text = weapons_items[index].Upgrade3_label;
        }

        weapon_models[index].SetActive(true);
        update_menu = false;
    }
    void ScrollLeft() 
    {
        weapon_models[index].SetActive(false);
        index--;
        if (index < 0)
            index = 2;

        update_menu = true;
    }

    void ScrollRight() 
    {
        weapon_models[index].SetActive(false);
        index++;
        if (index > 2) 
            index = 0;

        update_menu = true;
    }

    void UpgradeScrollLeft(int i, bool right) 
    {
        double preview_value = 0;
        switch (i)
        {
            case 0:
                //weapons[i].Get_Upgrade1(right);
                preview_value = 1 + weapons[index].Get_Upgrade1(right);
                upgrade1_value.text = "1  " + "-> " + preview_value; 
                break;
            case 1:
                preview_value = 1 + weapons[index].Get_Upgrade2(right);
                upgrade2_value.text = "1  " + "-> " + preview_value;
                break;
            case 2:
                preview_value = 1 + weapons[index].Get_Upgrade3(right);
                upgrade3_value.text = "1  " + "-> " + preview_value;
                break;
        }
        update_menu = true;
    }

    void UpgradeScrollRight(int i , bool right)
    {
        double preview_value = 0;
        switch (i)
        {
            case 0:
                //weapons[i].Get_Upgrade1(right);
                preview_value = 1 + weapons[index].Get_Upgrade1(right);
                upgrade1_value.text = "1  " + "-> " + preview_value;
                break;
            case 1:
                preview_value = 1 + weapons[index].Get_Upgrade2(right);
                upgrade2_value.text = "1  " + "-> " + preview_value;
                break;
            case 2:
                preview_value = 1 + weapons[index].Get_Upgrade3(right);
                upgrade3_value.text = "1  " + "-> " + preview_value;
                break;
        }
        update_menu = true;
    }

    void CreateWeapon() 
    {
        string weapon_path = "Prefabs/Weapon/" + weaponNames[index];
        GameObject temp = (GameObject)Resources.Load(weapon_path);
        GameObject weapon_temp = Instantiate(temp);

        if (weapon_temp != null)
        {
            weapon_temp.name = weaponNames[index];
            weapon_temp.GetComponent<Weapon_Func>().SetToPlayer();
            weapons_items[index].crafted = true;
            update_menu = true;
            this.gameObject.SetActive(false);
        }
        
    }

}
