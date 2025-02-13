using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    public int TIP;
    public int power;
    int rand;
    int rand2;
    public Transform[] room;
    public Transform[] point1;
    public Transform point2;
    public bool end;
    public Danger test;
    public int O;
    public float t;

    public Room rom;
    void Start()
    {
        if(TIP == -1)
        {
            power = Data.power;
        }
        if (power > 0)
        {
            if (point1.Length == 1)
            {
                rand = Random.Range(0, room.Length);
                test = Instantiate(room[rand], point1[0].position, point1[0].rotation, point1[0]).GetComponent<Danger>();
                test.power = power - 1;
                test.point2 = gameObject.transform;
                test.O = 0;
                gameObject.GetComponent<Danger>().enabled = false;
            }
        }
        else
        {
            gameObject.GetComponent<Danger>().enabled = false;
        }
    }
    void Update()
    {
        t += 0.1f;
        int y = 0;
        for (int i = 0; i != point1.Length; i++)
        {
            if (point1[i].childCount > 0) { y++; }
        }
            if (y == point1.Length)
            {
                end = true;
            }
        if (end == false)
        {
            rand2 = Random.Range(0, point1.Length);
            if (point1[rand2].childCount == 0)
            {
                rand = Random.Range(0, room.Length);
                test = Instantiate(room[rand], point1[rand2].position, point1[rand2].rotation, point1[rand2]).GetComponent<Danger>();
                test.power = power - 1;
                test.point2 = gameObject.transform;
                test.O = rand2;
            }
        }
        if (t > 5)
        {
            gameObject.GetComponent<Danger>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Danger>())
        {
            if (col.gameObject.transform != transform.parent && col.gameObject.transform.parent != gameObject.transform)
            {
                if (col.gameObject.GetComponent<Danger>().power >= power)
                {
                    //Instantiate(Wall1, gameObject.transform.position, gameObject.transform.rotation, transform.parent);
                    //gameObject.transform.parent.GetComponent<Danger>().full[O] = false;
                    Destroy(gameObject);

                }
            }
        }
    }
}
