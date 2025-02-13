using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telrport : MonoBehaviour
{
    float time;
    int rand;
    bool p;
    telrport[] tp;
    Transform point;
    public Transform point2;

    void Update()
    {
        if (p == false)
        {
            time += 0.1f;
            if (time > 2)
            {
                tp = FindObjectsOfType<telrport>();
                
                rand = Random.Range(0, tp.Length);
                point = tp[rand].point2;
                p = true;
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Move>())
        {
            col.gameObject.GetComponent<Move>().enabled = false;
            col.gameObject.transform.position = point.position;
            rand = Random.Range(0, tp.Length);
            point = tp[rand].point2;
            //col.gameObject.GetComponent<Move>().enabled = true;
        }
    }
}
