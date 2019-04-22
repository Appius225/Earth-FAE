using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class metadata
{
    private static GameObject[] tanks;
    private static float difficulty = 2.0f;
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
