using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    public GameObject[] itemDone;//0вода    1отпугиватель   2противо€дие
    public bool ok;
    public List<GameObject> ingridient;
    public int[] kolvo;//0-гриб    1-гул€ш     2-хлеб

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Item>())
        {
            if ((col.gameObject.GetComponent<Item>().I == 3 || col.gameObject.GetComponent<Item>().I == 8) && ok == false)
            {
                GameObject er = Instantiate(itemDone[0], col.gameObject.transform.position, col.gameObject.transform.rotation);
                Destroy(col.gameObject);
                ingridient.Add(er);
                ok = true;
            }
            else if (col.gameObject.GetComponent<Item>().I == 4)
            {
                ingridient.Add(col.gameObject);
                kolvo[0] += 1;
            }
            else if (col.gameObject.GetComponent<Item>().I == 12)
            {
                ingridient.Add(col.gameObject);
                kolvo[1] += 1;
            }
            else if (col.gameObject.GetComponent<Item>().I == 0)
            {
                ingridient.Add(col.gameObject);
                kolvo[2] += 1;
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<Item>())
        {
            if (col.gameObject.GetComponent<Item>().I == 3 || col.gameObject.GetComponent<Item>().I == 8)
            {
                ok = false;
                ingridient.Remove(col.gameObject);
            }
            else if (col.gameObject.GetComponent<Item>().I == 4)
            {
                ingridient.Remove(col.gameObject);
                kolvo[0] -= 1;
            }
            else if (col.gameObject.GetComponent<Item>().I == 12)
            {
                ingridient.Remove(col.gameObject);
                kolvo[1] -= 1;
            }
            else if (col.gameObject.GetComponent<Item>().I == 0)
            {
                ingridient.Remove(col.gameObject);
                kolvo[2] -= 1;
            }
        }
    }
    void Update()
    {
        if (ok)
        {
            int x=0;
            for (int i = 0; i < ingridient.Count; i++)
            {
                if (ingridient[i]  == null)
                {
                    ingridient.RemoveAt(i);
                    break;
                }
                if (ingridient[i].GetComponent<Item>().I == 3 || ingridient[i].GetComponent<Item>().I == 8)
                {
                    x += 1;
                }
            }
            if (x == 0)
            {
                ok = false;
            }
            if (kolvo[0] > 0 && kolvo[1] > 0)
            {
                for (int i = 0; i < ingridient.Count; i++)
                {
                    if (ingridient[i].GetComponent<Item>().I == 4)
                    {
                        Remo(i);
                    }
                    else if (ingridient[i].GetComponent<Item>().I == 12)
                    {
                        Remo(i);
                    }
                    else if(ingridient[i].GetComponent<Item>().I == 3 || ingridient[i].GetComponent<Item>().I == 8)
                    {
                        Instantiate(itemDone[1], ingridient[i].transform.position, ingridient[i].transform.rotation);
                        Remo(i);
                    }
                }
            }
            if (kolvo[0] > 0 && kolvo[2] > 0)
            {
                for (int i = 0; i < ingridient.Count; i++)
                {
                    if (ingridient[i].GetComponent<Item>().I == 4)
                    {
                        Remo(i);
                    }
                    else if (ingridient[i].GetComponent<Item>().I == 0)
                    {
                        Remo(i);
                    }
                    else if (ingridient[i].GetComponent<Item>().I == 3 || ingridient[i].GetComponent<Item>().I == 8)
                    {
                        Instantiate(itemDone[2], ingridient[i].transform.position, ingridient[i].transform.rotation);
                        Remo(i);
                    }
                }
            }
        }
    }
    void Remo(int i)
    {
        Destroy(ingridient[i]);
        ingridient.RemoveAt(i);
    }
}
