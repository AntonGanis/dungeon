using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telrportDva : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Move>())
        {
            col.gameObject.GetComponent<Move>().enabled = true;
        }
    }
}
