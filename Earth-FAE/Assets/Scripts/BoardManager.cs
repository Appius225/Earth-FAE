using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public int cfgNum;
    private int columns = 15;
    private int rows = 10;
    private string cfgFileTemplate = "Assets/Config/Board_X.cfg";
    private List<Vector3> hexGridPositions = new List<Vector3>();
    private GameObject[,] objectPositions;
    private Transform boardContainer;

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
            Debug.Log(columns);
            Debug.Log(rows);
            for(int i = 0; i < columns; i++)
            {
                string[] row = cfgIn.ReadLine().Split(',');
                foreach(var item in row)
                {
                    Debug.Log(item);
                }
                
                for(int j = 0; j < rows; j++)
                {
                    objectPositions[i,j] = tilePrefabs[tileToIntMap[row[j]]];
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
    }

    void InitializeGrid()
    {
        hexGridPositions.Clear();

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (x % 2 == 1) hexGridPositions.Add(new Vector3(x + 0.5f, y - ( y * .25f ), 0f));
                else hexGridPositions.Add(new Vector3(x, y - (y * .25f), 0f));
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
                GameObject instance = null;
                if (y % 2 == 1) instance =
                    Instantiate(toInstantiate, new Vector3(x + 0.5f, y - (y * .25f), 0f), Quaternion.identity) as GameObject;
                else instance =
                    Instantiate(toInstantiate, new Vector3(x, y - (y * .25f), 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardContainer);
            }
        }
    }

    void Awake()
    {
        FillObjectPositions();
        InitializeGrid();
        BoardSetup();
    }
}
