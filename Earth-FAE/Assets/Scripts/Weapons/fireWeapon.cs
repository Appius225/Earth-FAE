using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireWeapon : MonoBehaviour
{
    void OnMouseDown()
    {
        GameObject[] tanks = GameObject.FindGameObjectsWithTag("Firing1");
        foreach(GameObject tank in tanks)
        {
            Tank t = (tank.GetComponent(typeof(Tank))) as Tank;
            t.fire1(this.transform.position);
        }
        tanks = GameObject.FindGameObjectsWithTag("Firing2");
        foreach(GameObject tank in tanks)
        {
            Tank t = (tank.GetComponent(typeof(Tank))) as Tank;
            t.fire2(this.transform.position);
        }
    }
}
