using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    public Player player;

    private int health;

    public GameObject[] Hearts;

    public int Health
    {
        get => health;
        set
        {
            if(value<=5&&value>0)health = value;
        }
    }

    void Start()
    {
        if (player == null) player = GameObject.Find("Player").GetComponent<Player>();
        SetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth();
    }

    void SetHealth()
    {
        Health = player.HP;
        for (int i = 0; i < Health; i++)
        {
            Hearts[i].SetActive(true);
        }
        for(int i = Health; i < 5; i++)
        {
            Hearts[i].SetActive(false);
        }
    }
}
