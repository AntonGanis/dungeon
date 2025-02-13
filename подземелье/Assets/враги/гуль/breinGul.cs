using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breinGul : MonoBehaviour
{
    public int n;
    public Transform gul;
    public int N_gul;

    public Transform Algul;
    public Gul Algul1;
    public bool N_Algul;

    public List<Transform> nearestPoints = new List<Transform>();
    public List<Gul> Stai;
    public int point;
    int kadr;
    float time;
    void Start()
    {
        FindNearestPoints();
        for (int i = 0; i < N_gul; i++)
        {
            Gul gg = Instantiate(gul, transform.position, transform.rotation).GetComponent<Gul>();
            gg.pointFood = transform;
            Stai.Add(gg);
        }
        if (N_Algul == true)
        {
            Algul1 = Instantiate(Algul, transform.position, transform.rotation).GetComponent<Gul>();
            Algul1.pointFood = transform;
            Stai.Add(Algul1);

            for(int i = 0;i < N_gul;i++)
            {
                Algul1.guls.Add(Stai[i]);
            }
        }
        point = Random.Range(0, nearestPoints.Count);
    }
    void Update()
    {
        if (kadr == 10)
        {
            int x = 0;
            for (int i = 0; i < Stai.Count; i++)
            {
                if (Stai[i] != null)
                {
                    if (Stai[i].pointMain != nearestPoints[point])
                    {
                        Stai[i].pointMain = nearestPoints[point];
                    }
                    else
                    {
                        if (Stai[i].konec == true)
                        {
                            x++;
                        }
                    }
                }
                else
                {
                    Stai.RemoveAt(i);
                }
            }
            if(x == Stai.Count)
            {
                time += Time.deltaTime;
                if(time > 3)
                {
                    point = Random.Range(0, nearestPoints.Count);
                    for (int i = 0; i < Stai.Count; i++)
                    {
                        Stai[i].konec = false;
                    }
                     time = 0;
                }
            }
            kadr = 0;
        }
        kadr++;
        if(Stai.Count == 0)
        {
            Destroy(gameObject);
        }
    }
    void FindNearestPoints()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("точка");

        List<KeyValuePair<Transform, float>> distances = new List<KeyValuePair<Transform, float>>();

        foreach (GameObject point in points)
        {
            float distance = Vector3.Distance(transform.position, point.transform.position);
            distances.Add(new KeyValuePair<Transform, float>(point.transform, distance));
        }

        distances.Sort((a, b) => a.Value.CompareTo(b.Value));

        nearestPoints.Clear();
        for (int i = 0; i < Mathf.Min(n, distances.Count); i++)
        {
            nearestPoints.Add(distances[i].Key);
        }
    }
}