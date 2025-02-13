using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDop : MonoBehaviour
{
    public float time;
    public int danger;
    public int ID;
    public Danger[] dangers;
    public int id;
    public Box random;

    List<Transform> points;
    public int[] enemyTip;//0гуль   1слизень/улитка  2гриб
    public Transform[] enemy;//0гуль   1слизень   2улитка
    GameObject[] plars;
    void Start()
    {
        plars = GameObject.FindGameObjectsWithTag("Player");
    }
    void Update()
    {
        time += 0.1f;
        if (time > 3)
        {
            Room[] rooms = FindObjectsOfType<Room>();
            Box[] boxx = FindObjectsOfType<Box>();
            if (boxx.Length == 0)
            {
                int x = Random.Range(0, rooms.Length);
                rooms[x].I = 1;
                boxx = FindObjectsOfType<Box>();
            }
            int rand = Random.Range(0, boxx.Length);
            boxx[rand].rand = 0;
            random = boxx[rand];

            Traps[] trap = FindObjectsOfType<Traps>();
            for (int i = 0; i < trap.Length; i++)
            {
                rand = Random.Range(1, 101);
                if (rand <= danger)
                {
                    trap[i].enabled = true;
                    int rand2 = Random.Range(1, 4);
                    trap[i].I = rand2;
                }
            }


            dangers = FindObjectsOfType<Danger>();
            id = Find();
            dangers[id].rom.enabled = true;
            dangers[id].rom.I = ID - 1;

            GameObject[] pointArray = GameObject.FindGameObjectsWithTag("точка");
            points = new List<Transform>();
            foreach (GameObject point in pointArray)
            {
                points.Add(point.transform);
            }

            while (enemyTip[0] > 0)
            {
                rand = Random.Range(0, points.Count);
                breinGul guls = Instantiate(enemy[0], points[rand].position, points[rand].rotation).GetComponent<breinGul>();
                points.RemoveAt(rand);
                guls.n = Random.Range(3, 9);
                guls.N_gul = Random.Range(2, 6);
                int m = Random.Range(0, 3);
                if(m == 0) { guls.N_Algul = true; }
                enemyTip[0]--;
            }

            while (enemyTip[1] > 0)
            {
                rand = Random.Range(0, points.Count);
                int m = Random.Range(0, 2);
                if (m == 0)
                {
                    Instantiate(enemy[1], points[rand].position, points[rand].rotation);
                }
                else
                {
                    Instantiate(enemy[2], points[rand].position, points[rand].rotation);
                }
                enemyTip[1]--;
            }

            GameObject[] Mushroom = GameObject.FindGameObjectsWithTag("грибница");
            for (int i = 0; i < Mushroom.Length; i++)
            {
                Mushroom[i].SetActive(false);
            }
            while (enemyTip[2] > 0)
            {
                rand = Random.Range(0, Mushroom.Length);
                Mushroom[rand].SetActive(true);
                enemyTip[2]--;
            }

            for (int i = 0; i < plars.Length; i++)
            {
                plars[i].GetComponent<Move>().enabled = true;
                plars[i].GetComponent<Stats>().enabled = true;
            }

             gameObject.SetActive(false);
        }
    }
    int Find()
    {
        int farthestIndex = 0;
        float maxDistance = 0f;

        for (int i = 0; i < dangers.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, dangers[i].transform.position);
            if (distance > maxDistance && dangers[i].rom.I != 1)
            {
                maxDistance = distance;
                farthestIndex = i;
            }
        }
        return farthestIndex;
    }
}
