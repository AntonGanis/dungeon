using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCuilling : MonoBehaviour
{
    public int I;
    public Cuilling cuilling;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Stats>())
        {
            cuilling.buttons_done.Add(gameObject.GetComponent<ButtonCuilling>());
            gameObject.SetActive(false);
        }
    }

}
