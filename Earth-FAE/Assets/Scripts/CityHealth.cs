using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityHealth : MonoBehaviour
{
    private BoardManager grid;
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        grid = (BoardManager)FindObjectOfType(typeof(BoardManager));
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)grid.cityHealth / 10;
    }
}
