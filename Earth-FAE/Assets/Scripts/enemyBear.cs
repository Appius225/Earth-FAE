using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBear : MonoBehaviour, Enemy
{
    private int movement = 3;
    private int health = 4;
    private tileData tile;
    public tileData Tile { get { return this.tile; } set { this.tile = value; } }
    private bool onFire;
    public bool OnFire { get { return this.onFire; } set { this.onFire = value; } }
    public enemyBear() { onFire = false; }
    private Vector3 tileToHitDiff;

    public void move()
    {
        Queue<GameObject>[] queues = new Queue<GameObject>[movement + 1];
        GameObject cur;
        tileData tile;
        bool targetFound;
        for(int i = 0; i < movement + 1; i++)
        {
            queues[i] = new Queue<>();
        }
        if (Mathf.Floor(this.transform.position.x + 1) < grid.getCols() && Mathf.Round(this.transform.position.y / 0.75f) < grid.getRows())
            queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x + 1), (int)Mathf.Round(this.transform.position.y / 0.75f)]);
        if (Mathf.Floor(this.transform.position.x - 1) >= 0 && Mathf.Round(this.transform.position.y / 0.75f) < grid.getRows())
            queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x - 1), (int)Mathf.Round(this.transform.position.y / 0.75f)]);
        if (Mathf.Floor(this.transform.position.x + 0.5f) < grid.getCols() && Mathf.Round(this.transform.position.y / 0.75f + 1) < grid.getRows())
            queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x + 0.5f), (int)Mathf.Round(this.transform.position.y / 0.75f + 1)]);
        if (Mathf.Floor(this.transform.position.x + 0.5f) < grid.getCols() && Mathf.Round(this.transform.position.y / 0.75f - 1) >= 0)
            queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x + 0.5f), (int)Mathf.Round(this.transform.position.y / 0.75f - 1)]);
        if (Mathf.Floor(this.transform.position.x - 0.5f) >= 0 && Mathf.Round(this.transform.position.y / 0.75f + 1) < grid.getRows())
            queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x - 0.5f), (int)Mathf.Round(this.transform.position.y / 0.75f + 1)]);
        if (Mathf.Floor(this.transform.position.x - 0.5f) >= 0 && Mathf.Round(this.transform.position.y / 0.75f - 1) >= 0)
            queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x - 0.5f), (int)Mathf.Round(this.transform.position.y / 0.75f - 1)]);
        for(int i = 0; i < movement && !targetFound; i++)
        {
            while(queues[i].Count != 0 && !targetFound)
            {
                cur = queues[i].Dequeue();
                tile = cur.GetComponenet(typeof(tileData)) as tileData;
                if (tile.city)
                {
                    targetFound = true;
                    vector3 = new Vector3(cur.transform.position);
                }
                else if((tile.tank==null) && !tile.blocked && tile.isNull)
                {
                    if (moveTiles[(int)Mathf.Floor(cur.transform.position.x), (int)Mathf.Round(cur.transform.position.y / 0.75f)] == null && cur.transform.position != grid.objects[(int)Mathf.Floor(this.transform.position.x), (int)Mathf.Round(this.transform.position.y / 0.75f)].transform.position)
                    {
                        moveTiles[(int)Mathf.Floor(cur.transform.position.x), (int)Mathf.Round(cur.transform.position.y / 0.75f)] = Instantiate(greenTile, new Vector3(cur.transform.position.x, cur.transform.position.y, -0.02f), Quaternion.identity) as GameObject;
                    }
                    GameObject temp;
                    bool found;
                    if (Mathf.Floor(cur.transform.position.x + 1) < grid.getCols() && Mathf.Round(cur.transform.position.y / 0.75f) < grid.getRows())
                    {
                        temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x + 1), (int)Mathf.Round(cur.transform.position.y / 0.75f)];
                        found = false;
                        for (int j = 0; j < movement && !found; j++)
                        {
                            if (queues[j].Contains(temp))
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            queues[i + 1].Enqueue(temp);
                        }
                    }
                    if (Mathf.Floor(cur.transform.position.x - 1) >= 0 && Mathf.Round(cur.transform.position.y / 0.75f) < grid.getRows())
                    {
                        temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x - 1), (int)Mathf.Round(cur.transform.position.y / 0.75f)];
                        found = false;
                        for (int j = 0; j < movement && !found; j++)
                        {
                            if (queues[j].Contains(temp))
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            queues[i + 1].Enqueue(temp);
                        }
                    }
                    if (Mathf.Floor(cur.transform.position.x + 0.5f) < grid.getCols() && Mathf.Round(cur.transform.position.y / 0.75f + 1) < grid.getRows())
                    {
                        temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x + 0.5f), (int)Mathf.Round(cur.transform.position.y / 0.75f + 1)];
                        found = false;
                        for (int j = 0; j < movement && !found; j++)
                        {
                            if (queues[j].Contains(temp))
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            queues[i + 1].Enqueue(temp);
                        }
                    }
                    if (Mathf.Floor(cur.transform.position.x + 0.5f) < grid.getCols() && Mathf.Round(cur.transform.position.y / 0.75f - 1) >= 0)
                    {
                        temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x + 0.5f), (int)Mathf.Round(cur.transform.position.y / 0.75f - 1)];
                        found = false;
                        for (int j = 0; j < movement && !found; j++)
                        {
                            if (queues[j].Contains(temp))
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            queues[i + 1].Enqueue(temp);
                        }
                    }
                    if (Mathf.Floor(cur.transform.position.x - 0.5f) >= 0 && Mathf.Round(cur.transform.position.y / 0.75f + 1) < grid.getRows())
                    {
                        temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x - 0.5f), (int)Mathf.Round(cur.transform.position.y / 0.75f + 1)];
                        found = false;
                        for (int j = 0; j < movement && !found; j++)
                        {
                            if (queues[j].Contains(temp))
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            queues[i + 1].Enqueue(temp);
                        }
                    }
                    if (Mathf.Floor(cur.transform.position.x - 0.5f) >= 0 && Mathf.Round(cur.transform.position.y / 0.75f - 1) >= 0)
                    {
                        temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x - 0.5f), (int)Mathf.Round(cur.transform.position.y / 0.75f - 1)];
                        found = false;
                        for (int j = 0; j < movement && !found; j++)
                        {
                            if (queues[j].Contains(temp))
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            queues[i + 1].Enqueue(temp);
                        }
                    }
                }
                //inside loop
            }
        }
        while (queues[movement].Count != 0 && !targetFound)
        {
            cur = queues[movement].Dequeue();
            tile = cur.GetComponenet(typeof(tileData)) as tileData;
            if (tile.city)
            {
                targetFound = true;
                vector3 = new Vector3(cur.transform.position);
            }
        }
        if (!targetFound)
        {
            if (Mathf.Floor(this.transform.position.x + 1) < grid.getCols() && Mathf.Round(this.transform.position.y / 0.75f) < grid.getRows())
                queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x + 1), (int)Mathf.Round(this.transform.position.y / 0.75f)]);
            if (Mathf.Floor(this.transform.position.x - 1) >= 0 && Mathf.Round(this.transform.position.y / 0.75f) < grid.getRows())
                queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x - 1), (int)Mathf.Round(this.transform.position.y / 0.75f)]);
            if (Mathf.Floor(this.transform.position.x + 0.5f) < grid.getCols() && Mathf.Round(this.transform.position.y / 0.75f + 1) < grid.getRows())
                queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x + 0.5f), (int)Mathf.Round(this.transform.position.y / 0.75f + 1)]);
            if (Mathf.Floor(this.transform.position.x + 0.5f) < grid.getCols() && Mathf.Round(this.transform.position.y / 0.75f - 1) >= 0)
                queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x + 0.5f), (int)Mathf.Round(this.transform.position.y / 0.75f - 1)]);
            if (Mathf.Floor(this.transform.position.x - 0.5f) >= 0 && Mathf.Round(this.transform.position.y / 0.75f + 1) < grid.getRows())
                queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x - 0.5f), (int)Mathf.Round(this.transform.position.y / 0.75f + 1)]);
            if (Mathf.Floor(this.transform.position.x - 0.5f) >= 0 && Mathf.Round(this.transform.position.y / 0.75f - 1) >= 0)
                queues[0].Enqueue(grid.objects[(int)Mathf.Floor(this.transform.position.x - 0.5f), (int)Mathf.Round(this.transform.position.y / 0.75f - 1)]);
            for (int i = 0; i < movement && !targetFound; i++)
            {
                while (queues[i].Count != 0 && !targetFound)
                {
                    cur = queues[i].Dequeue();
                    tile = cur.GetComponenet(typeof(tileData)) as tileData;
                    if (tile.tank != null)
                    {
                        targetFound = true;
                        vector3 = new Vector3(cur.transform.position);
                    }
                    else if ((tile.tank == null) && !tile.blocked && tile.isNull)
                    {
                        if (moveTiles[(int)Mathf.Floor(cur.transform.position.x), (int)Mathf.Round(cur.transform.position.y / 0.75f)] == null && cur.transform.position != grid.objects[(int)Mathf.Floor(this.transform.position.x), (int)Mathf.Round(this.transform.position.y / 0.75f)].transform.position)
                        {
                            moveTiles[(int)Mathf.Floor(cur.transform.position.x), (int)Mathf.Round(cur.transform.position.y / 0.75f)] = Instantiate(greenTile, new Vector3(cur.transform.position.x, cur.transform.position.y, -0.02f), Quaternion.identity) as GameObject;
                        }
                        GameObject temp;
                        bool found;
                        if (Mathf.Floor(cur.transform.position.x + 1) < grid.getCols() && Mathf.Round(cur.transform.position.y / 0.75f) < grid.getRows())
                        {
                            temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x + 1), (int)Mathf.Round(cur.transform.position.y / 0.75f)];
                            found = false;
                            for (int j = 0; j < movement && !found; j++)
                            {
                                if (queues[j].Contains(temp))
                                {
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                queues[i + 1].Enqueue(temp);
                            }
                        }
                        if (Mathf.Floor(cur.transform.position.x - 1) >= 0 && Mathf.Round(cur.transform.position.y / 0.75f) < grid.getRows())
                        {
                            temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x - 1), (int)Mathf.Round(cur.transform.position.y / 0.75f)];
                            found = false;
                            for (int j = 0; j < movement && !found; j++)
                            {
                                if (queues[j].Contains(temp))
                                {
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                queues[i + 1].Enqueue(temp);
                            }
                        }
                        if (Mathf.Floor(cur.transform.position.x + 0.5f) < grid.getCols() && Mathf.Round(cur.transform.position.y / 0.75f + 1) < grid.getRows())
                        {
                            temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x + 0.5f), (int)Mathf.Round(cur.transform.position.y / 0.75f + 1)];
                            found = false;
                            for (int j = 0; j < movement && !found; j++)
                            {
                                if (queues[j].Contains(temp))
                                {
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                queues[i + 1].Enqueue(temp);
                            }
                        }
                        if (Mathf.Floor(cur.transform.position.x + 0.5f) < grid.getCols() && Mathf.Round(cur.transform.position.y / 0.75f - 1) >= 0)
                        {
                            temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x + 0.5f), (int)Mathf.Round(cur.transform.position.y / 0.75f - 1)];
                            found = false;
                            for (int j = 0; j < movement && !found; j++)
                            {
                                if (queues[j].Contains(temp))
                                {
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                queues[i + 1].Enqueue(temp);
                            }
                        }
                        if (Mathf.Floor(cur.transform.position.x - 0.5f) >= 0 && Mathf.Round(cur.transform.position.y / 0.75f + 1) < grid.getRows())
                        {
                            temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x - 0.5f), (int)Mathf.Round(cur.transform.position.y / 0.75f + 1)];
                            found = false;
                            for (int j = 0; j < movement && !found; j++)
                            {
                                if (queues[j].Contains(temp))
                                {
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                queues[i + 1].Enqueue(temp);
                            }
                        }
                        if (Mathf.Floor(cur.transform.position.x - 0.5f) >= 0 && Mathf.Round(cur.transform.position.y / 0.75f - 1) >= 0)
                        {
                            temp = grid.objects[(int)Mathf.Floor(cur.transform.position.x - 0.5f), (int)Mathf.Round(cur.transform.position.y / 0.75f - 1)];
                            found = false;
                            for (int j = 0; j < movement && !found; j++)
                            {
                                if (queues[j].Contains(temp))
                                {
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                queues[i + 1].Enqueue(temp);
                            }
                        }
                    }
                }
            }
            while(queues[movement].Count != 0 && !targetFound)
            {
                cur = queues[movement].Dequeue();
                tile = cur.GetComponenet(typeof(tileData)) as tileData;
                if (tile.city)
                {
                    targetFound = true;
                    vector3 = new Vector3(cur.transform.position);
                }
            }
        }
        //Final condition, move towards center x on a city row
        //Pathfind back to this.transform.position and display target
    }
    public void attack()
    {

    }
    public void damage(int d)
    {
        for(int i = 0; i < d && health > 0; i++)
        {
            health--;
        }
        if (health == 0)
        {
            this.die();
        }
    }
    public void die()
    {
        //animation
        Destroy(this);
    }
}
