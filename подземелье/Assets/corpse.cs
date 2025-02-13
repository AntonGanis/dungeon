using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corpse : MonoBehaviour
{
    public Transform[] right;
    public Transform[] left;
    public Transform[] point;

    void Start()
    {
        int rand1 = Random.Range(0, right.Length * 3);
        int rand2 = Random.Range(0, left.Length * 3);
        if (right.Length >= rand1)
        {
            Item u1 = Instantiate(right[rand1], point[0].position, point[0].rotation).GetComponent<Item>();
            u1.speed = Random.Range(0.5f, 2f);
            if(u1.I == 0)
            {
                u1.ValueDown = Random.Range(10, 22);
            }
            else if (u1.I == 1)
            {
                u1.ValueDown = Random.Range(7, 19);
            }
            else if (u1.I == 2)
            {
                u1.ValueDown = Random.Range(15, 27);
            }
            int rand0 = Random.Range(1, 101);
            if(rand0 <= 10)
            {
                u1.element = Random.Range(1, 4);
            }
        }
        if (left.Length >= rand2)
        {
            Item u1 = Instantiate(left[rand2], point[1].position, point[1].rotation).GetComponent<Item>();
        }
    }
}
