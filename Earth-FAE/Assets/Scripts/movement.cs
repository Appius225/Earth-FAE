using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    void OnMouseDown()
    {
        GameObject[] tanks = GameObject.FindGameObjectsWithTag("Moving");
        foreach (GameObject tank in tanks)
        {
            Tank t = (tank.GetComponent(typeof(Tank))) as Tank;
            Vector3 pos = t.transform.position;
            BoardManager grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
            GameObject cur = grid.objects[(int)Mathf.Floor(pos.x), (int)Mathf.Round(pos.y / 0.75f)];
            tileData tile = cur.GetComponent(typeof(tileData)) as tileData;
            tile.tank = null;
            t.transform.position = this.transform.position;
            cur = grid.objects[(int)Mathf.Floor(this.transform.position.x), (int)Mathf.Round(this.transform.position.y / 0.75f)];
            tile = cur.GetComponent(typeof(tileData)) as tileData;
            tile.tank = t;
            t.hideMoveableTiles();
            t.actions--;
        }
    }
}
