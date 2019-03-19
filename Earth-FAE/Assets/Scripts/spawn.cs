using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    void OnMouseDown()
    {
        BoardManager grid = (BoardManager) FindObjectOfType(typeof(BoardManager));
        grid.spawned = true;
        Destroy(gameObject);
    }
}
