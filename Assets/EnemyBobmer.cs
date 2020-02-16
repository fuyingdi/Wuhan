using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBobmer : MonoBehaviour
{
    // Start is called before the first frame update
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
            GetComponent<SpriteRenderer>().DOColor(Color.white, 0.5f).OnComplete(()=> { Bomb(); });
            foreach (Transform _ in transform)
            {
                _.gameObject.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.5f);
            }
            //attacking = true;
        }
    }
    void Bomb()
    {
        var shootRange = 5f;
        foreach  (Transform _ in transform)
        {
            if (_.gameObject.name != "Stabs(Clone)")
            _.DOMove(_.transform.position + (_.transform.position - transform.position).normalized * shootRange, 1.5f).OnComplete(()=>{ Destroy(_.gameObject); });
        }
    }
}
