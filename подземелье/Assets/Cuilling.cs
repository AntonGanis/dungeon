using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuilling : MonoBehaviour
{
    public ButtonCuilling[] buttons;
    public List<ButtonCuilling> buttons_done;
    public thorns zone;
    public bool lose;
    bool rr;
    float uiu;
    void Start()
    {
        int[] U = new int[buttons.Length];
        for (int i = 0; i < buttons.Length; i++) { U[i] = i + 1; }
        for (int i = buttons.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); 
            int temp = U[i];
            U[i] = U[j];
            U[j] = temp;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].I = U[i];
        }
    }

    void Update()
    {
        if(zone.kill == true && lose == false)
        {        
            if (buttons_done.Count == buttons.Length)
            {
                int min = 0, diactiv = 0;
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons_done[i].I > min) { min = buttons_done[i].I; diactiv++; }
                    else { rr = true; }
                }
                if(diactiv == buttons.Length)
                {
                    gameObject.GetComponent<Animator>().SetInteger("действие", 0);
                    zone.kill = false;
                    rr = true;
                }
            }
            else if(buttons_done.Count == 0 && rr == false)
            {
                gameObject.GetComponent<Animator>().SetInteger("действие", 1);
            }
        }
        else
        {
            if (buttons_done.Count != 0)
            {
                Diactiving();
            }
        }
        if (rr)
        {
            uiu += 0.1f;
            if (uiu > 3)
            {
                uiu = 0;
                Diactiving();
                rr = false;
            }
        }

    }
    void Diactiving()
    {
        for (int i = 0; i < buttons_done.Count; i++)
        {
            buttons_done[i].gameObject.SetActive(true);
        }
        buttons_done = new List<ButtonCuilling>();
    }
}
