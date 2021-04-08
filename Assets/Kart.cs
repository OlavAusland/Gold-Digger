using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour
{

    public GameManager gm;
    public bool moveLeft;

    void Update()
    {
        Behaviour();
    }

    public void Behaviour()
    {
        Vector3 dir = new Vector3();

        if (moveLeft) { dir = Vector3.left; }
        else { dir = Vector3.right; }

        transform.Translate(dir * 0.002f);

        if(dir == Vector3.left) { if ((transform.position.x) < gm.groundBoundary[0].x - 0.5f) { Destroy(transform.gameObject); } }
        else if (dir == Vector3.right) { if ((transform.position.x) > gm.groundBoundary[1].x + 0.5f) { Destroy(transform.gameObject); } }
    }
}
