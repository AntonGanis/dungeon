using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLIZKO : MonoBehaviour
{
    public List<GameObject> find;
    public GameObject closest;
    int kadr;
    GameObject FindClosest(List<GameObject> massiv)
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in massiv)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    void Update()
    {
        if(find.Count != 0)
        {
            closest = FindClosest(find);
            kadr++;
            if(kadr > 2)
            {
                find.Clear();
            }
        }
    }
}
