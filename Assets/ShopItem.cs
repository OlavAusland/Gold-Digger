using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    SpeedPotion = 0,
    MarketPotion = 1,
    TimePotion = 2,
    Bomb = 3
};

public class ShopItem : MonoBehaviour
{
    DataManager dm => GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
    public ItemType item;
    public uint cost;

    public void Buy()
    {
        if(dm.currency < cost) { return; }
        dm.currency -= cost;
        switch (item)
        {
            case ItemType.SpeedPotion:
                dm.inventory.speedPotions++;
                break;
            case ItemType.MarketPotion:
                dm.inventory.marketPotions++;
                break;
            case ItemType.TimePotion:
                dm.inventory.timePotions++;
                break;
            case ItemType.Bomb:
                dm.inventory.bombs++;
                break;
            default:
                break;
        }
    }
}
