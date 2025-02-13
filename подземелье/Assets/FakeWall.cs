using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWall : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Atak>())
        {
            Destroy(gameObject);
        }
    }
}
