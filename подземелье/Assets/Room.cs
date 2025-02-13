using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int I = -1;
    public GameObject[] room;
    void Update()
    {
        if(I != -1)
        {
            room[I].SetActive(true);
            gameObject.GetComponent<Room>().enabled = false;
        }
        else if(I == room.Length)
        {
            for (int i = 0; i < room.Length - 1; i++)
            {
                room[i].SetActive(false);
            }
            room[I].SetActive(true);
        }
    }
}
