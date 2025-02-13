using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int health2;
    int healthMax;
    public int regen;
    float time;
    public int TIP;//0 ничего    1 огонь    2 лед     3 молния
    public Transform Dead;
    public GameObject[] body;
    public GameObject[] point;
    public Transform backpack;
    public bool trup;
    
    void Start()
    {
        healthMax = health;
    }
    void Update()
    {
        if (regen != 0)
        {
            time += Time.deltaTime;
        }
        if(time > 5)
        {
            health += regen;
            health2 += regen;
            time = 0;
        }
        if (health > healthMax)
        {
            health = healthMax;
            health2 = healthMax;
        }
        if(health2 != health)
        {
            if (gameObject.GetComponent<Snail>())
            {
                gameObject.GetComponent<Snail>().run = true;
            }
        }
        if (health <= 0)
        {
            Trup trup = Instantiate(Dead, gameObject.transform.position, gameObject.transform.rotation).GetComponent<Trup>();
            if (body.Length != 0)
            {
                for (int i = 0; i < body.Length; i++)
                {
                    if (body[i] == null)
                    {
                        trup.body[i].SetActive(false);
                    }
                }
            }
            if (point.Length != 0)
            {
                for (int i = 0; i < point.Length; i++)
                {
                    if (point[i] == null || point[i].activeSelf == false)
                    {
                        trup.point[i].SetActive(false);
                    }
                }
            }
            if(backpack != null)
            {
                backpack.parent = null;
            }
            Destroy(gameObject);
        }
        health2 = health;
    }
}
