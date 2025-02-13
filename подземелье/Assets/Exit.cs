using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject[] plars;
    public List<GameObject> plarsGo;
    public int lvl = 1;
    DontDestroy dnd;
    void Start()
    {
        dnd = FindObjectOfType<DontDestroy>();
    }

    void Update()
    {
        plars = GameObject.FindGameObjectsWithTag("Player");
        
        if(plars.Length == plarsGo.Count)
        {
            for (int i = 0; i < plarsGo.Count; i++)
            {
                plarsGo[i].GetComponent<Move>().exit = false;
            }
            dnd.Go(lvl);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Move>() && col.gameObject.GetComponent<Move>().exit == false)
        {
            col.gameObject.GetComponent<Move>().exit = true;
            plarsGo.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<Move>())
        {
            col.gameObject.GetComponent<Move>().exit = false;
            plarsGo.Remove(col.gameObject);
        }
    }
}
