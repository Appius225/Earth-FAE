using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class metadata
{
    private static GameObject[] tanks = new GameObject[3];
    private static float difficulty = 2.0f;
    private static int[] levelsDone = new int[10];
    public static int[] LevelsDone
    {
        get
        {
            return levelsDone;
        }
        set
        {
            levelsDone = value;
        }
    }
    public static GameObject[] Tanks
    {
        get
        {
            return tanks;
        }
        set
        {
            tanks = value;
        }
    }
    public static float Difficulty
    {
        get
        {
            return difficulty;
        }
        set
        {
            difficulty = value;
        }
    }
}
