using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public uint marketPotions = 0;
    public uint speedPotions = 0;
    public uint timePotions = 0;
    public uint bombs = 0;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public Inventory inventory;
    [Space(20)]
    public uint currency = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
