using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public int I;
    int rand;

    int trap1;
    public GameObject[] corpse;
    List<Transform> spisoc;
    public Vector3[] trap1_point;

    public GameObject[] fire;

    public GameObject[] trap3;

    void Update()
    {
        if(I == 1)
        {
            trap1 = Random.Range(0, trap1_point.Length/2);

            for(int i = 0; i < trap1; i++)
            {
                rand = Random.Range(0, trap1_point.Length);
                int rand2 = Random.Range(0, 3);

                Transform u1 = Instantiate(corpse[rand2]).GetComponent<Transform>();
                u1.transform.parent = transform;
                u1.transform.localPosition = trap1_point[rand];

                RemoveElementAt(ref trap1_point, rand);
            }
            gameObject.GetComponent<Traps>().enabled = false;
        }
        else if(I == 2)
        {
            if (fire.Length != 1)
            {
                rand = Random.Range(0, fire.Length);
                fire[rand].SetActive(true);
            }
            else
            {
                fire[0].SetActive(true);
            }
            gameObject.GetComponent<Traps>().enabled = false;
        }
        else if (I == 3)
        {
            trap3[0].SetActive(true);
            int rand = Random.Range(0, trap1_point.Length);
            trap3[1].transform.localPosition = trap1_point[rand];
            gameObject.GetComponent<Traps>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Traps>().enabled = false;
        }
    }
    void RemoveElementAt(ref Vector3[] array, int index)
    {
        Vector3[] newArray = new Vector3[array.Length - 1];

        for (int i = 0, j = 0; i < array.Length; i++)
        {
            if (i != index)
            {
                newArray[j++] = array[i];
            }
        }

        array = newArray;
    }
}
