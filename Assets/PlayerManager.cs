using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class PlayerManager : MonoBehaviour
{
    AudioSource audioSource => GetComponent<AudioSource>();
    public GameObject particle => Resources.Load<GameObject>("Explosion");
    public GameManager gm => GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    public DataManager dm => GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
    public LineRenderer lr => GetComponent<LineRenderer>();
    public KeyCode action = KeyCode.DownArrow;
    public KeyCode bombAction = KeyCode.UpArrow;

    private float angle = 180;

    public float rotationSpeed = 1.5f;

    static float maximum = 270;
    static float minimum = 90;
    static float t = 0.0f;

    public bool dragging = false;

    public uint currency = 0;

    private GameObject draggingItem;

    private void Awake() { angle = Random.Range(270, 90); }

    public void Update()
    {
        if (Input.GetKeyDown(action)) { Shoot(); }
        Rotate();
        DrawHook();
        ThrowBomb();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position,  RotateVector2D(transform.position, angle));
        Gizmos.color = Color.blue;
    }

    public void ThrowBomb()
    {
        if (!Input.GetKeyDown(bombAction) || draggingItem == null) { return; }
        if(dm.inventory.bombs <= 0) { return; }
        dm.inventory.bombs--;
        AudioSource.PlayClipAtPoint(gm.effects[0].effect, draggingItem.transform.position);
        GameObject GO = Instantiate(particle, draggingItem.transform.position, Quaternion.identity);
        Destroy(draggingItem);
        draggingItem = null;
        dragging = false;
        Destroy(GO, 2);
    }
    
    private void Rotate()
    {
        if (dragging) { return; }
        angle = Mathf.Lerp(maximum, minimum, t);

        t += 0.5f * Time.deltaTime * rotationSpeed;

        if(t > 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }
    }

    private Vector3 RotateVector2D(Vector3 vector, float angle)
    {
        return Quaternion.Euler(angle * Vector3.forward) * new Vector3(0, vector.y, vector.z).normalized;
    }

    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, RotateVector2D(transform.position, angle));
        if (hit.collider != null) 
        {
            hit.transform.GetComponent<ItemManager>().Move(this);
            dragging = true;
            draggingItem = hit.transform.gameObject;
        }

        
    }

    private void DrawHook()
    {
        if (dragging) { return; }
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + RotateVector2D(transform.position, angle) * 0.25f);
    }
}
