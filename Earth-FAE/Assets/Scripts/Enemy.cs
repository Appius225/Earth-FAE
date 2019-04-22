using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy
{
    tileData Tile { get; set; }
    bool OnFire { get; set; }
    bool isWaiting { get; set; }
    GameObject getGameObject();
    IEnumerator move();
    IEnumerator attack();
    void damage(int d);
    void die();
}
