using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class PlayerManager : MonoBehaviour
{
    public GameManager gm => GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    public LineRenderer lr => GetComponent<LineRenderer>();
    public KeyCode action = KeyCode.DownArrow;

    public float angle = 180;
    public float rotationSpeed = 1.5f;

    static float maximum = 270;
    static float minimum = 90;
    static float t = 0.0f;

    public bool dragging = false;

    public uint currency = 0;

    public void Update()
    {
        if (Input.GetKeyDown(action)) { Shoot(); }
        Rotate();
        DrawHook();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, RotateVector2D(transform.position, angle));
        Gizmos.color = Color.blue;
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

    private Vector3 RotateVector2D(Vector3 oldDirection, float angle)
    {
        float newX = Mathf.Cos(angle * Mathf.Deg2Rad) * (oldDirection.x) - Mathf.Sin(angle * Mathf.Deg2Rad) * (oldDirection.y);
        float newY = Mathf.Sin(angle * Mathf.Deg2Rad) * (oldDirection.x) + Mathf.Cos(angle * Mathf.Deg2Rad) * (oldDirection.y);
        float newZ = oldDirection.z;
        return new Vector3(newX, newY, newZ);
    }

    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, RotateVector2D(transform.position, angle));

        if(hit.collider != null) 
        {
            hit.transform.GetComponent<ItemManager>().Move(this);
            dragging = true;
        }
    }

    private void DrawHook()
    {
        if (dragging) { return; }
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + RotateVector2D(transform.position, angle) * 0.25f);
    }
}
