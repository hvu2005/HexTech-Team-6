using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefs;
    [SerializeField] private int poolSize;
    private List<GameObject> pool;
    // Start is called before the first frame update
    void Awake()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(prefs);
            bullet.SetActive(false);
            pool.Add(bullet);
        }
    }
    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                obj.transform.position = transform.position + new Vector3(0, -0.2f, 0);
                return obj;
            }
        }

        GameObject newObj = Instantiate(prefs);
        newObj.transform.position = transform.position + new Vector3(0, -0.2f, 0);
        newObj.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
