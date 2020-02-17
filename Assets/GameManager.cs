using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager main;

    public static GameManager Main
    {
        get
        {
            return main;
        }
        set => main = value;
    }

    public int CurrentSample;
    public int TotalSample;

    public int CurrentHealth;

    public GameObject PauseMenu;
    public GameObject WinMenu;
    public bool isPaused;

    public double timer;
    public float score;

    void Start()
    {
        Main = this;
        CurrentHealth = 0;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
                isPaused = false;
            }
        }
        if (CurrentSample >= 12)
        {
            print("win");
            //显示胜利菜单
            score = (float)(Time.time - timer);
            WinMenu.SetActive(true);
        }
    }
}
