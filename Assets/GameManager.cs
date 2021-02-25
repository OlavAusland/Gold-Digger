using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public string name;
    public uint value;
    public Sprite sprite;

    public uint count;

    [Range(0, 3)]
    public float minSize = 1;
    [Range(0, 3)]
    public float maxSize = 1;

    public float speed = 1;
}

[Serializable]
public class LevelLayout
{
    public string name = "Level";
    public List<Item> items;
}

public class GameManager : MonoBehaviour
{
    DataManager dataManager => GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();

    public Vector3[] viewBoundary = new Vector3[4];
    public Vector3[] groundBoundary = new Vector3[4];
    public readonly Vector3[] boundary = new Vector3[4] { new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 0) };

    public List<LevelLayout> levelLayout;
    private int level = 0;

    public GameObject itemPrefab => Resources.Load<GameObject>("Item");

    public List<GameObject> instances;
    [Space(20)]
    public uint spacing = 10;
    public uint iterations;

    public GameObject ground;
    [Space(20)]
    public Text currencyText;
    public uint currency = 0;
    [Space(20)]
    public Text timeText;
    public float time = 60;
    [Space(20)]
    public uint moneyMultiplier = 1;
    public float speedMultiplier = 1;
    [Space(20)]
    public GameObject menu;

    private void Start()
    {
        ResizeBoundaries();
        SpawnItems();
        ResizeGround(groundBoundary[0], groundBoundary[3]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){ ToggleMenu(); }
        currency = dataManager.currency;
        currencyText.text = currency.ToString("0$");
        timeText.text = time.ToString("0.0S");
        time -= Time.deltaTime;

        //CHANGE
        if(time <= 0) { Application.LoadLevel(2); }

    }

    public void ToggleMenu()
    {
        menu.SetActive(!menu.gameObject.active);

        if (menu.gameObject.active) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector3 point in viewBoundary)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }

        Gizmos.color = Color.green;
        foreach(Vector3 point in groundBoundary)
        {
            Gizmos.DrawSphere(point, 0.05f);
        }

        Gizmos.DrawLine(groundBoundary[0], groundBoundary[1]);
    }

    private void SpawnItems()
    {
        List<Item> levelItems = levelLayout[0].items;

        foreach(Item item in levelItems)
        {
            for(int i = 0; i < item.count; i++)
            {
                GameObject GO = Instantiate(itemPrefab, RandomGroundPosition(), Quaternion.identity);
                GO.GetComponent<SpriteRenderer>().sprite = item.sprite;
                GO.GetComponent<ItemManager>().item = item;
                GO.name = item.name;
                instances.Add(GO);
            }
        }
    }

    private void ResizeBoundaries()
    {
        for(int i = 0; i < boundary.Length; i++)
        {
            viewBoundary[i] = Camera.main.ViewportToWorldPoint(boundary[i]);
            groundBoundary[i] = viewBoundary[i];
        }

        groundBoundary[0].y -= (groundBoundary[0].y / 1.5f);
        groundBoundary[1].y -= (groundBoundary[1].y / 1.5f);
    }
    
    private Vector3 RandomGroundPosition()
    {
        Vector3 position = new Vector3();

        for(int i = 0; i < iterations; i++)
        {
            position.x = UnityEngine.Random.Range(groundBoundary[0].x, groundBoundary[1].x);
            position.y = UnityEngine.Random.Range(groundBoundary[0].y - 1, groundBoundary[3].y + 0.25f);
            if(CheckDistance(position)) { break; }
        };


        return position;
    }

    private bool CheckDistance(Vector3 position)
    {
        foreach(GameObject obj in instances)
        {
            if(Vector3.Distance(position, obj.transform.position) < (spacing / 10)) { return false; }
        }

        return true;
    }

    private void ResizeGround(Vector3 pointA, Vector3 pointB)
    {
        float spriteSize = ground.GetComponent<SpriteRenderer>().sprite.rect.width / ground.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        Vector3 scale = ground.transform.localScale;            
        scale.x = (pointA.x - pointB.x) / spriteSize;
        scale.y = (pointA.y - pointB.y) / spriteSize;
        ground.transform.position = new Vector3(0, (pointA.y + pointB.y) / 2, 1);
        ground.transform.localScale = scale;
    }
}

