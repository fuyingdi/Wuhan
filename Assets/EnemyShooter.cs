using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyShooter : MonoBehaviour
{
    public bool attacking;
    [Range(0.01f,1)]
    public float rotateSpeed = 1f;
    [Range(0.02f, 1f)]
    public float shootCD = 1f;
    [Range(5, 50f)]
    public float shootRange = 5f;
    public GameObject bullet;
    public AudioClip shoot;

    GameObject aimPoint;
    bool isAimed;


    Rigidbody2D rb;
    float shootTimer;
    void Start()
    {
        attacking = false;
        rb = GetComponent<Rigidbody2D>();
        shootTimer = 0;
        aimPoint = transform.Find("Aim").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed * 10));
        }
        else
        {
            if (shootTimer <= 0) Shoot();
        }
        shootTimer -= Time.deltaTime;
    }
    void Shoot()
    {
        if (!isAimed) return;
        AudioSource tempAudio=this.GetComponent<AudioSource>();
        tempAudio.clip=shoot;
        tempAudio.Play();
        var _ = Instantiate(bullet, aimPoint.transform.position, Quaternion.identity);
        _.GetComponent<SpriteRenderer>().color = Color.red;
        //var shootRange = 15f;
        _.transform.position=new Vector3(_.transform.position.x,_.transform.position.y,0);
        _.transform.DOMove(_.transform.position + (_.transform.position - transform.position).normalized * shootRange*1f,2f).OnComplete(()=> { Destroy(_.gameObject); });
        shootTimer = shootCD;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().DOColor(Color.red, 0.1f);
            attacking = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().DOColor(Color.green, 0.25f);
            attacking = false;
        }
        isAimed = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(GetComponent<SpriteRenderer>().color != Color.red) GetComponent<SpriteRenderer>().DOColor(Color.red, 0.1f);
            float _angle = Mathf.Atan2(collision.transform.position.y - transform.position.y, collision.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(_angle, Vector3.forward);
            rb.MoveRotation(angle: (rb.rotation+ (_angle-rb.rotation)*rotateSpeed));
            // rb.rotation = Mathf.Lerp(transform.rotation.eulerAngles.z, _angle, 0.2f);//Quaternion.Slerp(transform.rotation, q, 0.2f);
            if (Mathf.Abs(_angle - rb.rotation) <= 10f) isAimed = true;
        }
    }
}
