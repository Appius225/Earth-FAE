using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private BoardManager grid;
    private int movement = 4;
    public int maxHealth = 3;
    public int health = 3;
    public int maxActions = 3;
    public int actions;
    public bool OnFire = false;
    private Weapon weap1;
    private Weapon weap2;
    public GameObject greenTile;
    private GameObject[,] moveTiles;
    private bool moveTilesDisp = false;
    public GameObject redTile;
    private GameObject[,] hittableTiles;
    private bool hitTilesDisp = false;
    public Vector3[] prevMove;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (hitTilesDisp)
            {
                removeHitTiles();
            }
            else if (moveTilesDisp)
            {
                hideMoveableTiles();
            }
        }
    }
    void Awake()
    {
        grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
        hittableTiles = new GameObject[grid.getCols(), grid.getRows()];
        moveTiles = new GameObject[grid.getRows(), grid.getCols()];
        actions = maxActions;
        weap1 = new normalShot();
        prevMove = new Vector3[maxActions];
    }
    void undo()
    {
        Vector3 def = new Vector3(0, 0, 0);
        int i;
        bool done = false;
        for(i = 0; i < maxActions && !done; i++)
        {
            if (prevMove[i] == def)
            {
                i--;
                done = true;
            }
        }
        if (!done)
        {
            i = maxActions - 1;
        }
        if (i >= 0)
        {
            if((grid.objects[(int)Mathf.Floor(prevMove[i].x), (int)Mathf.Round(prevMove[i].y/0.75f)].GetComponent(typeof(tileData))as tileData).tank != null)
            {
                Vector3 pos = this.transform.position;
                tileData tile = grid.objects[(int)Mathf.Floor(pos.x), (int)Mathf.Round(pos.y / 0.75f)].GetComponent(typeof(tileData)) as tileData;
                tile.tank = null;
                tile = grid.objects[(int)Mathf.Floor(prevMove[i].x), (int)Mathf.Round(prevMove[i].y / 0.75f)].GetComponent(typeof(tileData)) as tileData;
                tile.tank = this;
                this.transform.position = prevMove[i];
            }
        }
    }
    void repair()
    {
        if (actions > 0)
        {
            actions--;
            health++;
            Vector3 pos = this.transform.position;
            int x = (int)Mathf.Floor(pos.x);
            int y = (int)Mathf.Round(pos.y / 0.75f);
            if (!(grid.objects[x, y].GetComponent(typeof(tileData)) as tileData ).onFire)
                OnFire = false;
            else
                OnFire = true;
        }
    }
    void showMovableTiles()
    {
        this.tag = "Moving";
        Queue<GameObject>[] queues = new Queue<GameObject>[movement];
        GameObject cur;
        tileData tile;
        moveTilesDisp = true;
        for(int i = 0; i < movement; i++)
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
        for(int i = 0; i < movement - 1; i++)
        {
            while (queues[i].Count != 0)
            {
                cur = queues[i].Dequeue();
                tile = cur.GetComponent(typeof(tileData)) as tileData;
                if (!tile.city && (tile.enemy==null)  && !tile.blocked && !tile.isNull)
                {
                    if(moveTiles[(int)Mathf.Floor(cur.transform.position.x), (int)Mathf.Round(cur.transform.position.y / 0.75f)] == null && cur.transform.position != grid.objects[(int)Mathf.Floor(this.transform.position.x),(int)Mathf.Round(this.transform.position.y/0.75f)].transform.position && tile.tank == null)
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
    }
    Vector3 offToAxial(Vector3 off)
    {
        Vector3 temp = new Vector3((off.x - Mathf.Floor(off.y / 2)), off.y, 0);
        temp.z = 0 - temp.x - temp.y;
        return temp;
    }
    public void hideMoveableTiles()
    {
        this.tag = "Untagged";
        for(int i = 0; i < moveTiles.GetLength(0); i++)
        {
            for(int j = 0; j < moveTiles.GetLength(1); j++)
            {
                if (moveTiles[i, j] != null)
                {
                    Destroy(moveTiles[i, j]);
                    moveTiles[i, j] = null;
                }
            }
        }
        moveTilesDisp = false;
    }
    void showHittableTiles1()
    {
        if(actions > 1)
        {
            this.tag = "Firing1";
            bool[,] hitTiles = weap1.tilesHittable(this.transform.position);
            for (int i = 0; i < hitTiles.GetLength(0); i++)
            {
                for (int j = 0; j < hitTiles.GetLength(1); j++)
                {
                    if (hitTiles[i, j])
                    {
                        if (j % 2 == 1)
                        {
                            hittableTiles[i,j] = Instantiate(redTile, new Vector3(i + 0.5f, 0.75f * j, -0.1f), Quaternion.identity) as GameObject;
                        }
                        else
                        {
                            hittableTiles[i,j] = Instantiate(redTile, new Vector3(i, 0.75f * j, -0.1f), Quaternion.identity) as GameObject;
                        }

                    }
                }
            }
            hitTilesDisp = true;
        }
    }
    public void fire1(Vector3 target)
    {
        if (actions > 1)
        {
            this.tag = "Untagged";
            removeHitTiles();
            weap1.fire(this.transform.position,target);
            actions -= 2;
        }
    }
    void showHittableTiles2()
    {
        if (actions > 1)
        {
            this.tag = "Firing2";
            bool[,] hitTiles = weap2.tilesHittable(this.transform.position);
            for (int i = 0; i < hitTiles.GetLength(0); i++)
            {
                for (int j = 0; j < hitTiles.GetLength(1); j++)
                {
                    if (hitTiles[i, j] != null)
                    {
                        if (i % 2 == 1)
                        {
                            hittableTiles[i,j] = Instantiate(redTile, new Vector3(i + 0.5f, 0.75f * j, -0.1f), Quaternion.identity) as GameObject;
                        }
                        else
                        {
                            hittableTiles[i,j] = Instantiate(redTile, new Vector3(i, 0.75f * j, -0.1f), Quaternion.identity) as GameObject;
                        }
                    }
                }
            }
            hitTilesDisp = true;
        }
    }
    public void fire2(Vector3 target)
    {
        if (actions > 1)
        {
            this.tag = "Untagged";
            removeHitTiles();
            weap2.fire(this.transform.position,target);
            actions -= 2;
        }
    }
    void removeHitTiles()
    {
        this.tag = "Untagged";
        for(int i = 0; i < hittableTiles.GetLength(0); i++)
        {
            for(int j = 0; j < hittableTiles.GetLength(1); j++)
            {
                if (hittableTiles[i, j] != null)
                {
                    Destroy(hittableTiles[i, j]);
                    hittableTiles[i, j] = null;
                }
            }
        }
        hitTilesDisp = false;
    }
    void OnMouseDown()
    {
        if (actions > 2)
        {
            GameObject[] tanks = GameObject.FindGameObjectsWithTag("Moving");
            if (tanks.GetLength(0) > 0)
            {
                foreach(GameObject temp in tanks)
                {
                    Tank t = temp.GetComponent(typeof(Tank)) as Tank;
                    t.hideMoveableTiles();
                }
            }
            tanks = GameObject.FindGameObjectsWithTag("Firing1");
            if (tanks.GetLength(0) > 0)
            {
                foreach(GameObject temp in tanks)
                {
                    Tank t = temp.GetComponent(typeof(Tank)) as Tank;
                    t.removeHitTiles();
                }
            }
            tanks = GameObject.FindGameObjectsWithTag("Firing2");
            if (tanks.GetLength(0) > 0)
            {
                foreach (GameObject temp in tanks)
                {
                    Tank t = temp.GetComponent(typeof(Tank)) as Tank;
                    t.removeHitTiles();
                }
            }
            showMovableTiles();
        }
        else if(actions > 1)
        {
            GameObject[] tanks = GameObject.FindGameObjectsWithTag("Moving");
            if (tanks.GetLength(0) > 0)
            {
                foreach (GameObject temp in tanks)
                {
                    Tank t = temp.GetComponent(typeof(Tank)) as Tank;
                    t.hideMoveableTiles();
                }
            }
            tanks = GameObject.FindGameObjectsWithTag("Firing1");
            if (tanks.GetLength(0) > 0)
            {
                foreach (GameObject temp in tanks)
                {
                    Tank t = temp.GetComponent(typeof(Tank)) as Tank;
                    t.removeHitTiles();
                }
            }
            tanks = GameObject.FindGameObjectsWithTag("Firing2");
            if (tanks.GetLength(0) > 0)
            {
                foreach (GameObject temp in tanks)
                {
                    Tank t = temp.GetComponent(typeof(Tank)) as Tank;
                    t.removeHitTiles();
                }
            }
            showHittableTiles1();
        }
    }
    public void damage(int d)
    {
        for (int i = 1; i <= d && health > 0; i++)
        {
            health--;
        }
        if (health == 0)
            this.die();
    }
    void die()
    {
        Destroy(this);
    }
}