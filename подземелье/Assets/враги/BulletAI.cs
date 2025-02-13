using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{
    public int Speed;
    Vector3 lastPos;

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        RaycastHit hit;
        Debug.DrawLine(lastPos, transform.position);
        lastPos = transform.position;
    }
}