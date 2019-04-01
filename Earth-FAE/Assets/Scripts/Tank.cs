using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private BoardManager grid;
    private int movement = 3;
    private int health = 3;
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
        hittableTiles = new GameObject[grid.getRows(), grid.getCols()];
        moveTiles = new GameObject[grid.getRows(), grid.getCols()];
        actions = maxActions;
        weap1 = new normalShot();
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
        int x = (int) Mathf.Floor(this.transform.position.x);
        int y = (int) Mathf.Round(this.transform.position.y / 0.75f);
        for(int i = 0; i < moveTiles.GetLength(0); i++)
        {
            for(int j = 0; j < moveTiles.GetLength(1); j++)
            {
                if (Mathf.Max((float)(i-x),(float)(j-y)) < movement) 
                {
                    if (j % 2 == 1)
                    {
                        moveTiles[i, j] = Instantiate(greenTile, new Vector3(i + 0.5f, 0.75f * j, -0.1f), Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        moveTiles[i, j] = Instantiate(greenTile, new Vector3(i, 0.75f * j, -0.1f), Quaternion.identity) as GameObject;
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
            showMovableTiles();
        }
        else if(actions > 1)
        {
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

    }
}