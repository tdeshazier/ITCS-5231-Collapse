using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetup : MonoBehaviour
{
    public List<ItemData> game_items;
    public bool itemsReset = true;
    private void Awake()
    {
        if (itemsReset)
        {
            ResetItems();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void ResetItems()
    {
        for (int i = 0; i < game_items.Count; i++)
        {
            switch (game_items[i].type)
            {
                case ItemType.Resources:
                    break;
                case ItemType.Equipable:
                    game_items[i].Upgrade1_Level = 0;
                    game_items[i].Upgrade2_Level = 0;
                    game_items[i].Upgrade3_Level = 0;
                    game_items[i].Equipment_Level = 0;
                    game_items[i].crafted = false;
                    break;
                case ItemType.Consumable:
                    break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
