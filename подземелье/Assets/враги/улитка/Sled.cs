using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sled : MonoBehaviour
{
    public Transform bullet;
    public float predel;
    float time;
    void Update()
    {
        time += Time.deltaTime * 5;
        if (time > predel)
        {
            Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
            time = 0f;
        }
    }
}
