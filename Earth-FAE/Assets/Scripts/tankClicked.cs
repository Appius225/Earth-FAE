using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankClicked : MonoBehaviour
{
    private BoardManager grid;
    private int movement = 3;
    private int health = 3;
    private int maxActions = 3;
    public int actions;
    GameObject weap1;
    GameObject weap2;

    void Awake()
    {
        grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
    }
    void OnMouseDown()
    {
        
    }
}