using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyBear : MonoBehaviour, Enemy
{
    private int movement = 3;
    private int health = 3;
    private int maxHealth = 3;
    private int dmg = 2;
    private tileData tile;
    public tileData Tile { get { return this.tile; } set { this.tile = value; } }
    private bool onFire;
    public bool OnFire { get { return this.onFire; } set { this.onFire = value; } }
    public enemyBear() { onFire = false; }
    private Vector3 tileToHitDiff;
    private bool isRunning;
    private GameObject tileToHit;
    public GameObject hitTile;
    private bool waiting;
    public bool isWaiting { get { return this.waiting; } set { this.waiting = value; } }
    public Image healthBar;

    public GameObject getGameObject()
    {
        return gameObject;
    }
    public IEnumerator move()
    {
        BoardManager grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
        Queue<GameObject>[] queues = new Queue<GameObject>[movement + 1];
        GameObject cur;
        tileData tile;
        bool targetFound = false;
        Vector3 originalPosition = this.transform.position;
        Vector3 finalPosition = new Vector3(0.0f,0.0f,0.0f);
        for(int i = 0; i < movement + 1; i++)
        {
            queues[i] = new Queue<GameObject>();
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
                tile = cur.GetComponent(typeof(tileData)) as tileData;
                if (tile.city)
                {
                    targetFound = true;
                    tileToHitDiff = new Vector3(cur.transform.position.x,cur.transform.position.y,cur.transform.position.z);
                }
                else if((tile.tank==null) && !tile.blocked && !tile.isNull && (tile.enemy == null) && !tile.isWater)
                {
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
            tile = cur.GetComponent(typeof(tileData)) as tileData;
            if (tile.city)
            {
                targetFound = true;
                tileToHitDiff = new Vector3(cur.transform.position.x,cur.transform.position.y,cur.transform.position.z);
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
                    tile = cur.GetComponent(typeof(tileData)) as tileData;
                    if (tile.tank != null)
                    {
                        targetFound = true;
                        tileToHitDiff = new Vector3(cur.transform.position.x,cur.transform.position.y,cur.transform.position.z);
                    }
                    else if ((tile.tank == null) && !tile.blocked && !tile.isNull && (tile.enemy == null) && !tile.isWater)
                    {
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
                tile = cur.GetComponent(typeof(tileData)) as tileData;
                if (tile.tank != null)
                {
                    targetFound = true;
                    tileToHitDiff = new Vector3(cur.transform.position.x,cur.transform.position.y,cur.transform.position.z);
                }
            }
        }
        if (!targetFound)
        {
            int posY = (int)Mathf.Round(this.transform.position.y / 0.75f);
            float x = 0.0f;
            int y = 0;
            for(int i = 0; i < grid.getRows(); i++)
            {
                if (grid.cityRows[i])
                {
                    if (Mathf.Abs(posY - y) > Mathf.Abs(posY - i))
                    {
                        y = i;
                    }
                }
            }
            if (y % 2 == 1)
            {
                x = x + 0.5f;
            }
            float dirX;
            if((int)Mathf.Floor(this.transform.position.x) > (int)Mathf.Floor(x))
            {
                dirX = -1.0f;
            }
            else
            {
                dirX = 1.0f;
            }
            for(int i = 0; i < movement; i++)
            {
                float my = 0.0f;
                float mx = 0.0f;
                Vector3 pos = this.transform.position;
                Debug.Log(y);
                if ((int)Mathf.Round(pos.y / 0.75f) > y)
                {
                    my = -0.75f;
                    if (dirX > 0.0f)
                    {
                        mx = 0.5f;
                    }
                    else
                    {
                        mx = -0.5f;
                    }
                }
                else if ((int)Mathf.Round(pos.y / 0.75f) < y)
                {
                    my = 0.75f;
                    if (dirX > 0.0f)
                    {
                        mx = 0.5f;
                    }
                    else
                    {
                        mx = -0.5f;
                    }
                }
                else
                {
                    if (dirX > 0.0f)
                    {
                        mx = 1.0f;
                    }
                    else
                    {
                        mx = -1.0f;
                    }
                }
                Vector3 moveTo = new Vector3(this.transform.position.x + mx, this.transform.position.y + my, this.transform.position.z);
                GameObject temp = grid.objects[(int)Mathf.Floor(moveTo.x), (int)Mathf.Round(moveTo.y / 0.75f)];
                tile = temp.GetComponent(typeof(tileData)) as tileData;
                int iter = 0;
                while((tile.enemy!=null || tile.blocked || tile.isNull || tile.city || tile.tank != null || tile.isWater) && iter < 6)
                {
                    if (mx > 0.0f)
                    {
                        if (my > 0.0f)
                        {
                            mx = 1.0f;
                            my = 0.0f;
                            moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                        }
                        else if ((int)my == 0)
                        {
                            mx = 0.5f;
                            my = -0.75f;
                            moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                        }
                        else
                        {
                            mx = -0.5f;
                            my = 0.75f;
                            moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                        }
                    }
                    else
                    {
                        if(my > 0.0f)
                        {
                            mx = -1.0f;
                            my = 0.0f;
                            moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                        }
                        else if((int)my == 0)
                        {
                            mx = -0.5f;
                            my = -0.75f;
                            moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                        }
                        else
                        {
                            mx = 0.5f;
                            my = 0.75f;
                            moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                        }
                    }
                    iter++;
                    if((int)Mathf.Floor(moveTo.x)>=0 && (int)Mathf.Floor(moveTo.x)<grid.getCols() && (int)Mathf.Round(moveTo.y/0.75f)>=0 && (int)Mathf.Round(moveTo.y / 0.75f) < grid.getRows())
                    {
                        temp = grid.objects[(int)Mathf.Floor(moveTo.x), (int)Mathf.Round(moveTo.y / 0.75f)];
                        tile = temp.GetComponent(typeof(tileData)) as tileData;
                    }
                }
                if(iter == 6)
                {
                    moveTo = new Vector3(pos.x, pos.y, pos.z);
                }
                isRunning = true;
                StartCoroutine(moveTile(moveTo));
                while (isRunning)
                {
                    Debug.Log(isRunning);
                    yield return new WaitForEndOfFrame();
                }
                finalPosition = moveTo;
            }
        }
        else
        {
            Vector3 moveTo;
            cur = grid.objects[(int)Mathf.Floor(this.transform.position.x), (int)Mathf.Round(this.transform.position.y / 0.75f)];
            bool done = false;
            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            float mx;
            float my;
            while (!done)
            {

                if ((int)Mathf.Round(tileToHitDiff.y / 0.75f) > (int)Mathf.Round(this.transform.position.y / 0.75f))
                {
                    if ((int)Mathf.Floor(this.transform.position.x) < (int)Mathf.Floor(tileToHitDiff.x))
                    {
                        moveTo = new Vector3(pos.x + 0.5f, pos.y + 0.75f, pos.z);
                        mx = 0.5f;
                        my = 0.75f;
                    }
                    else
                    {
                        moveTo = new Vector3(pos.x - 0.5f, pos.y + 0.75f, pos.z);
                        mx = -0.5f;
                        my = 0.75f;
                    }
                }
                else if ((int)Mathf.Round(tileToHitDiff.y / 0.75f) < (int)Mathf.Round(this.transform.position.y / 0.75f))
                {
                    if ((int)Mathf.Floor(this.transform.position.x) < (int)Mathf.Floor(tileToHitDiff.x))
                    {
                        moveTo = new Vector3(pos.x + 0.5f, pos.y - 0.75f, pos.z);
                        mx = 0.5f;
                        my = -0.75f;
                    }
                    else
                    {
                        moveTo = new Vector3(pos.x - 0.5f, pos.y - 0.75f, pos.z);
                        mx = -0.5f;
                        my = -0.75f;
                    }
                }
                else
                {
                    if ((int)Mathf.Floor(tileToHitDiff.x) < (int)Mathf.Floor(this.transform.position.x))
                    {
                        moveTo = new Vector3(pos.x - 1.0f, pos.y, pos.z);
                        mx = -1.0f;
                        my = 0.0f;
                    }
                    else
                    {
                        moveTo = new Vector3(pos.x + 1.0f, pos.y, pos.z);
                        mx = 1.0f;
                        my = 0.0f;
                    }
                }
                if((int)Mathf.Floor(moveTo.x)==(int)Mathf.Floor(tileToHitDiff.x) && (int)Mathf.Round(moveTo.y / 0.75f) == (int)Mathf.Round(tileToHitDiff.y / 0.75f))
                {
                    done = true;
                    finalPosition = this.transform.position;
                }
                if (!done)
                {
                    GameObject temp = grid.objects[(int)Mathf.Floor(moveTo.x), (int)Mathf.Round(moveTo.y / 0.75f)];
                    tile = temp.GetComponent(typeof(tileData)) as tileData;
                    while (tile.blocked || tile.isNull || tile.city || tile.tank != null || tile.enemy != null ||  tile.isWater)
                    {
                        if (mx > 0.0f)
                        {
                            if (my > 0.0f)
                            {
                                mx = 1.0f;
                                my = 0.0f;
                                moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                            }
                            else if ((int)my == 0)
                            {
                                mx = 0.5f;
                                my = -0.75f;
                                moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                            }
                            else
                            {
                                mx = -0.5f;
                                my = 0.75f;
                                moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                            }
                        }
                        else
                        {
                            if (my > 0.0f)
                            {
                                mx = -1.0f;
                                my = 0.0f;
                                moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                            }
                            else if ((int)my == 0)
                            {
                                mx = -0.5f;
                                my = -0.75f;
                                moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                            }
                            else
                            {
                                mx = 0.5f;
                                my = 0.75f;
                                moveTo = new Vector3(pos.x + mx, pos.y + my, pos.z);
                            }
                        }
                        temp = grid.objects[(int)Mathf.Floor(moveTo.x), (int)Mathf.Round(moveTo.y / 0.75f)];
                        tile = temp.GetComponent(typeof(tileData)) as tileData;
                    }
                    isRunning = true;
                    StartCoroutine(moveTile(moveTo));
                    while (isRunning)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                    finalPosition = moveTo;
                    pos = this.transform.position;
                }
            }
            tileToHitDiff.x = tileToHitDiff.x - this.transform.position.x;
            tileToHitDiff.y = tileToHitDiff.y - this.transform.position.y;
            tileToHit = Instantiate(hitTile, new Vector3(this.transform.position.x + tileToHitDiff.x, this.transform.position.y + tileToHitDiff.y, this.transform.position.z),Quaternion.identity);
        }
        cur = grid.objects[(int)Mathf.Floor(originalPosition.x), (int)Mathf.Round(originalPosition.y / 0.75f)];
        tile = cur.GetComponent(typeof(tileData)) as tileData;
        tile.enemy = null;
        cur = grid.objects[(int)Mathf.Floor(finalPosition.x), (int)Mathf.Round(finalPosition.y / 0.75f)];
        tile = cur.GetComponent(typeof(tileData)) as tileData;
        tile.enemy = this;
        this.tile = tile;
        waiting = false;
    }
    IEnumerator moveTile(Vector3 pos)
    {
        this.transform.position = pos;
        yield return new WaitForSeconds(0.25f);
        isRunning = false;
    }
    public IEnumerator attack()
    {
        BoardManager grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
        Vector3 def = new Vector3(0, 0, 0);
        if(tileToHitDiff != def)
        {
            Vector3 target = new Vector3(this.transform.position.x + tileToHitDiff.x, this.transform.position.y + tileToHitDiff.y, 0.0f);
            if (target.x > 0 && target.x < grid.getCols() && target.y > 0 && target.y < grid.getRows())
            {
                GameObject hit = grid.objects[(int)Mathf.Floor(target.x), (int)Mathf.Round(target.y / 0.75f)];
                tileData tile = hit.GetComponent(typeof(tileData)) as tileData;
                if (tile.city)
                {
                    grid.damageCity(dmg);
                }
                else if (tile.enemy != null)
                {
                    tile.enemy.damage(dmg);
                }
                else if (tile.tank != null)
                {
                    tile.tank.damage(dmg);
                }
            }
            Destroy(tileToHit);
            yield return new WaitForSeconds(0.5f);
        }
        waiting = false;
    }
    public void damage(int d)
    {
        for(int i = 0; i < d && health > 0; i++)
        {
            health--;
            healthBar.fillAmount = (float) health / maxHealth;
        }
        if (health == 0)
        {
            this.die();
        }
    }
    public void die()
    {
        //animation
        BoardManager grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
        ((grid.objects[(int)Mathf.Floor(this.transform.position.x), (int)Mathf.Round(this.transform.position.y / 0.75f)]).GetComponent(typeof(tileData)) as tileData).enemy = null;
        Destroy(gameObject);
    }
}
