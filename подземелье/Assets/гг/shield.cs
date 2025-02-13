using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    public Stats plar;
    Animator ani;
    public Atak trigger;
    public int minus;
    public int minus2;

    int minus3;
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (trigger.blok == 2)
        {
            ani.SetInteger("действие", 2);
            plar.blok = false;
            trigger.blok = 0;
            plar.Stamina -= minus2;
        }
        else if (trigger.blok == 1)
        {
            trigger.blok = 0;
            plar.Stamina -= minus;
        }
        else if (Input.GetMouseButton(1) && plar.Stamina > 0)
        {
            ani.SetInteger("действие", 1);
            plar.blok = true;
        }
        else
        {
            ani.SetInteger("действие", 0);
            plar.blok = false;
        }
        if(trigger.Stamina != 0)
        {
            plar.Stamina -= minus3;
            trigger.Stamina = 0;
        }
    }
}
