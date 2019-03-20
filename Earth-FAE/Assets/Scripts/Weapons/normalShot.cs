﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalShot : MonoBehaviour , Weapon
{
    public int damage = 1;
    public void fire(Vector3 pos, Vector3 target)
    {
        BoardManager grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
        int px = (int)Mathf.Floor(pos.x);
        int py = (int)Mathf.Round(pos.y / 0.75f);
        int tx = (int)Mathf.Floor(target.x);
        int ty = (int)Mathf.Round(target.y / 0.75f);
        tileData tile;
        int i;
        if ((px - tx) == 0)
        {
            if (ty > py)
            {
                for(i = py + 1; i < grid.getRows(); i++)
                {
                    tile = (grid.objects[tx, i].GetComponent(typeof(tileData)) as tileData);
                    if (tile.city)
                    {
                        grid.cityHealth -= damage;
                        if (grid.cityHealth <= 0)
                        {
                            //something something lose game
                        }
                        //something something animation
                        i = 100;
                    }
                    else if (tile.blocked) { i = 100; }
                    else if (tile.enemy != null)
                    {
                        tile.enemy.damage(damage);
                        //something something animation
                        i = 100;
                    }
                    else if (tile.tank != null)
                    {
                        tile.tank.damage(damage);
                        //something something animation
                        i = 100;
                    }
                }
                if (i != 100)
                {
                    //something something miss animation
                }
            }
            else
            {
                for(i = py - 1; i >= 0; i++)
                {
                    tile = (grid.objects[tx, i].GetComponent(typeof(tileData)) as tileData);
                    if (tile.city)
                    {
                        grid.cityHealth -= damage;
                        if (grid.cityHealth <= 0)
                        {
                            //something something lose game
                        }
                        //something something animation
                        i = -100;
                    }
                    else if (tile.blocked) { i = -100; }
                    else if(tile.enemy != null)
                    {
                        tile.enemy.damage(damage);
                        //something something animation
                        i = -100;
                    }
                    else if(tile.tank != null)
                    {
                        tile.tank.damage(damage);
                        //something something animation
                        i = -100;
                    }
                }
                if (i != -100)
                {
                    //something something miss animation
                }
            }
        }
        else if ((py - ty) == 0)
        {
            if (tx > px)
            {
                for(i = px + 1; i < grid.getCol(); i++)
                {
                    tile = (grid.objects[i, ty].GetComponent(typeof(tileData)) as tileData);
                    if (tile.city)
                    {
                        grid.cityHealth -= damage;
                        if (grid.cityHealth <= 0)
                        {
                            //something something lose game
                        }
                        //something something animation
                        i = 100;
                    }
                    else if (tile.blocked) { i = 100; }
                    else if (tile.enemy != null)
                    {
                        tile.enemy.damage(damage);
                        //something something animation
                        i = 100;
                    }
                    else if (tile.tank != null)
                    {
                        tile.tank.damage(damage);
                        //something something animation
                        i = 100;
                    }
                }
                if (i != 100)
                {
                    //something something miss animation
                }
            }
            else
            {
                for (i = px - 1; i >= 0; i++)
                {
                    tile = (grid.objects[i, ty].GetComponent(typeof(tileData)) as tileData);
                    if (tile.city)
                    {
                        grid.cityHealth -= damage;
                        if (grid.cityHealth <= 0)
                        {
                            //something something lose game
                        }
                        //something something animation
                        i = -100;
                    }
                    else if (tile.blocked) { i = -100; }
                    else if (tile.enemy != null)
                    {
                        tile.enemy.damage(damage);
                        //something something animation
                        i = -100;
                    }
                    else if (tile.tank != null)
                    {
                        tile.tank.damage(damage);
                        //something something animation
                        i = -100;
                    }
                }
                if (i != -100)
                {
                    //something something miss animation
                }
            }
        }
        else
        {
            if (tx > px)
            {
                i = px + 1;
                int j = py - 1;
                while (i < grid.getCol() && j >= 0)
                {
                    tile = (grid.objects[i, j].GetComponent(typeof(tileData)) as tileData);
                    if (tile.city)
                    {
                        grid.cityHealth -= damage;
                        if (grid.cityHealth <= 0)
                        {
                            //something something lose game
                        }
                        //something something animation
                        i = 100;
                    }
                    else if (tile.blocked) { i = 100; }
                    else if (tile.enemy != null)
                    {
                        tile.enemy.damage(damage);
                        //something something animation
                        i = 100;
                    }
                    else if (tile.tank != null)
                    {
                        tile.tank.damage(damage);
                        //something something animation
                        i = 100;
                    }
                    i++;
                    j--;
                }
                if (i != 100)
                {
                    //something something miss animation
                }
            }
            else
            {
                i = px - 1;
                int j = py + 1;
                while (i >= 0 && j < grid.getRows())
                {
                    tile = (grid.objects[i, ty].GetComponent(typeof(tileData)) as tileData);
                    if (tile.city)
                    {
                        grid.cityHealth -= damage;
                        if (grid.cityHealth <= 0)
                        {
                            //something something lose game
                        }
                        //something something animation
                        i = -100;
                    }
                    else if (tile.blocked) { i = -100; }
                    else if (tile.enemy != null)
                    {
                        tile.enemy.damage(damage);
                        //something something animation
                        i = -100;
                    }
                    else if (tile.tank != null)
                    {
                        tile.tank.damage(damage);
                        //something something animation
                        i = -100;
                    }
                    i--;
                    j++;
                }
                if (i != -100)
                {
                    //something something miss animation
                }
            }
        }
    }
}
