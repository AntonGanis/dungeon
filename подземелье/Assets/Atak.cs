using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atak : MonoBehaviour
{
    public take Take;
    public int valueDown;
    public bool plarWeapon;
    public bool Wall;
    public bool shield;
    public int blok;// 1 = блок  2 = оглуш   3 = враг на враг
    public int Stamina;
    void Start()
    {
        Take = FindObjectOfType<take>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<EnemyHealth>() && shield == false)
        {
            if (Take != null && Take.Element0 == col.gameObject.GetComponent<EnemyHealth>().TIP && Take.Element0 != 0)
            {
                col.GetComponent<EnemyHealth>().health -= valueDown / 3;
            }
            col.GetComponent<EnemyHealth>().health -= valueDown;
        }
        if (plarWeapon)
        {
            if (col.tag == "WALL")
            {
                Wall = true;
            }
        }
        if (shield)
        {
            if (col.gameObject.GetComponent<damage>() && col.gameObject.GetComponent<damage>().TIP == 0)
            {
                blok = 1;
            }
        }
    }
}