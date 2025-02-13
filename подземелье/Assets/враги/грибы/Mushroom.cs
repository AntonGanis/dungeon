using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Transform eatPoint;

    public GameObject[] children;
    public GameObject atac;

    BLIZKO Find;
    Transform Object;
    int kadr;
    Low at1;

    public int Tip_Atak;

    public float Distance;
    public float Limit;

    public Transform point;
    public Transform tent;
    public bool cel;
    public float time;
    tentakl next_tentakl;
    void Start()
    {
        int j;
        j = Random.Range(0, 11);
        if (j < 6) { gameObject.SetActive(false); }
        for (int i = 0; i < children.Length; i++)
        {
            j = Random.Range(0, 2);
            if(j == 0) { children[i].SetActive(false); }
            else { children[i].SetActive(true); }
        }
        Tip_Atak = Random.Range(0, 4);
        if(Tip_Atak == 0) 
        { 
            at1 = atac.GetComponent<Low>();
        }
        Find = gameObject.GetComponent<BLIZKO>();
    }

    void Update()
    {
        if (Tip_Atak != 2 && Tip_Atak != 3)
        {
            if (kadr == 20)
            {
                List<take> plars = new List<take>(FindObjectsOfType<take>());
                List<Snail> snails = new List<Snail>(FindObjectsOfType<Snail>());
                List<Gul> guls = new List<Gul>(FindObjectsOfType<Gul>());
                foreach (var plar in plars)
                {
                    Find.find.Add(plar.gameObject);
                }
                foreach (var gul in guls)
                {
                    Find.find.Add(gul.gameObject);
                }
                foreach (var snail in snails)
                {
                    Find.find.Add(snail.gameObject);
                }

                if (Find.closest.transform != null)
                {
                    Object = Find.closest.transform;
                    Distance = Vector3.Distance(transform.position, Object.position);
                    if (Distance < Limit)
                    {
                        if (Tip_Atak == 0)
                        {
                            atac.SetActive(true);
                            at1.enabled = true;
                            at1.target = Object;
                        }
                        else if (Tip_Atak == 1 && cel == false)
                        {
                            cel = true;
                            next_tentakl = Instantiate(tent, point.position, point.rotation, point).GetComponent<tentakl>();
                            next_tentakl.Max = Random.Range(20, 41);
                            next_tentakl.plar = Object;
                            next_tentakl.eatPoint = eatPoint;
                            next_tentakl.lw.target = Object;
                        }
                    }
                    else
                    {
                        if (Tip_Atak == 0)
                        {
                            atac.SetActive(false);
                        }
                        else if (Tip_Atak == 1 && next_tentakl != null)
                        {
                            time += Time.deltaTime;
                            if (time > 2)
                            {
                                next_tentakl.GetComponent<EnemyHealth>().health = 900;
                                cel = false;
                                time = 0;
                            }
                        }
                    }


                }
                kadr = 0;
            }
            kadr++;
        }
    }
}
