using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool weapon;
    public bool weapon_left;
    public int I;
    public int max;
    public int Regen;
    public float time;
    public int element; // 0 нет   1 огонь    2 лед     3 молния
    public GameObject[] runs;
    public float speed;
    public int ValueDown;
}
