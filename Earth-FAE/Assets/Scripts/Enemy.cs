﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy
{
    tileData Tile { get; set; }
    bool OnFire { get; set; }

    void move();
    void attack();
    void damage(int d);
    void die();
}