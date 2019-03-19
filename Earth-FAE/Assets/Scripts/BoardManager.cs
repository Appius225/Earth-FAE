using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject tank;
    public int cfgNum;
    private int columns = 15;
    private int rows = 10;
    private string cfgFileTemplate = "Assets/Config/Board_X.cfg";
    private List<Vector3> hexGridPositions = new List<Vector3>();
    private GameObject[,] objectPositions;
    private Transform boardContainer;
    public GameObject[,] objects;
    public Boolean spawned = false;

    Camera MainCamera;
    public static Vector3 screenCenter;

    private static Dictionary<string, int> tileToIntMap = new Dictionary<string, int>()
    {
        { "N", 0 },
        { "D", 1 },
        { "G", 2 }
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

        float new_x = (rows / 2) + (rows % 2 == 0 ? 0.5f : 0.0f); // for camera centering; this one is exact center
        float new_y = (columns / 2) - ((columns / 2) * .25f);     // for camera centering; this one should be close enough

        screenCenter = new Vector3(new_x, new_y, -10f);

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
                    objects[x,y] = Instantiate(toInstantiate, new Vector3(x + 0.5f, y - (y * .25f), 0.0f), Quaternion.identity) as GameObject;
                else 
                    objects[x,y] = Instantiate(toInstantiate, new Vector3(x, y - (y * .25f), 0.0f), Quaternion.identity) as GameObject;

                objects[x,y].transform.SetParent(boardContainer);
            }
        }
    }

    IEnumerator testTankSpawn()
    {
        GameObject instance = Instantiate(tilePrefabs[3], new Vector3(0.0f, 0.0f, -0.1f), Quaternion.identity) as GameObject;
        while (!spawned)
        {
            yield return null;
        }
        Vector3 start = new Vector3(-4.0f, 0.0f, -0.1f);
        instance = Instantiate(tank, start, Quaternion.identity) as GameObject;
        float elapsedTime = 0.0f;
        while(elapsedTime < 1)
        {
            instance.transform.position = Vector3.Lerp(start, new Vector3(0.0f, 0.0f, -0.1f), (elapsedTime / 1));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        instance.transform.position = new Vector3(0.0f, 0.0f, -0.1f);
    }

    void Awake()
    {
        FillObjectPositions();
        InitializeGrid();
        BoardSetup();
        MainCamera = Camera.main;

        MainCamera.transform.position = screenCenter;
        MainCamera.orthographicSize = 2.5f;

        StartCoroutine(testTankSpawn());
    }
}
