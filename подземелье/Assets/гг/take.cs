using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class take : MonoBehaviour
{
    public Transform UI;

    public GameObject plar;
    public Inventory interfac;
    Inventory interfac0;
    public Call[] calls;
    public GameObject[] icons;
    public GameObject[] items;

    public GameObject[] weapon1;
    public GameObject[] weapon2;
    GameObject weapon_don1;
    GameObject weapon_don2;

    public int Element0;
    public float Speed;
    public int Damag;

    public Transform botle;

    public Transform Map;

    public GameObject[] off;

    public bool zapah;
    public float time;
    public float predel;


    void Start()
    {
        int y = interfac.GetComponent<Inventory>().SLON.Length;
        calls = new Call[y];
        for (int i = 0; i < y; i++)
        {
            calls[i] = interfac.GetComponent<Inventory>().SLON[i];
        }

    }
    void Update()
    {
        if (zapah)
        {
            time += Time.deltaTime;
            if(time > predel)
            {
                time = 0;
                zapah = false;
            }
        }
        if (interfac != null && interfac.transform.parent != transform.parent)
        {
            interfac = null;
            calls = null;
        }
        if (Input.GetMouseButton(2) && interfac != null)
        {
            interfac.transform.localPosition = new Vector3(0, 0, 1);
            interfac.GetComponent<Rigidbody>().isKinematic = false;
            interfac.GetComponent<BoxCollider>().enabled = true;
            interfac.transform.parent = null;
            calls = null;
            interfac.weapon = null;
            interfac.cam[0] = null;
            interfac.cam[1] = null;
            interfac.plar = null;
            interfac = null;
        }
        if (Input.GetKey(KeyCode.F))
        {
            if (Input.GetMouseButton(0) && weapon_don1 != null)
            {
                weapon_don1.transform.localPosition = new Vector3(0, 0, 1.5f);
                weapon_don1.transform.localRotation = Quaternion.Euler(0, 0, 0);
                weapon_don1.transform.parent = null;
                weapon_don1.SetActive(true);
                if (weapon_don1.GetComponent<Item>().element != 0)
                {
                    weapon_don1.GetComponent<Item>().runs[Element0 - 1].SetActive(true);
                }
                weapon_don1 = null;
                weapon1[0].SetActive(false);
                weapon1[1].SetActive(false);
                weapon1[2].SetActive(false);
            }
            else if (Input.GetMouseButton(1) && weapon_don2 != null)
            {
                weapon_don2.transform.localPosition = new Vector3(0, 0, 1.5f);
                weapon_don2.transform.localRotation = Quaternion.Euler(0, 0, 0);
                weapon_don2.transform.parent = null;
                weapon_don2.SetActive(true);
                weapon_don2 = null;
                weapon2[0].SetActive(false);
                weapon2[1].SetActive(false);
            }
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2))
            {
                if (hit.collider.GetComponent<Item>())
                {
                    Item it = hit.collider.GetComponent<Item>();
                    if (it.weapon)
                    {
                        if(weapon_don1 == null && it.weapon_left == false)
                        {
                            weapon1[it.I].SetActive(true);
                            weapon_don1 = hit.collider.gameObject;
                            weapon_don1.transform.parent = transform;
                            weapon_don1.SetActive(false);
                            Element0 = it.element;
                            Speed = it.speed;
                            Damag = it.ValueDown;
                        }
                        if (weapon_don2 == null && it.weapon_left)
                        {
                            weapon2[it.I].SetActive(true);
                            weapon_don2 = hit.collider.gameObject;
                            weapon_don2.transform.parent = transform;
                            weapon_don2.SetActive(false);

                        }
                    }
                    else
                    {
                        if(it.I == 5)
                        {
                            plar.GetComponent<Move>().key = true;
                        }
                        else if (it.I == 18)
                        {
                            map OOO = it.runs[0].GetComponent<map>();
                            OOO.tako = gameObject.GetComponent<take>();
                            OOO.weapon = off[0];
                            OOO.cam[0] = off[1].GetComponent<MouseLook>();
                            OOO.cam[1] = off[2].GetComponent<MouseLook>();
                            OOO.plar = off[3].GetComponent<Move>();
                            Map = it.GetComponent<Transform>();
                            Map.parent = transform;
                            Map.gameObject.SetActive(false);
                        }
                        for (int i = 0; i < calls.Length; i++)
                        {
                            if (calls[i].I == -1)
                            {
                                GameObject UI = Instantiate(icons[it.I]);
                                UI.transform.parent = calls[i].transform;
                                UI.transform.localPosition = new Vector2(0, 0);
                                calls[i].I = it.I;
                                calls[i].colvo += 1;
                                if (it.I != 18)
                                {
                                    Destroy(hit.collider.gameObject);
                                }
                                break;
                            }
                            else if (calls[i].I == it.I && it.max != calls[i].colvo)
                            {
                                calls[i].colvo += 1;
                                Destroy(hit.collider.gameObject);
                                break;
                            }
                        }
                    }
                }
                else if (hit.collider.GetComponent<Inventory>() && interfac == null)
                {
                    interfac0 = hit.collider.GetComponent<Inventory>();
                    interfac0.open = true;
                    interfac0.weapon = off[0];
                    interfac0.cam[0] = off[1].GetComponent<MouseLook>();
                    interfac0.cam[1] = off[2].GetComponent<MouseLook>();
                    interfac0.plar = off[3].GetComponent<Move>();
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2))
            {
                if (hit.collider.GetComponent<Item>() && hit.collider.GetComponent<Item>().weapon == false)
                {
                    Item it = hit.collider.GetComponent<Item>();
                    if (it.I == 9 || it.I == 8 || it.I == 11 || it.I == 10)
                    {
                        if (it.I == 9)
                        {
                            plar.GetComponent<Stats>().Health += 20;
                            plar.GetComponent<Stats>().wounds += 40;
                            plar.GetComponent<Stats>().maxHealth += 20;
                        }
                        else if(it.I == 10 && plar.GetComponent<Stats>().Regen < 0)
                        {
                            plar.GetComponent<Stats>().Regen = 0;
                        }
                        else if (it.I == 11)
                        {
                            zapah = true;
                            time -= 40;
                        }
                        Instantiate(botle, hit.collider.transform.position, hit.collider.transform.rotation);
                        Destroy(hit.collider.gameObject);
                    }
                    else if (it.I == 4 || it.I == 13 || it.I == 12 || it.I == 2 || it.I == 0 || it.I == 14 || it.I == 1)
                    {
                        plar.GetComponent<Stats>().Regen += it.Regen;
                        plar.GetComponent<Stats>().RegrnTime += it.time;
                        plar.GetComponent<Stats>().RegrnTime2 = plar.GetComponent<Stats>().RegrnTime - 1;
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }

    }
    public void Take()
    {
        interfac0.transform.parent = transform.parent;
        interfac0.transform.localPosition = new Vector3(0, 0, -0.575f);
        interfac0.transform.localRotation = Quaternion.Euler(0, 0, 0);
        interfac0.GetComponent<Rigidbody>().isKinematic = true;
        interfac0.GetComponent<BoxCollider>().enabled = false;
        interfac = interfac0;
        int y = interfac.GetComponent<Inventory>().SLON.Length;
        calls = new Call[y];
        for (int i = 0; i < y; i++)
        {
            calls[i] = interfac.GetComponent<Inventory>().SLON[i];
        }
    }
    public void iconss(Call col)
    {
        if (col.colvo != 0)
        {
            if (col.I != 18)
            {
                GameObject obj = Instantiate(items[col.I]);
                obj.transform.parent = transform.parent;
                obj.transform.localPosition = new Vector3(0, 0, 1.5f);
                obj.transform.parent = null;
                if (col.colvo == 1)
                {
                    Destroy(col.transform.GetChild(1).gameObject);
                    col.I = -1;
                }
                if (obj.GetComponent<Item>().I == 5)
                {
                    plar.GetComponent<Move>().key = false;
                }
            }
            else
            {
                map OOO = Map.GetComponent<Item>().runs[0].GetComponent<map>();
                OOO.tako = null;
                OOO.weapon = null;
                OOO.cam[0] = null;
                OOO.cam[1] = null;
                OOO.plar = null;

                Map.localPosition = new Vector3(0, 0, 1.5f);
                Map.parent = null;
                Map.gameObject.SetActive(true);
                Map = null;

                Destroy(col.transform.GetChild(1).gameObject);
                col.I = -1;
            }
            col.colvo -= 1;
        }
    }
}
