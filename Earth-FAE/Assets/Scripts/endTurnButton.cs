using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endTurnButton : MonoBehaviour
{
    private BoardManager grid;
    void Awake()
    {
        grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
    }
    void OnMouseDown()
    {
        grid.turnEnded = true;
    }
}
