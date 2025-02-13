using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiGul : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public float Distance;
    public float Limit;
    Transform[] points;
    int rand;
    int kadr;

    BLIZKO Find;
    public Transform point;
    public Transform pointFood;
    Transform Object;

    Animator ani;
    public float Speed;
    public int stayFind; //0ищет по запаху   1 стоит и нюхает    2 идет к стае
    public float time;

    public Transform eatPoint;
    public Rigidbody food;
    void Start()
    {
        point.parent = null;
        ani = gameObject.GetComponent<Animator>();
        Find = gameObject.GetComponent<BLIZKO>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Speed = agent.speed;

        GameObject[] pointObjects = GameObject.FindGameObjectsWithTag("точка");
        points = new Transform[pointObjects.Length];
        for (int i = 0; i < pointObjects.Length; i++)
        {
            points[i] = pointObjects[i].transform;
        }
        rand = Random.Range(0, points.Length);
    }

    void Update()
    {
        if(kadr > 10)
        {
            if (food == null)
            {
                List<Item> items = new List<Item>(FindObjectsOfType<Item>());
                List<take> plars = new List<take>(FindObjectsOfType<take>());
                List<Snail> snails = new List<Snail>(FindObjectsOfType<Snail>());
                List<Gul> guls = new List<Gul>(FindObjectsOfType<Gul>());
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].weapon == false && (items[i].I == 13 || items[i].I == 1 || items[i].I == 12 || items[i].I == 2))
                    {
                        Find.find.Add(items[i].gameObject);
                    }
                }
                foreach (var plar in plars)
                {
                    Find.find.Add(plar.gameObject);
                }
                foreach (var gul in guls)
                {
                    Find.find.Add(gul.gameObject);
                }
                foreach (var snail in snails)
                {
                    Find.find.Add(snail.gameObject);
                }
                if (Find.closest.transform != null)
                {
                    Object = Find.closest.transform;
                    Distance = Vector3.Distance(transform.position, Object.position);

                    if (Distance < Limit)
                    {
                        if (stayFind == 2 || stayFind == 1)
                        {
                            End();
                            stayFind = 0;
                        }
                        if (Object.GetComponent<take>() && Object.GetComponent<take>().zapah && Distance < Limit / 3)
                        {
                            point.position = Object.position;
                        }
                        else if ((Object.GetComponent<take>() == null) || (Object.GetComponent<take>() && Object.GetComponent<take>().zapah == false))
                        {
                            point.position = Object.position;
                        }
                        else
                        {
                            stayFind = 1;
                        }
                        agent.SetDestination(point.position);
                    }
                    else
                    {
                        if (stayFind != 2)
                        {
                            stayFind = 1;
                        }
                    }
                }
                if (stayFind == 1)
                {
                    time += Time.deltaTime;
                    ani.SetInteger("действие", 3); // смотрит
                    if (time > 1.2f)
                    {
                        time = 0;
                        stayFind = 2;
                    }
                }
                else if (stayFind == 2)
                {
                    Object = null;
                    End();
                    agent.SetDestination(points[rand].position);
                    Distance = Vector3.Distance(transform.position, points[rand].position);
                    if (Distance < 1)
                    {
                        ani.SetInteger("действие", 3); // смотрит
                        time += Time.deltaTime;
                        agent.speed = 0;
                        if(time > 2)
                        {
                            End();
                            rand = Random.Range(0, points.Length);
                            time = 0;
                        }
                    }
                }
            }
            else
            {
                End();
                agent.SetDestination(pointFood.position);
                Distance = Vector3.Distance(transform.position, pointFood.position);
                if (Distance < 2)
                {
                    food.isKinematic = false;
                    food.transform.parent = null;
                    gameObject.GetComponent<EnemyHealth>().backpack = null;
                    food = null;
                }
            }
            kadr = 0;
        }
        kadr++;
    }
    public void End()
    {
        agent.speed = Speed;
        ani.SetInteger("действие", 0);// иди
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Item>() && (col.gameObject.GetComponent<Item>().I == 13 || col.gameObject.GetComponent<Item>().I == 1 || col.gameObject.GetComponent<Item>().I == 12 || col.gameObject.GetComponent<Item>().I == 2) && col.gameObject.GetComponent<Item>().weapon == false)
        {
            ani.SetInteger("действие", 2); // ест
            agent.speed = 0;
            gameObject.GetComponent<EnemyHealth>().health += 20;
            gameObject.GetComponent<EnemyHealth>().health2 += 20;
            Object = null;
            Destroy(col.gameObject);
        }
        else if (col.gameObject.GetComponent<Move>() || col.gameObject.GetComponent<Snail>() || col.gameObject.GetComponent<Gul>())
        {
            ani.SetInteger("действие", 1); // атака
            agent.speed = 0;
        }
        else if (col.gameObject.GetComponent<EnemyHealth>() && col.gameObject.GetComponent<EnemyHealth>().trup == true)
        {
            food = col.gameObject.GetComponent<Rigidbody>();
            food.isKinematic = true;
            food.transform.position = eatPoint.position;
            food.transform.parent = eatPoint;
            food.GetComponent<EnemyHealth>().trup = false;
        }
        else if (col.gameObject.GetComponent<Inventory>() && col.gameObject.GetComponent<Inventory>().weapon == null)
        {
            food = col.gameObject.GetComponent<Rigidbody>();
            food.isKinematic = true;
            food.transform.position = eatPoint.position;
            food.transform.parent = eatPoint;
            gameObject.GetComponent<EnemyHealth>().backpack = food.transform;
            food.GetComponent<Inventory>().weapon = eatPoint.gameObject;
        }
    }

}
