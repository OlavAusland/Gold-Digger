using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    DataManager dm => GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
    public Text text;   

    private void Update(){ text.text = dm.currency.ToString("0$"); }
    public void LoadLevel(int index)
    {
        Application.LoadLevel(index);
    }
}
