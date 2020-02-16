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
    public bool isPaused;

    void Start()
    {
        Main = this;
        CurrentHealth = 0;
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
    }
}
