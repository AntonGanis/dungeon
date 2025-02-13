using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune : MonoBehaviour
{
    public int Element;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Item>() )
        {
            Item uyu = col.gameObject.GetComponent<Item>();
            if (uyu.weapon && uyu.weapon_left == false && uyu.element == 0)
            {
                uyu.element = Element;
                Destroy(gameObject);
            }
        }
    }
}
