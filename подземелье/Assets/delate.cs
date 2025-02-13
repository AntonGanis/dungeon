using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delate : MonoBehaviour
{
    public float time;
    public float lemit;
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > lemit)
        {
            Destroy(gameObject);
        }
    }
}
