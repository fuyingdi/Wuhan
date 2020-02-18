using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    public GameObject DeadMenu;
    public TextMeshProUGUI Timer;
    public bool isPaused;

    public float timer;
    public float score;

    void Start()
    {
        Main = this;
        CurrentHealth = 0;
        timer = Time.unscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        Timer.text = Utils.timeToFormat((float)Time.unscaledTime - timer);
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
        if (CurrentSample >= TotalSample||(Input.GetKey(KeyCode.P)&&Input.GetKey(KeyCode.Q)))
        {
            print("win");
            //显示胜利菜单
            score = (float)(Time.unscaledTime - timer);
            WinMenu.SetActive(true);
        }
    }

    public void Die()
    {
        DeadMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
