using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBear : MonoBehaviour, Enemy
{
    private int movement = 3;
    private int health = 4;
    private tileData tile;
    public tileData Tile { get { return this.tile; } set { this.tile = value; } }
    private bool onFire;
    public bool OnFire { get { return this.onFire; } set { this.onFire = value; } }
    public enemyBear() { onFire = false; }

    public void move()
    {

    }
    public void attack()
    {

    }
    public void damage(int d)
    {
        for(int i = 0; i < d && health > 0; i++)
        {
            health--;
        }
        if (health == 0)
        {
            this.die();
        }
    }
    public void die()
    {
        //animation
        Destroy(this);
    }
}
