using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta
{
    public enum TileType { CITY, PLAYER, ENEMY, ROCK, WATER, DEFAULT };
    public bool blocked;
    public bool onFire = false;
    public TileType type;
    
    public Meta(TileType type = TileType.DEFAULT, bool blocked = false, bool onFire = false)
    {
        this.blocked = blocked;
        this.onFire = onFire;
        this.type = type;
    }
}
 