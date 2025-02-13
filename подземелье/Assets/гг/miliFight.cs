using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miliFight : MonoBehaviour
{
    Animator ani;
    public Atak trigger;
    take Take;
    public GameObject[] rune;
    public Stats plar;
    public bool ataka;
    public int minus;
    public int minus2;
    public float oldSpeed;
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
        Take = FindObjectOfType<take>();
    }

    void Update()
    {
        if(oldSpeed != Take.Speed || ani.GetFloat("скорость") == 0)
        {
            oldSpeed = Take.Speed;
            ani.SetFloat("скорость", oldSpeed);
            trigger.valueDown = Take.Damag;
        }
        if (trigger.Wall == true)
        {
            ani.SetInteger("действие", 5);
            trigger.Wall = false;
            plar.Stamina -= minus2;
        }
        else if(Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.A) && plar.Stamina > 0 && ataka == false)
        {
            ani.SetInteger("действие", 2);
            plar.Stamina -= minus;
        }
        else if (Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.D) && plar.Stamina > 0 && ataka == false)
        {
            ani.SetInteger("действие", 3);
            plar.Stamina -= minus;
        }
        else if (Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.W) && plar.Stamina > 0 && ataka == false)
        {
            ani.SetInteger("действие", 1);
            plar.Stamina -= minus;
        }
        else if ((Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.S)) || Input.GetMouseButtonUp(0) && plar.Stamina > 0 && ataka == false)
        {
            ani.SetInteger("действие", 4);
            plar.Stamina -= minus;
        }
        else
        {
            ani.SetInteger("действие", 0);
        }
        if(Take.Element0 != 0)
        {
            rune[Take.Element0 - 1].SetActive(true);
        }
    }
}