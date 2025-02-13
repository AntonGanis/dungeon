using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Transform[] items;
    public Transform broke;
    public int rand;
    void Start() 
    {
        rand = Random.Range(1, items.Length);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Atak>() || col.gameObject.GetComponent<damage>())
        {
            Instantiate(broke, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(items[rand], gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
