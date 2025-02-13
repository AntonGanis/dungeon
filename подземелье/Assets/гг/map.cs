using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map : MonoBehaviour
{
    public Transform UI;
    public take tako;
    public GameObject interfac;
    public GameObject weapon;
    public Move plar;
    public MouseLook[] cam;
    public bool isLocked;
    bool open;

    public Image point;
    public List<Image> spic;
    float kadr;
    void Start()
    {
        gameObject.transform.parent = UI;
        gameObject.transform.localPosition = new Vector2(0, 0);
        SetCursorLock(true);
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
        if (tako != null)
        {
            UI = tako.UI;
            if(transform.parent != UI)
            {
                gameObject.transform.parent = UI;
                gameObject.transform.localPosition = new Vector2(0, 0);
            }
            if (Input.GetKeyUp(KeyCode.M))
            {
                SetCursorLock(!isLocked);
            }
            if (isLocked == false)
            {
                kadr += 1;
                if (Input.GetMouseButton(0) && kadr > 2)
                {
                    Vector3 mousePosition = Input.mousePosition;
                    if (RectTransformUtility.RectangleContainsScreenPoint(interfac.GetComponent<RectTransform>(), mousePosition))
                    {
                        Image newPoint = Instantiate(point, interfac.transform);
                        newPoint.transform.position = mousePosition;
                        spic.Add(newPoint);
                        kadr = 0;
                    }
                }
                else if (Input.GetMouseButton(1))
                {
                    Vector3 mousePosition = Input.mousePosition;
                    for (int i = spic.Count - 1; i >= 0; i--)
                    {
                        Image img = spic[i];
                        if (RectTransformUtility.RectangleContainsScreenPoint(img.GetComponent<RectTransform>(), mousePosition))
                        {
                            spic.RemoveAt(i);
                            Destroy(img.gameObject);
                            break;
                        }
                    }
                }
            }
        }
    }
}
