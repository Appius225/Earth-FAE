using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    void OnMouseDown()
    {
        BoardManager grid = (BoardManager) FindObjectOfType(typeof(BoardManager));
        Vector3 def = new Vector3(0.0f, 0.0f, 0.0f);
        if (grid.spawnPos[0] == null || grid.spawnPos[0]==def)
        {
            grid.spawnPos[0] = this.transform.position;
        }
        else if (grid.spawnPos[1] == null || grid.spawnPos[1]==def)
        {
            grid.spawnPos[1] = this.transform.position;
        }
        else if (grid.spawnPos[2] == null || grid.spawnPos[2]==def)
        {
            grid.spawnPos[2] = this.transform.position;
        }
    }
}
