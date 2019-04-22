using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Weapon
{
    IEnumerator fire(Vector3 start, Vector3 target);
    bool[,] tilesHittable(Vector3 start);
}
