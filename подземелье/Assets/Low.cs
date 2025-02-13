using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Low : MonoBehaviour
{
    public Transform target;
    public float speed = 5;
    void LateUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }
}
