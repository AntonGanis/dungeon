using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentakl : MonoBehaviour
{
    public Transform eatPoint;

    public Transform plar;
    public Transform point;
    public Transform next;
    public tentakl next_tentakl;

    public Low lw;
    EnemyHealth Health;
    public Rigidbody rb;
    public ConfigurableJoint cj;

    public int Max;

    public ZombiGul[] zombi;

    void Start()
    {
        Health = gameObject.GetComponent<EnemyHealth>();
        if (Max == 0)
        {
            gameObject.GetComponent<tentakl>().enabled = false;
            Destroy(gameObject.GetComponent<Low>());
        }
    }
    void Update()
    {
        if (next_tentakl != null)
        {
            if (Health.health < 100000)
            {
                transform.parent = null;
                rb.isKinematic = false;
                next_tentakl.Health.enabled = false;
                next_tentakl.GetComponent<delate>().enabled = true;
                gameObject.GetComponent<delate>().enabled = true;

                Destroy(cj);
                Destroy(Health);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(gameObject.GetComponent<tentakl>());
            }
            else if (Health.enabled == false)
            {
                rb.isKinematic = false;
                next_tentakl.Health.enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;

                if (next_tentakl.Max == 0)
                {
                    next_tentakl.rb.isKinematic = false;
                    next_tentakl.GetComponent<BoxCollider>().enabled = false;
                }

                Destroy(gameObject.GetComponent<tentakl>());
            }
        }
        else
        {
            if (Health.enabled == false)
            {
                rb.isKinematic = false;          
                Destroy(gameObject.GetComponent<tentakl>());
            }
        }
    }
    public void Next()
    {
        if (Max > 0)
        {
            Max--;
            next_tentakl = Instantiate(next, point.position, point.rotation, transform).GetComponent<tentakl>();
            next_tentakl.plar = plar;
            next_tentakl.eatPoint = eatPoint;
            next_tentakl.lw.target = plar;
            next_tentakl.cj.connectedBody = rb;
            next_tentakl.Max = Max;
        }
        Destroy(gameObject.GetComponent<Low>());
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Gul>())
        {
            Gul gg = col.gameObject.GetComponent<Gul>();
            gg.zaraza -= 1;
            if(gg.zaraza == 0)
            {
                ZombiGul zom;
                if (gg.Algul)
                {
                    zom = Instantiate(zombi[0], gg.transform.position, gg.transform.rotation).GetComponent<ZombiGul>();
                }
                else
                {
                    zom = Instantiate(zombi[1], gg.transform.position, gg.transform.rotation).GetComponent<ZombiGul>();
                }
                zom.pointFood = eatPoint;
                Destroy(gg.gameObject);
            }
        }
    }

}
