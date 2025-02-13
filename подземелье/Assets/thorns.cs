using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thorns : MonoBehaviour
{
    public bool kill;
    public int I;
    public GameObject fire;
    void Start()
    {
        I = Random.Range(3, 14);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Stats>() && I > 0)
        {
            kill = true;
            gameObject.GetComponent<Animator>().SetTrigger("атака");
            I--;
            if (I == 0 && fire != null)
            {
                Instantiate(fire, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
        }
        if (col.gameObject.GetComponent<Atak>() && fire != null)
        {
            Instantiate(fire, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
