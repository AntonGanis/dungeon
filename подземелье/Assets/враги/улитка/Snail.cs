using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public bool snail;
    public GameObject[] item;
    public GameObject shell;
    int DopXp;
    public float dist0;
    public float dist;
    bool begi;
    Transform po;

    Animator ani;
    Transform plar;
    BLIZKO Findplar;
    UnityEngine.AI.NavMeshAgent agent;
    Transform[] points;
    public float reachThreshold = 0.5f;
    int currentPointIndex;

    public int rays = 8;
    public int distance = 33;
    public float angle = 40;
    public Transform offset;

    int kadr;

    bool animationPlayed;
    bool anim;
    public bool run;
    public float limit;
    public float time;
    public float _speed;

    public Low shuta;

    public EnemyHealth[] eyes;

    void Start()
    {
        Findplar = gameObject.GetComponent<BLIZKO>();
        ani = gameObject.GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GameObject[] pointObjects = GameObject.FindGameObjectsWithTag("точка");
        points = new Transform[pointObjects.Length];
        for (int i = 0; i < pointObjects.Length; i++)
        {
            points[i] = pointObjects[i].transform;
        }
        SetNewDestination();
        anim = true;
        _speed = agent.speed;

        if (snail)
        {
            shell.SetActive(true);
            DopXp += 80;
        }
        else
        {
            for (int i = 0; i < item.Length; i++)
            {
                int rand = Random.Range(0, 2);
                if(rand == 1)
                {
                    item[i].SetActive(true);
                    DopXp += 5;
                }

            }
        }
        gameObject.GetComponent<EnemyHealth>().health += DopXp;
        gameObject.GetComponent<EnemyHealth>().health2 += DopXp;
    }
    void Update()
    {
        int minus=0;
        kadr += 1;
        if (kadr > 7)
        {
            List<take> plars = new List<take>(FindObjectsOfType<take>());
            List<Mushroom> mushrooms = new List<Mushroom>(FindObjectsOfType<Mushroom>());
            foreach (var mushroom in mushrooms)
            {
                Findplar.find.Add(mushroom.gameObject);
            }
            foreach (var plar in plars)
            {
                Findplar.find.Add(plar.gameObject);
            }
            plar = Findplar.closest.transform;


            for (int i = 0; i < eyes.Length; i++)
            {
                if (eyes[i] != null) { minus++; }
            }
            int x = RayToScan();
            if (x != 0 || run)
            {
                run = true;
                if (x == 2 && begi == false)
                {
                    begi = true;
                    po = points[Find()];
                }
                else if (x != 2)
                {
                    dist0 = Vector3.Distance(transform.position, plar.position);
                    if (dist0 < dist && snail)
                    {
                        ani.SetInteger("действие", 5); // прятаться
                        agent.speed = 0;
                    }
                    else
                    {
                        shuta.target = plar;
                        agent.speed = _speed;
                        ani.SetInteger("действие", 4); // атака
                        agent.SetDestination(plar.position);
                    }
                }
                else if (x == 3)
                {
                    agent.speed = _speed;
                    agent.SetDestination(plar.position);

                }
            }
            else
            {
                if (agent.remainingDistance <= reachThreshold && !agent.pathPending)
                {
                    int rand = Random.Range(1, 101);
                    if (animationPlayed == false) 
                    {
                        if (rand > 20)
                        {
                            ani.SetInteger("действие", 1); // стоит
                        }
                        else
                        {
                            ani.SetInteger("действие", 2); // спать
                        }
                        animationPlayed = true; 
                    }
                    agent.speed = 0;
                }
            }
            kadr = 0;
        }
        if(begi == true)
        {
            agent.speed = _speed;
            shuta.target = po;
            ani.SetInteger("действие", 0); // прятаться
        }
        if (run)
        {
            time += Time.deltaTime;
            if ((time > limit && minus == 2) || (time > limit/2 && minus == 1) || (time > limit/3 && minus == 0))
            {
                SetNewDestination();
                run = false;
                begi = false;
                time = 0;
            }
        }
    }
    void SetNewDestination()
    {
        ani.SetInteger("действие", 0);// иди
        agent.speed = _speed;
        int newPointIndex;
        do
        {
            newPointIndex = Random.Range(0, points.Length);
        } while (newPointIndex == currentPointIndex);

        currentPointIndex = newPointIndex;
        agent.SetDestination(points[currentPointIndex].position);
    }


    int GetRaycast(Vector3 dir)
    {
        int result = 0;//0ничего     1игрок      2гуль    3гриб
        RaycastHit hit = new RaycastHit();
        Vector3 pos = offset.position;
        if (Physics.Raycast(pos, dir, out hit, distance))
        {
            if (hit.collider.GetComponent<Move>())
            {
                result = 1;
                Debug.DrawLine(pos, hit.point, Color.green);
            }
            else if (hit.collider.GetComponent<Gul>())
            {
                result = 2;
                Debug.DrawLine(pos, hit.point, Color.black);
            }
            else
            {
                Debug.DrawLine(pos, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(pos, dir * distance, Color.red);
        }
        return result;
    }
    int RayToScan()
    {
        int a = 0;
        int b = 0;
        float j = 0;
        for (int i = 0; i < rays; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += angle * Mathf.Deg2Rad / rays;

            Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
            if (GetRaycast(dir) == 1)
            {
                a = 1;
            }
            else if(GetRaycast(dir) == 2)
            {
                a = 2;
            }
            else if (GetRaycast(dir) == 3)
            {
                a = 3;
            }
            else
                    {
                a = 0;
            }

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, 0, y));
                if (GetRaycast(dir) == 1)
                {
                    b = 1;
                }
                else if (GetRaycast(dir) == 2)
                {
                    b = 2;
                }
                else if (GetRaycast(dir) == 3)
                {
                    b = 3;
                }
                else
                {
                    b = 0;
                }
            }
        }

        if (a == 1 || b == 1)
        {
            return 1;
        }
        else if (a == 2 || b == 2)
        {
            return 2;
        }
        else if (a == 3 || b == 3)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Item>() && (col.gameObject.GetComponent<Item>().I == 4 || col.gameObject.GetComponent<Item>().I == 14))
        {
            ani.SetInteger("действие", 3); // ест
            agent.speed = 0;
            run = false;
            time = 0;
            gameObject.GetComponent<EnemyHealth>().health += 20;
            gameObject.GetComponent<EnemyHealth>().health2 += 20;
            Destroy(col.gameObject);
        }
        if (col.gameObject.GetComponent<Mushroom>())
        {
            ani.SetInteger("действие", 3); // ест
            agent.speed = 0;
            run = false;
            time = 0;
            gameObject.GetComponent<EnemyHealth>().health += 20;
            gameObject.GetComponent<EnemyHealth>().health2 += 20;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.GetComponent<Mushroom>())
        {
            col.gameObject.GetComponent<EnemyHealth>().health -= 1;
        }
    }


    public void ANIME()
    {
        animationPlayed = false;
        anim = false;
        SetNewDestination();
    }


    int Find()
    {
        int farthestIndex = 0;
        float maxDistance = 0f;

        for (int i = 0; i < points.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, points[i].position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestIndex = i;
            }
        }
        return farthestIndex;
    }
}
