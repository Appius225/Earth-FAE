using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int movement = 4;
    private int health = 3;
    public tileData tile;
    public bool onFire = false;

    void move()
    {

    }
    void attack()
    {

    }
    public void damage(int d)
    {
        for(int i = 1; i <= d && health > 0; i++)
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
