using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform UI;
    take tako;
    public Button but;
    public GameObject interfac;
    public GameObject weapon;
    public Move plar;
    public MouseLook[] cam;
    bool isLocked;
    public bool open;
    public Call[] SLON;

    void Start()
    {
        SetCursorLock(true);
        tako = FindObjectOfType<take>();
        interfac.transform.parent = UI;
        interfac.transform.localPosition = new Vector2(0, 0);
    }

    void SetCursorLock(bool isLocked)
    {
        this.isLocked = isLocked;
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;
        interfac.SetActive(!isLocked);
        weapon.SetActive(isLocked);
        cam[0].enabled = isLocked;
        cam[1].enabled = isLocked;
        plar.enabled = isLocked;
    }
    void Update()
    {
        if (open)
        {
            SetCursorLock(!isLocked);
            open = false;
        }
    }
    public void Tak()
    {
        tako.Take();
        SetCursorLock(!isLocked);
    }
    public void ico(Call col)
    {
        tako.iconss(col);
    }
}
