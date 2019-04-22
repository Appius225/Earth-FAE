using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{

    private int level;
    public GameObject[] tanks;
    private int[] tankLoad = new int[3];
    private GameObject[] iTank = new GameObject[3];

    public void loadTank1(int var)
    {
        tankLoad[0] = var;
    }
    public void loadTank2(int var)
    {
        tankLoad[1] = var;
    }
    public void loadTank3(int var)
    {
        tankLoad[2] = var;
    }
    public void PlayGame()
    {
        if (iTank[0] == null && iTank[1] == null && iTank[2] == null)
        {
            iTank[0] = Instantiate(tanks[tankLoad[0]], new Vector3(100, 100, 0), Quaternion.identity);
            iTank[1] = Instantiate(tanks[tankLoad[1]], new Vector3(100, 100, 0), Quaternion.identity);
            iTank[2] = Instantiate(tanks[tankLoad[2]], new Vector3(100, 100, 0), Quaternion.identity);
            DontDestroyOnLoad(iTank[0]);
            DontDestroyOnLoad(iTank[1]);
            DontDestroyOnLoad(iTank[2]);

            metadata.Tanks[0] = iTank[0];
            metadata.Tanks[1] = iTank[1];
            metadata.Tanks[2] = iTank[2];
        }
        SceneManager.LoadScene(level);
    }
    public void LoadScene(int level)
    {
        this.level = level;
    }

    void awake()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < tanks.Length; j++)
            {

            }
        }
    }
}