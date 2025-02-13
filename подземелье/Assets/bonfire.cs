using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonfire : MonoBehaviour
{
    Stats plar;
    float dist;
    public float rest;
    public float time0;
    public GameObject fire;
    GameObject item;
    public bool gorit;
    bool it;
    bool at;
    public List<GameObject> food;
    public List<float> time;
    public float[] gotovo; // 0-м€со    1-гул€ш     2-гриб
    public GameObject[] done;// 0-м€со    1-гул€ш     2-гриб
    float timeGorit;

    void Start()
    {
        plar = FindObjectOfType<Stats>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Item>())
        {
            if(col.gameObject.GetComponent<Item>().I == 7)
            {
                it = true;
                item = col.gameObject;
            }
            else if (col.gameObject.GetComponent<Item>().I == 13 || col.gameObject.GetComponent<Item>().I == 12 || col.gameObject.GetComponent<Item>().I == 4)
            {
                food.Add(col.gameObject);
                time.Add(0);
            }
        }
        if (col.gameObject.GetComponent<Atak>())
        {
            at = true;
        }
        if (at && it || (col.gameObject.GetComponent<Atak>() && col.gameObject.GetComponent<Atak>().Take.Element0 == 1))
        {
            fire.SetActive(true);
            gorit = true;
            Destroy(item);
        }
        if (col.gameObject.GetComponent<Atak>() && col.gameObject.GetComponent<Atak>().Take.Element0 == 2)
        {
            gorit = false;
            timeGorit = 0;
            fire.SetActive(false);
        }

    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<Item>())
        {
            if(col.gameObject.GetComponent<Item>().I == 7)
            {
                it = false;
                item = null;
            }
            else if(col.gameObject.GetComponent<Item>().I == 13 || col.gameObject.GetComponent<Item>().I == 12 || col.gameObject.GetComponent<Item>().I == 4)
            {
                time.Remove(food.IndexOf(col.gameObject));
                food.Remove(col.gameObject);
            }
        }
        if (col.gameObject.GetComponent<Atak>())
        {
            at = false;
        }
    }
    void Update()
    {
        if (gorit)
        {
            timeGorit += Time.deltaTime;
            if (timeGorit > 300)
            {
                gorit = false;
                at = false;
                it = false;
                timeGorit = 0;
                fire.SetActive(false);
            }
            for (int i = 0; i < food.Count; i++)
            {
                if (food[i] == null)
                {
                    food.RemoveAt(i);
                    time.RemoveAt(i);
                }
            }
            dist = Vector3.Distance(plar.transform.position, transform.position);
            if(dist < 5)
            {
                time0 += Time.deltaTime;
                if(time0 > rest)
                {
                    plar.maxHealth += 1;
                    plar.maxStamina += 1;
                }
            }
            else
            {
                time0 = 0;
            }
            if (food.Count > 0)
            {
                for(int i = 0; i < food.Count; i++)
                {
                    if (food[i].GetComponent<Item>().I == 13)
                    {
                        Cooking(i, 0);
                    }
                    else if (food[i].GetComponent<Item>().I == 12)
                    {
                        Cooking(i, 1);
                    }
                    else if(food[i].GetComponent<Item>().I == 4)
                    {
                        Cooking(i, 2);
                    }
                }
            }
        }
    }
    void Cooking(int i, int x)
    {
        if (time[i] < gotovo[x])
        {
            time[i] += Time.deltaTime;
        }
        else
        {
            Instantiate(done[x], food[i].transform.position, food[i].transform.rotation);
            Destroy(food[i]);
            food.RemoveAt(i);
            time.RemoveAt(i);
        }
    }
}
