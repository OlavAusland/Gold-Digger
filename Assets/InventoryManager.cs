using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    GameManager gm => GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    DataManager dm => GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
    public List<Text> texts;

    public void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        texts[0].text = dm.inventory.marketPotions.ToString();
        texts[1].text = dm.inventory.timePotions.ToString();
        texts[2].text = dm.inventory.speedPotions.ToString();
        texts[3].text = dm.inventory.bombs.ToString();
    }

    public void MarketPotion(int time)
    {
        if(dm.inventory.marketPotions <= 0) { return; }
        gm.moneyMultiplier *= 2;
        dm.inventory.marketPotions--;
        StartCoroutine(RevertMultiplier(time, 0));
    }

    public void TimePotion(int time)    
    {
        if (dm.inventory.timePotions <= 0) { return; }
        gm.time += time;
        dm.inventory.timePotions--;
    }

    public void SpeedPotion(float speed)
    {
        if (dm.inventory.speedPotions <= 0) { return; }
        gm.speedMultiplier *= speed;
        dm.inventory.speedPotions--;
        StartCoroutine(RevertMultiplier(25, 1));
    }

    private IEnumerator RevertMultiplier(int time, int type)
    {
        yield return new WaitForSeconds(time);
        switch(type)
        {
            case 0: gm.moneyMultiplier = 1;break;
            case 1: gm.speedMultiplier = 1;break;
            default:break;
        }   
    }
}
