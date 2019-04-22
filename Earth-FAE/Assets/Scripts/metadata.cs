using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class metadata
{
    private static Tank[] tanks = { new Tank(), new Tank(), new Tank() };
    private static float difficulty = 2.0f;
    public static Tank[] Tanks
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
