using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

//public class Delay:MonoBehaviour
//{
//    //public static Do()
//}

public class Player : MonoBehaviour
{
    Vector2 StartPos;
    Vector2 EndPos;
    Rigidbody2D rb;
    LineRenderer line;
    AudioSource audioSource;

    [Range(1,100)]
    public float force;
    [Range(0, 1)]
    public float timescale;
    public int HP;
    public bool inevitable;
    [Header("Sounds")]
    public AudioClip boost;
    public AudioClip dead;
    public AudioClip gene;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource=GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();

        HP = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Main.isPaused) return;
        if (Input.GetMouseButtonDown(0))
        {
            //StartPos设为鼠标的位置
            StartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.positionCount = 2;
            line.SetPosition(0, (Vector2)transform.position);
            Time.timeScale = timescale;
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //rb.AddForce(force * (EndPos - StartPos));
            rb.velocity = (force * (EndPos - StartPos));
            line.positionCount = 0;
            Time.timeScale = 1;
            audioSource.clip=boost;
            audioSource.Play();
        }
        if (Input.GetMouseButton(0))
        {
            line.SetPosition(0, (Vector2)transform.position);
            line.SetPosition(1, (Vector2)transform.position+(Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - StartPos);
        }
    }

    public void Hurt()
    {
        if (inevitable) return;
        else
        {
            inevitable = true;
            Sequence Blink = DOTween.Sequence();
            for (int i = 0; i < 5; i++)
            {
                Blink.Append(GetComponent<SpriteRenderer>().DOFade(0, 0.1f));
                Blink.Append(GetComponent<SpriteRenderer>().DOFade(1, 0.1f));
                Blink.OnComplete(() => { inevitable = false; });
            }
            audioSource.clip=dead;
            HP--;
            if (HP <= 0) Die();

        }
    }
    void Die()
    {
        audioSource.clip=dead;
        audioSource.Play();
        GameManager.Main.Die();
    }
}
