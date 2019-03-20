using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private BoardManager grid;
    private int movement = 3;
    private int health = 3;
    public int maxActions = 3;
    public int actions;
    public bool onFire = false;
    private Weapon weap1;
    private Weapon weap2;

    void Awake()
    {
        grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
    }
    void repair()
    {
        if (actions > 0)
        {
            actions--;
            health++;
            Vector3 pos = this.transform.position;
            int x = (int)Mathf.Floor(pos.x);
            int y = (int)Mathf.Round(pos.y / 0.75f);
            if (!(grid.objects[x, y].GetComponent(typeof(tileData)) as tileData ).onFire)
                onFire = false;
            else
                onFire = true;
        }
    }
    void fire1(Vector3 target)
    {
        if (actions > 1)
        {
            weap1.fire(this.transform.position,target);
            actions -= 2;
        }
    }
    void fire2(Vector3 target)
    {
        if (actions > 1)
        {
            weap2.fire(this.transform.position,target);
            actions -= 2;
        }
    }
    void OnMouseDown()
    {
        if (actions > 0)
        {

        }
    }
    public void damage(int d)
    {
        for (int i = 1; i <= d && health > 0; i++)
        {
            health--;
        }
        if (health == 0)
            this.die();
    }
    void die()
    {

    }
}