using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject tank;
    public GameObject red;
    public Boolean spawned = false;
    private Boolean initialized = false;
    public Vector3[] spawnPos = new Vector3[3];
    private GameObject[] tanks = new GameObject[3];
    public int cfgNum;
    private int columns = 15;
    private int rows = 10;
    private string cfgFileTemplate = "Earth-FAE/Assets/Config/Board_X.cfg";
    private List<Vector3> hexGridPositions = new List<Vector3>();
    private GameObject[,] objectPositions;
    public int cityHealth = 10;
    private Transform boardContainer;
    public GameObject[,] objects;

    public Boolean turnEnded;
    public GameObject endTurn;

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
        { "W", 3 }
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
            objectPositions = new GameObject[rows, columns];
            objects = new GameObject[rows, columns];
            // Debug.Log(columns);
            // Debug.Log(rows);
            for(int i = 0; i < columns; i++)
            {
                string[] row = cfgIn.ReadLine().Split(',');
                // foreach(var item in row)
                // {
                //     Debug.Log(item);
                // }
                
                for(int j = 0; j < rows; j++)
                {
                    objectPositions[j,i] = tilePrefabs[tileToIntMap[row[j]]];
                }
            }
        }
        else
        {
            objectPositions = new GameObject[columns, rows];

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if(j % 2 == 1 && i == columns-1) objectPositions[i,j] = tilePrefabs[0]; // NULL TILE
                    else objectPositions[i,j] = tilePrefabs[1];           // DEFAULT TILE
                }
            }
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

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject toInstantiate = objectPositions[x, y];
                if (y % 2 == 1)
                    objects[x, y] = Instantiate(toInstantiate, new Vector3(x + 0.5f, y - (y * .25f), 0.0f), Quaternion.identity) as GameObject;
                else
                    objects[x, y] = Instantiate(toInstantiate, new Vector3(x, y - (y * .25f), 0.0f), Quaternion.identity) as GameObject;

                objects[x,y].transform.SetParent(boardContainer);
            }
        }
    }

    IEnumerator testTankSpawn()
    {
        GameObject[,] spawnable = new GameObject[2, rows];
        for(int x = 0; x < 2; x++)
        {
            for(int y = 0; y < rows; y++)
            {
                if (y % 2 == 1 && x != 1)
                    spawnable[x, y] = Instantiate(red, new Vector3(x + 0.5f, y - (y * .25f), -0.1f), Quaternion.identity) as GameObject;
                else if (!(y%2==1 && x==1))
                    spawnable[x, y] = Instantiate(red, new Vector3(x, y - (y * .25f), -0.1f), Quaternion.identity) as GameObject;
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
                Destroy(spawnable[x, y]);
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
        initialized = true;
    }

    IEnumerator turn()
    {
        while (!initialized)
        {
            yield return new WaitForEndOfFrame();
        }
        GameObject endButton = Instantiate(endTurn, new Vector3(screenCenter.x, 0.75f * rows + 0.5f, 0.0f), Quaternion.identity) as GameObject;
        for(int i = 0; i < 5; i++)
        {
            while (!turnEnded)
            {
                yield return new WaitForEndOfFrame();
            }
            Destroy(endButton);
            fire();
            enemyAttacks();
            enemyMove();
            if (i != 4)
            {
                turnEnded = false;
                endButton = Instantiate(endTurn, new Vector3(screenCenter.x, 0.75f * rows + 0.5f, 0.0f), Quaternion.identity) as GameObject;
                for(int j = 0; j < 3; j++)
                {
                    (tanks[j].GetComponent(typeof(Tank)) as Tank).actions = (tanks[j].GetComponent(typeof(Tank)) as Tank).maxActions;
                }
            }
        }
    }
    void fire()
    {

    }
    void enemyAttacks()
    {

    }
    void enemyMove()
    {

    }

    void Awake()
    {
        FillObjectPositions();
        InitializeGrid();
        BoardSetup();
        MainCamera = Camera.main;

        MainCamera.transform.position = screenCenter;
        MainCamera.orthographicSize = ((rows - (rows * .25f)) / 2.0f) + 1.0f; // calculate the size of the camera

        StartCoroutine(testTankSpawn());
        StartCoroutine(turn());
    }
}
