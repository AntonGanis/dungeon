using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{
    Transform plar;
    public GameObject[] point;
    public float predel;
    float dist;
    int kadr;

    void Update()
    {
        if(plar == null)
        {
            plar = FindObjectOfType<take>().GetComponent<Transform>();
        }
        else
        {
            if (kadr > 5)
            {
                dist = Vector3.Distance(plar.position, transform.position);
                if (dist < predel)
                {
                    for (int i = 0; i < point.Length; i++)
                    {
                        if(point[i] != null) point[i].SetActive(true);
                    }
                }
                else
                {
                    for (int i = 0; i < point.Length; i++)
                    {
                        if (point[i] != null) point[i].SetActive(false);
                    }
                }
                kadr = 0;
            }
            else { kadr += 1; }
        }
        
    }
}
