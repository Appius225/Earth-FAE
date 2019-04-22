using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject tank;
    public GameObject spawn;
    public Boolean spawned = false;
    private Boolean initialized = false;
    public Vector3[] spawnPos = new Vector3[3];
    private GameObject[] tanks = new GameObject[3];
    public int cfgNum;
    private int columns = 15;
    private int rows = 10;
    private string cfgFileTemplate = "Assets/Config/Board_X.cfg";
    private List<Vector3> hexGridPositions = new List<Vector3>();
    private GameObject[,] objectPositions;
    public bool[,] cityTiles;
    public bool[] cityRows;
    public int cityHealth = 10;
    private Transform boardContainer;
    private Transform enemyContainer;
    public GameObject[,] objects; //2D array holding the initialized tiles
    private bool waiting;
    private GameObject[,] spawningPos;

    public float difficulty = 2;
    public GameObject[] enemyFabs;

    public Boolean turnEnded;
   // public GameObject endTurn;

    Camera MainCamera;
    public static Vector3 screenCenter;
    public int getRows()
    {
        return rows;
    }
    public int getCols()
    {
        return columns;
    }
    private static Dictionary<string, int> tileToIntMap = new Dictionary<string, int>()
    {
        { "N", 0 },
        { "D", 1 },
        { "G", 2 },
        { "W", 3 },
        { "C", 4 },
        { "B", 5 },
    };

    /*
     *  This function takes the member cfgNum and tries to load a board config from it
     *  The board config template is specified in the member cfgFileTemplate
     *  If a valid board cfg is not found with the specified cfgNum then
     *  a default map will be loaded.
     *  
     *  Valid cfg looks as follows:
     *  4,4
     *  0,0,0,0
     *  0,0,0,0
     *  0,0,0,0
     *  0,0,0,0
     *  
     *  where 4,4 are the x,y dimensions
     *  and the objects in the grid define the type of tile in that position
     *  
     *  N = null tile
     *  D = dirt tile
     *  G = grass tile
     *  W = water tile
     *  C = city tile
     *  B = boulder tile
     *  ...
     */
    void FillObjectPositions()
    {
        string cfgPath = cfgFileTemplate.Replace("X", cfgNum.ToString());
        if (File.Exists(cfgPath))
        {
            StreamReader cfgIn = new StreamReader(cfgPath);
            int[] dimensions = Array.ConvertAll<string, int>(cfgIn.ReadLine().Split(','), int.Parse);
            columns = dimensions[0];
            rows = dimensions[1];
            objectPositions = new GameObject[columns, rows];
            objects = new GameObject[columns, rows];
            cityTiles = new bool[columns, rows];
            cityRows = new bool[rows];
            spawningPos = new GameObject[columns, rows];
            // Debug.Log(columns);
            // Debug.Log(rows);
            for(int i = 0; i < rows; i++)
            {
                cityRows[i] = false;
            }
            for(int i = 0; i < rows; i++)
            {
                string[] row = cfgIn.ReadLine().Split(',');
                // foreach(var item in row)
                // {
                //     Debug.Log(item);
                // }
                
                for(int j = 0; j < columns; j++)
                {
                    if (tileToIntMap[row[j]] == 4)
                    {
                        cityTiles[j, i] = true;
                        cityRows[i] = true;
                    }
                    else
                    {
                        cityTiles[j, i] = false;
                    }
                    objectPositions[j, i] = tilePrefabs[tileToIntMap[row[j]]];
                }
            }
        }
        else
        {
            Debug.Log("Error: Config Not Found!");
        }

        float new_x = ((columns / 2) - 1.0f) + (columns % 2 == 0 ? 0.5f : 1.0f); //new calculation for camera
        float new_y = ((rows / 2) - 1.0f) - (((rows / 2) - 1.0f) * .25f) + 0.5f;     // for camera centering; this one should be close enough

        screenCenter = new Vector3(new_x, new_y, -10.0f);

        //Debug.Log(new_x);
        //Debug.Log(new_y);
    }

    void InitializeGrid()
    {
        hexGridPositions.Clear();

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (x % 2 == 1) hexGridPositions.Add(new Vector3(x + 0.5f, y - ( y * .25f ), 0.0f));
                else hexGridPositions.Add(new Vector3(x, y - (y * .25f), 0.0f));
            }
        }
    }

    void BoardSetup()
    {
        boardContainer = new GameObject("Container").transform;
        enemyContainer = new GameObject("Enemy Container").transform;
        int cities = 0;
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject toInstantiate = objectPositions[x,y];
                if (y % 2 == 1)
                {
                    if (cityTiles[x, y])
                    {
                        cities++;
                        objects[x, y] = Instantiate(toInstantiate, new Vector3(x + 0.5f, y * 0.75f, -0.008f + 0.001f * cities), Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        objects[x, y] = Instantiate(toInstantiate, new Vector3(x + 0.5f, y - (y * .25f), 0.0f), Quaternion.identity) as GameObject;
                    }
                }
                else
                {
                    if (cityTiles[x, y])
                    {
                        cities++;
                        objects[x, y] = Instantiate(toInstantiate, new Vector3(x, y * 0.75f, -0.008f + 0.001f * cities), Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        objects[x, y] = Instantiate(toInstantiate, new Vector3(x, y - (y * .25f), 0.0f), Quaternion.identity) as GameObject;
                    }
                }
                objects[x,y].transform.SetParent(boardContainer);
            }
        }
    }

    IEnumerator testTankSpawn()
    {
        while (waiting)
        {
            yield return new WaitForEndOfFrame();
        }
        GameObject[,] spawnable = new GameObject[2, rows];
        for(int x = 0; x < 2; x++)
        {
            for(int y = 0; y < rows; y++)
            {
                if (y % 2 == 1 && x != 1)
                    spawnable[x,y] = Instantiate(spawn, new Vector3(x + 0.5f, y - (y * .25f), -0.1f), Quaternion.identity) as GameObject;
                else if (!(y%2==1 && x==1))
                    spawnable[x,y] = Instantiate(spawn, new Vector3(x, y - (y * .25f), -0.1f), Quaternion.identity) as GameObject;
            }
        }
        Vector3 def = new Vector3(0.0f, 0.0f, 0.0f);
        while (spawnPos[0] == def || spawnPos[1] == def || spawnPos[2] == def) 
        {
            yield return new WaitForEndOfFrame();
        }
        for(int x = 0; x < 2; x++)
        {
            for(int y = 0; y < rows; y++)
            {
                Destroy(spawnable[x,y]);
            }
        }
        Vector3[] startPos = new Vector3[3];
        for(int i = 0; i < 3; i++)
        {
            startPos[i] = new Vector3(spawnPos[i].x - 5.0f,spawnPos[i].y,spawnPos[i].z);
            tanks[i] = Instantiate(tank,startPos[i],Quaternion.identity) as GameObject;
        }
        float elapsedTime = 0.0f;
        while (elapsedTime < 0.5f)
        {
            tanks[0].transform.position = Vector3.Lerp(startPos[0], spawnPos[0], (elapsedTime / 0.5f));
            tanks[1].transform.position = Vector3.Lerp(startPos[1], spawnPos[1], (elapsedTime / 0.5f));
            tanks[2].transform.position = Vector3.Lerp(startPos[2], spawnPos[2], (elapsedTime / 0.5f));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        tanks[0].transform.position = spawnPos[0];
        tanks[1].transform.position = spawnPos[1];
        tanks[2].transform.position = spawnPos[2];
        tileData tile;
        tile = objects[(int)Mathf.Floor(spawnPos[0].x), (int)Mathf.Round(spawnPos[0].y / 0.75f)].GetComponent(typeof(tileData)) as tileData;
        tile.tank = tanks[0].GetComponent(typeof(Tank)) as Tank;
        tile = objects[(int)Mathf.Floor(spawnPos[1].x), (int)Mathf.Round(spawnPos[1].y / 0.75f)].GetComponent(typeof(tileData)) as tileData;
        tile.tank = tanks[1].GetComponent(typeof(Tank)) as Tank;
        tile = objects[(int)Mathf.Floor(spawnPos[2].x), (int)Mathf.Round(spawnPos[2].y / 0.75f)].GetComponent(typeof(tileData)) as tileData;
        tile.tank = tanks[2].GetComponent(typeof(Tank)) as Tank;
        spawned = true;
    }
    public void endTurnButton()
    {
        turnEnded = true;
    }
    IEnumerator turn()
    {
        while (!initialized)
        {
            yield return new WaitForEndOfFrame();
        }
        //GameObject endButton = Instantiate(endTurn, new Vector3(screenCenter.x, 0.75f * rows + 0.5f, 0.0f), Quaternion.identity) as GameObject;
        for(int i = 0; i < 5; i++)
        {
            while (!turnEnded)
            {
                yield return new WaitForEndOfFrame();
            }
            //Destroy(endButton);
            fire();       //Implemented, but no animation
            waiting = true;
            StartCoroutine(enemyAttacks());
            Debug.Log("Attacking");
            while (waiting)
            {
                //Debug.Log(waiting);
                yield return new WaitForEndOfFrame();
            }
            if (i != 4)
            {

                waiting = true;
                StartCoroutine(enemySpawn());
                Debug.Log("Spawning");
                while (waiting)
                {
                    yield return new WaitForEndOfFrame();
                }
                waiting = true;
                StartCoroutine(enemyMove());
                Debug.Log("Moving");
                while (waiting)
                {
                    yield return new WaitForEndOfFrame();
                }
                /*waiting = true;
                StartCoroutine(enemyNewSpawns());
                while (waiting)
                {
                    yield return new WaitForEndOfFrame();
                }*/
                turnEnded = false;
                //endButton = Instantiate(endTurn, new Vector3(screenCenter.x, 0.75f * rows + 0.5f, 0.0f), Quaternion.identity) as GameObject;
                for(int j = 0; j < 3; j++)
                {
                    (tanks[j].GetComponent(typeof(Tank)) as Tank).actions = (tanks[j].GetComponent(typeof(Tank)) as Tank).maxActions;
                }
            }
            else
            {
                //traverse back to selection scene
            }
        }
    }
    IEnumerator enemySpawn()
    {
        int num = UnityEngine.Random.Range(2, (int)Mathf.Floor(difficulty) + 1);
        GameObject temp;
        for(int i = 0; i < num; i++)
        {
            float y = UnityEngine.Random.Range(0, rows);
            float x = (float)UnityEngine.Random.Range(columns * 2 / 3 - 1, columns - 1);
            tileData tile = objects[(int)x, (int)y].GetComponent(typeof(tileData)) as tileData;
            if (y % 2 == 1)
            {
                x += 0.5f;
            }
            y = y * 0.75f;
            while(tile.enemy!=null || tile.isWater || tile.blocked || tile.city || tile.tank != null)
            {

            }
            temp = Instantiate(enemyFabs[UnityEngine.Random.Range(0, enemyFabs.GetLength(0))], new Vector3(12, y, -0.1f), Quaternion.identity) as GameObject;
            float elapsedTime = 0.0f;
            while (elapsedTime < 0.5f)
            {
                temp.transform.position = Vector3.Lerp(new Vector3(12, y, -0.1f), new Vector3(x, y, -0.1f), elapsedTime / 0.5f);
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            temp.transform.position = new Vector3(x, y, -0.1f);
            temp.transform.SetParent(enemyContainer);
            tile.enemy = temp.GetComponent(typeof(Enemy)) as Enemy;
        }
        waiting = false;
    }
    void fire()
    {
        tileData tile;
        for(int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                tile = objects[i,j].GetComponent(typeof(tileData)) as tileData;
                if (tile.onFire)
                {
                    //fire damaging animation of some variety here
                    if(tile.enemy != null)
                    {
                        tile.enemy.damage(1);
                    }
                    else if(tile.tank != null)
                    {
                        tile.tank.damage(1);
                    }
                }
                else if (tile.enemy != null)
                {
                    if (tile.enemy.OnFire)
                    {
                        tile.enemy.damage(1);
                    }
                }
                else if (tile.tank != null)
                {
                    if (tile.tank.OnFire)
                    {
                        tile.tank.damage(1);
                    }
                }
            }
        }
    }
    IEnumerator enemyAttacks()
    {
        tileData tile;
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                tile = objects[i, j].GetComponent(typeof(tileData)) as tileData;
                if (tile.enemy != null)
                {
                    tile.enemy.isWaiting = true;
                    StartCoroutine(tile.enemy.attack());
                    while (tile.enemy.isWaiting)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
        }
        Debug.Log(waiting);
        waiting = false;
    }
    IEnumerator enemyMove()
    {
        while (!spawned)
        {
            yield return new WaitForEndOfFrame();
        }
        tileData tile;
        for(int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                tile = objects[i, j].GetComponent(typeof(tileData)) as tileData;
                if (tile.enemy != null)
                {
                    Enemy e = tile.enemy;
                    e.isWaiting = true;
                    StartCoroutine(e.move());
                    while (e.isWaiting)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
        }
        waiting = false;
        initialized = true;
    }
    public void damageCity(int d)
    {
        for(int i = 0; i < d && cityHealth > 0; i++)
        {
            cityHealth--;
        }
        if(cityHealth == 0)
        {
            //lose game
        }
    }

    void Awake()
    {
        FillObjectPositions();
        InitializeGrid();
        BoardSetup();
        MainCamera = Camera.main;

        MainCamera.transform.position = screenCenter;
        MainCamera.orthographicSize = ((rows - (rows * .25f)) / 2.0f) + 1.5f; // calculate the size of the camera

        waiting = true;
        spawned = false;
        StartCoroutine(enemySpawn());
        StartCoroutine(testTankSpawn());
        StartCoroutine(enemyMove());
        StartCoroutine(turn());
    }
}
