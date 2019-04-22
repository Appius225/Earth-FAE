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
    private int[] tank1 = new int[2];
    private int weap1Num = 0;
    private int[] tank2 = new int[2];
    private int weap2Num = 0;
    private int[] tank3 = new int[2];
    private int weap3Num = 0;

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

    public void tank1Weap(int weap)
    {
        tank1[weap1Num] = weap;
        weap1Num++;
        if (weap1Num == 2)
        {
            weap1Num = 0;
        }
    }
    public void tank2Weap(int weap)
    {
        tank2[weap2Num] = weap;
        weap2Num++;
        if(weap2Num == 2)
        {
            weap2Num = 0;
        }
    }
    public void tank3Weap(int weap)
    {
        tank3[weap3Num] = weap;
        weap3Num++;
        if(weap3Num == 2)
        {
            weap3Num = 0;
        }
    }

    public void PlayGame()
    {
        for (int i = 0; i < 3; i++)
        {
            if(iTank[0] == null && iTank[1] == null && iTank[2] == null)
            {

                iTank[i] = Instantiate(tanks[tankLoad[i]], new Vector3(100, 100, 0), Quaternion.identity);
            }
            DontDestroyOnLoad(iTank[i]);
            Tank t = iTank[i].GetComponent(typeof(Tank)) as Tank;
            if (i == 0) //tank 1
            {
                if (tank1[0] == 0) //weapon 1 initialization
                {
                    t.weap1 = new normalShot();
                }
                else if (tank1[0] == 1)
                {

                }
                else
                {

                }

                if (tank1[1] == 0) //weapon 2 initialization
                {
                    t.weap2 = new normalShot();
                }
                else if (tank1[1] == 1)
                {

                }
                else
                {

                }
            }
            else if (i == 2) //tank 2
            {
                if (tank2[0] == 0) //weapon 1 initialization
                {
                    t.weap1 = new normalShot();
                }
                else if (tank2[0] == 1)
                {

                }
                else
                {

                }

                if (tank2[1] == 0) //weapon 2 initialization
                {
                    t.weap2 = new normalShot();
                }
                else if (tank2[1] == 1)
                {

                }
                else
                {

                }
            }
            else //tank 3
            {
                if (tank3[0] == 0) //weapon 1 initialization
                {
                    t.weap1 = new normalShot();
                }
                else if (tank3[0] == 1)
                {

                }
                else
                {

                }

                if (tank3[1] == 0) //weapon 2 initialization
                {
                    t.weap2 = new normalShot();
                }
                else if (tank3[1] == 1)
                {

                }
                else
                {

                }
            }
        }
        metadata.Tanks[0] = iTank[0];
        metadata.Tanks[1] = iTank[1];
        metadata.Tanks[2] = iTank[2];
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