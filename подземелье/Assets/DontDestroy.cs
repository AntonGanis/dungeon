using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public List<GameObject> dont;

    private void Awake()
    {
        if (Data.one == false)
        {
            DontDestroyOnLoad(gameObject);
            Data.one = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Go(int levl)
    {
        foreach (GameObject obj in dont)
        {
            obj.transform.SetParent(transform);
        }
        Data.power += 1;
        StartCoroutine(LoadScene(levl));
    }

    private IEnumerator LoadScene(int levl)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levl);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        MoveDontObjects();
    }

    private void MoveDontObjects()
    {
        foreach (GameObject obj in dont)
        {
            if (obj.GetComponent<RectTransform>() == null)
            {
                float x = Random.Range(-7f, 7f);
                float z = Random.Range(-7f, 7f);
                obj.transform.position = new Vector3(x, 1.5f, z);
                if(obj.GetComponent<Move>()!=null)
                {
                    obj.GetComponent<Move>().enabled = false;
                    obj.GetComponent<Stats>().enabled = false;
                }
            }
            else
            {
                obj.transform.SetParent(GameObject.Find("Canvas").transform);
            }
        }
    }
}
