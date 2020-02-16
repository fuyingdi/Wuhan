using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[ExecuteInEditMode]
public class TrainEnd : MonoBehaviour
{
    public GameObject nextEnd;
    [Range(0.1f,10f)]
    public float speed = 1f;
    [Space]
    public GameObject target;

    void Start()
    {
        //nextEnd = transform.Find("TrailEnd")!=null?transform.Find("TrainEnd").gameObject:null;
        if(target!=null)
            ChangeToEnd();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void ChangeToEnd()
    {
        float distance = (nextEnd.transform.position - transform.position).magnitude;
        target.transform.DOMove(nextEnd.transform.position, distance/speed).OnComplete(()=> { ChangeToStart(); });
    }
    void ChangeToStart()
    {
        float distance = (nextEnd.transform.position - transform.position).magnitude;
        target.transform.DOMove(transform.position, distance /speed).OnComplete(() => { ChangeToEnd(); });  
    }
}
