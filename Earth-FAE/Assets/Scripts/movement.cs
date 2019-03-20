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
            t.transform.position = this.transform.position;
            t.hideMoveableTiles();
            t.actions--;
        }
    }
}
