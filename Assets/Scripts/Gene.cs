using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gene : MonoBehaviour
{

    public AudioClip gene;
    public bool isGot;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource tempAudio=this.GetComponent<AudioSource>();
            tempAudio.clip=gene;
            tempAudio.Play();
            Destroy(gameObject, 0.3f);
            transform.DOScale(5f, 0.3f);
            transform.DOMoveY(transform.position.y + 0.2f, 0.3f);
            GetComponent<SpriteRenderer>().DOFade(0.0f, 0.3f);
            GetPoint();
            isGot = true;
        }
    }

    void GetPoint()
    {
        if (isGot) return;
        //修改GameManager里的分数
        GameManager.Main.CurrentSample++;
        isGot = true;
        return;
    }
}
