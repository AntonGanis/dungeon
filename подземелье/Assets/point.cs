using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point : MonoBehaviour
{
    float x;
    public Transform[] other;//0стена    1фейк стена      2дверь

    void Update()
    {
        x += 0.1f;
        if (x > 9)
        {
            if(gameObject.transform.childCount == 0)
            {
                Instantiate(other[0], gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
                Destroy(gameObject.GetComponent<point>());
            }
            else if(gameObject.transform.childCount != 0 && gameObject.transform.GetChild(0).transform.name != "стена(Clone)")
            {
                int rand = Random.Range(1, 101);
                if(rand < 25)
                {
                    Instantiate(other[1], gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
                }
                else if(rand < 50)
                {
                    Instantiate(other[2], gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
                }
                Destroy(gameObject.GetComponent<point>());
            }

        }
    }
}
