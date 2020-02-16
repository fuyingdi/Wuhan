using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Resume()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
    public void Levels()
    {
        SceneManager.LoadScene(1);
    }
}
