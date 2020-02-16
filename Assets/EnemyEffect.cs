using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyEffect : MonoBehaviour
{
    GameObject Stabs;
    public GameObject StabTemplate;
    [Range(4,16)]
    public int count=4;
    int _count;
    bool isBomb;
    void Start()
    {
        if (transform.Find("Stabs") == null) Stabs = Instantiate(new GameObject("Stabs"), transform.position, Quaternion.identity, transform);
        else Stabs = transform.Find("Stabs").gameObject;
        count = 10;
        if (GetComponent<EnemyBobmer>() != null) isBomb = true;
    }

    void Update()
    {
        if (_count != count)
        {
            _count = count;
            SetStab();
        }
    }
    void SetStab()
    {
        // Clear
        if (Stabs != null)
        {
            Destroy(Stabs);
            Stabs = Instantiate(new GameObject("Stabs"), transform.position, Quaternion.identity, transform);
        }
        for (int i = 0; i < 360; i+=360/count)
        {
            Vector3 pos = transform.position + Quaternion.AngleAxis(i, Vector3.forward) * Vector3.up * 0.15f;
            Instantiate(StabTemplate, pos, Quaternion.AngleAxis(i, Vector3.forward),Stabs.transform);
        }
        SetScale();
        if (isBomb)
        {
            GameObject Stabs = Instantiate(new GameObject("Bullets"), transform.position, Quaternion.identity, transform);
            for (int i = 0; i < 360; i += 360 / count)
            {
                Vector3 pos = transform.position + Quaternion.AngleAxis(i, Vector3.forward) * Vector3.up * 0.15f;
                Instantiate(StabTemplate, pos, Quaternion.AngleAxis(i, Vector3.forward), Stabs.transform);
            }
        }
    }

    void SetScale()
    {
        if (isBomb)
        {
            foreach (Transform t in Stabs.transform)
            {
                Sequence seq = DOTween.Sequence();
                for (int i = 0; i < 4; i++)
                {
                    //seq.Append(t.DOPunchScale(new Vector3(1.5f,1.5f,1),3f,1,0.1f));
                    seq.Append(t.DOScaleY(Random.Range(1.2f, 1.8f), 1f));
                    seq.Append(t.DOScaleY(Random.Range(0.75f, 1.0f), 1f));
                }
                seq.SetLoops(-1);
            }
            return;
        }
        foreach (Transform t in Stabs.transform)
        {
            Sequence seq = DOTween.Sequence();
            for (int i = 0; i < 4; i++)
            {
                //seq.Append(t.DOPunchScale(new Vector3(1.5f,1.5f,1),3f,1,0.1f));
                seq.Append(t.DOScale(Random.Range(1.2f, 1.8f), 1f));
                seq.Append(t.DOScale(Random.Range(0.75f, 1.0f), 1f));
            }
            seq.SetLoops(-1);
        }
    }


}
