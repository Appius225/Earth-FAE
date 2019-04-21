using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalShot : MonoBehaviour , Weapon
{
    public int damage = 1;
    public void fire(Vector3 pos, Vector3 target)
    {
        BoardManager grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
        tileData tile;
        if (target.y == pos.y)
        {
            int y = (int)Mathf.Round(pos.y / 0.75f);
            if (target.x > pos.x)
            {
                int i;
                for(i = (int)Mathf.Floor(pos.x + 1.0f); i < grid.getCols(); i++)
                {
                    tile = (grid.objects[i,y].GetComponent(typeof(tileData)) as tileData);
                    if (tile.city)
                    {
                        //animation
                        grid.cityHealth -= damage;
                        if (grid.cityHealth <= 0)
                        {
                            //lose game
                        }
                        i = 100;
                    }
                    else if (tile.blocked)
                    {
                        i = 100;
                        //run animation
                    }
                    else if (tile.enemy != null)
                    {
                        //animation
                        tile.enemy.damage(damage);
                        i = 100;
                    }
                    else if(tile.tank != null)
                    {
                        //animation
                        tile.tank.damage(damage);
                        i = 100;
                    }
                }
                if (i != 100)
                {
                    //miss animation
                }
            }
            else
            {
                int i;
                for (i = (int)Mathf.Floor(pos.x - 1.0f); i >= 0; i--)
                {
                    tile = (grid.objects[i, y].GetComponent(typeof(tileData)) as tileData);
                    if (tile.city)
                    {
                        //animation
                        grid.cityHealth -= damage;
                        if (grid.cityHealth <= 0)
                        {
                            //lose game
                        }
                        i = -100;
                    }
                    else if (tile.blocked)
                    {
                        i = -100;
                        //run animation
                    }
                    else if (tile.enemy != null)
                    {
                        //animation
                        tile.enemy.damage(damage);
                        i = -100;
                    }
                    else if (tile.tank != null)
                    {
                        //animation
                        tile.tank.damage(damage);
                        i = -100;
                    }
                }
                if (i != -100)
                {
                    //miss animation
                }
            }
        }
        else if(target.x>pos.x && target.y > pos.y)
        {
            int i;
            for(i = 1; Mathf.Floor(pos.x + 0.5f * i) < grid.getCols() && Mathf.Round((pos.y + i * 0.75f) / 0.75f) < grid.getRows(); i++)
            {
                tile = (grid.objects[(int)Mathf.Floor(pos.x + 0.5f * i), (int)Mathf.Round((pos.y + i * 0.75f) / 0.75f)].GetComponent(typeof(tileData)) as tileData);
                if (tile.city)
                {
                    //animation
                    grid.cityHealth -= damage;
                    if (grid.cityHealth <= 0)
                    {
                        //lose game
                    }
                    i = 100;
                }
                else if (tile.blocked)
                {
                    i = 100;
                    //run animation
                }
                else if (tile.enemy != null)
                {
                    //animation
                    tile.enemy.damage(damage);
                    i = 100;
                }
                else if (tile.tank != null)
                {
                    //animation
                    tile.tank.damage(damage);
                    i = 100;
                }
            }
            if (i != 100)
            {
                //miss animation
            }
        }
        else if(target.x>pos.x && target.y < pos.y)
        {
            int i;
            for(i = 1;Mathf.Floor(pos.x+0.5f*i)<grid.getCols() && Mathf.Round((pos.y - i * 0.75f) / 0.75f) >= 0; i++)
            {
                tile = (grid.objects[(int)Mathf.Floor(pos.x + 0.5f * i), (int)Mathf.Round((pos.y + i * 0.75f) / 0.75f)].GetComponent(typeof(tileData)) as tileData);
                if (tile.city)
                {
                    //animation
                    grid.cityHealth -= damage;
                    if (grid.cityHealth <= 0)
                    {
                        //lose game
                    }
                    i = 100;
                }
                else if (tile.blocked)
                {
                    i = 100;
                    //run animation
                }
                else if (tile.enemy != null)
                {
                    //animation
                    tile.enemy.damage(damage);
                    i = 100;
                }
                else if (tile.tank != null)
                {
                    //animation
                    tile.tank.damage(damage);
                    i = 100;
                }
            }
            if (i != 100)
            {
                //miss animation
            }
        }
        else if (target.x < pos.x && target.y > pos.y)
        {
            int i;
            for (i = 1; Mathf.Floor(pos.x - 0.5f * i) >= 0 && Mathf.Round((pos.y + i * 0.75f) / 0.75f) < grid.getRows();i++)
            {
                tile = (grid.objects[(int)Mathf.Floor(pos.x + 0.5f * i), (int)Mathf.Round((pos.y + i * 0.75f) / 0.75f)].GetComponent(typeof(tileData)) as tileData);
                if (tile.city)
                {
                    //animation
                    grid.cityHealth -= damage;
                    if (grid.cityHealth <= 0)
                    {
                        //lose game
                    }
                    i = 100;
                }
                else if (tile.blocked)
                {
                    i = 100;
                    //run animation
                }
                else if (tile.enemy != null)
                {
                    //animation
                    tile.enemy.damage(damage);
                    i = 100;
                }
                else if (tile.tank != null)
                {
                    //animation
                    tile.tank.damage(damage);
                    i = 100;
                }
            }
            if (i != 100)
            {
                //miss animation
            }
        }
        else
        {
            int i;
            for (i = 1; Mathf.Floor(pos.x - 0.5f * i) >= 0 && Mathf.Round((pos.y - i * 0.75f) / 0.75f) >= 0; i++)
            {
                tile = (grid.objects[(int)Mathf.Floor(pos.x + 0.5f * i), (int)Mathf.Round((pos.y + i * 0.75f) / 0.75f)].GetComponent(typeof(tileData)) as tileData);
                if (tile.city)
                {
                    //animation
                    grid.cityHealth -= damage;
                    if (grid.cityHealth <= 0)
                    {
                        //lose game
                    }
                    i = 100;
                }
                else if (tile.blocked)
                {
                    i = 100;
                    //run animation
                }
                else if (tile.enemy != null)
                {
                    //animation
                    tile.enemy.damage(damage);
                    i = 100;
                }
                else if (tile.tank != null)
                {
                    //animation
                    tile.tank.damage(damage);
                    i = 100;
                }
            }
            if (i != 100)
            {
                //miss animation
            }
        }
    }
    public bool[,] tilesHittable(Vector3 position)
    {
        BoardManager grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
        bool[,] hitTiles = new bool[grid.getRows(), grid.getCols()];
        int x = (int)Mathf.Floor(position.x);
        int y = (int)Mathf.Round(position.y / 0.75f);
        for(int i = 0; i < hitTiles.GetLength(0); i++)
        {
            for(int j = 0; j < hitTiles.GetLength(1); j++)
            {
                if ((j-y)==0)
                {
                    if(i!=x)
                    {
                        hitTiles[i, j] = true;
                    }
                }
                else if ((j%2)==1)
                {
                    if (Mathf.Round(((j * 0.75f) - position.y) / 0.75f) == Mathf.Round(((i + 0.5f) - position.x)/0.5f))
                    {
                        hitTiles[i, j] = true;
                    }
                    else if(Mathf.Round(((j * 0.75f) - position.y) / 0.75f) == - Mathf.Round(((i + 0.5f) - position.x) / 0.5f))
                    {
                        hitTiles[i, j] = true;
                    }
                    else
                    {
                        hitTiles[i, j] = false;
                    }
                }
                else 
                {
                    if(Mathf.Round(((j*0.75f)-position.y)/0.75f)==Mathf.Round(((i - position.x)/0.5f)))
                    {
                        hitTiles[i, j] = true;
                    }
                    else if (Mathf.Round(((j * 0.75f) - position.y) / 0.75f) == - Mathf.Round(((i - position.x)/0.5f)))
                    {
                        hitTiles[i, j] = true;
                    }
                    else
                    {
                        hitTiles[i, j] = false;
                    }
                }
            }
        }
        return hitTiles;
    }
}
