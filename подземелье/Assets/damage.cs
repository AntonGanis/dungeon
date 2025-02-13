using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    public int valueDown;
    int valueDown2;
    public int TIP;//0простой   1огонь  2яд    3ловушка    4пуля    5сбить протфель
    public bool gul;
    public bool zombi;
    public bool backpack;
    bool tyt;
    Stats plar;
    float time;
    

    void Start()
    {
        valueDown2 = valueDown;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Atak>() && col.gameObject.GetComponent<Atak>().shield)
        {
            valueDown2 = 0;
            col.gameObject.GetComponent<Atak>().Stamina -= valueDown;
        }
        else if (col.gameObject.GetComponent<Stats>())
        {
            if (TIP != 3)
            {
                col.GetComponent<Stats>().Health -= valueDown2;
                col.GetComponent<Stats>().wounds -= valueDown2;
                if (TIP == 1)
                {
                    tyt = true;
                    plar = col.gameObject.GetComponent<Stats>();
                }
                if (TIP == 2)
                {
                    col.GetComponent<Stats>().RegrnTime += 5;
                    col.GetComponent<Stats>().RegrnTime2 += col.GetComponent<Stats>().RegrnTime - 1;
                    col.GetComponent<Stats>().Regen -= 1;
                }
            }
            else
            {
                col.GetComponent<Stats>().Health -= valueDown;
                col.GetComponent<Stats>().wounds -= valueDown;
            }
        }
        if (gul)
        {
            if (col.gameObject.GetComponent<Snail>() || col.gameObject.GetComponent<ZombiGul>())
            {
                col.GetComponent<EnemyHealth>().health -= valueDown * 3;
            }
        }
        if (zombi)
        {
            if (col.gameObject.GetComponent<Snail>() || col.gameObject.GetComponent<Gul>())
            {
                col.GetComponent<EnemyHealth>().health -= valueDown * 3;
            }
        }
        if (backpack)
        {
            if (col.gameObject.GetComponent<Inventory>())
            {
                col.GetComponent<Inventory>().weapon = null;
                col.GetComponent<Inventory>().plar = null;
                col.transform.parent = null;
            }

        }
        if (TIP == 4)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<Atak>() && col.gameObject.GetComponent<Atak>().shield)
        {
            valueDown2 = valueDown;
        }
        else if (col.gameObject.GetComponent<Stats>())
        {
            if (TIP == 1)
            {
                tyt = false;
            }
        }
    }
    void Update()
    {
        if (tyt)
        {
            time += 0.1f;
            if (time > 2)
            {
                plar.Health -= valueDown;
                plar.wounds -= valueDown;
                time = 0;
            }
        }
    }
}