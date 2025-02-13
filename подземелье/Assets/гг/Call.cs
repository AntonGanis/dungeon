using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Call : MonoBehaviour
{
    public int I;
    public int colvo;
    public Text txt;
    int x;
    void Update()
    {
        if (x != colvo)
        {
            txt.text = ""+ colvo;
            x = colvo;
        }
    }
}
