using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullRoom : MonoBehaviour
{
    public float time;
    Room[] rooms;
    public int shance;

    public GameObject trap;

    void Update()
    {
        time += Time.deltaTime; 
        if (time > 4)
        {
            rooms = FindObjectsOfType<Room>();

            for (int i = 0; i < rooms.Length; i++)
            {
                int rand = Random.Range(0, shance-1);
                rooms[i].I = rand;
            }
            trap.SetActive(true);
            trap.GetComponent<BoxDop>().ID = shance;
            gameObject.SetActive(false);
        }
    }
}
