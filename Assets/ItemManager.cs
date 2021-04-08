using UnityEngine;

public class ItemManager : MonoBehaviour
{
    PlayerManager pm;
    public Item item;
    public bool move;
    public Vector3 target;

    private void Start()
    {
        float size = Random.Range(item.minSize, item.maxSize);
        transform.localScale = new Vector3(size, size, 0);
    }

    public void Update()
    {
        if (!move) { return; }
        transform.position = Vector3.MoveTowards(transform.position, target, (Time.deltaTime * item.speed * pm.gm.speedMultiplier) / transform.localScale.x);
        CheckDistance();
        DrawHook();
    }

    public void CheckDistance()
    {
        if (Vector3.Distance(target, transform.position) <= 0)
        {
            pm.dragging = false;
            pm.dm.currency += item.value * pm.gm.moneyMultiplier;
            AudioSource.PlayClipAtPoint(pm.gm.effects[1].effect, pm.transform.position);
            Destroy(transform.gameObject);
            pm.gm.time += 5; // CHANGE VALUE FOR BALANCING
        }
    }

    public void Move(PlayerManager player){move = true; target = player.transform.position; pm = player; }

    private void DrawHook()
    {
        pm.lr.SetPosition(0, target);
        pm.lr.SetPosition(1, transform.position);
    }
}
