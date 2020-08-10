using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amount;
    public GameObject prefab;
    public bool expandPool;
}

public class ObjectPooler : MonoBehaviour
{

    //lets declare this class as a static variable, so other classes can actually access it
    public static ObjectPooler SharedInstance;
    public bool expandPool = true;

    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;
    // Start is called before the first frame update

    //initialise itself in awake
    void Awake()
    {
        SharedInstance = this;
        pooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject pickup = (GameObject)Instantiate(item.prefab);
                pickup.SetActive(false);
                pooledObjects.Add(pickup);
            }
        }
    }
    void Start()
    {



    }

    //method to get one active object from the pool
    public GameObject GetPooledObject(string tag)
    {

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.prefab.tag == tag)
            {
                if (item.expandPool)
                {
                    GameObject pickup = (GameObject)Instantiate(item.prefab);
                    pickup.SetActive(false);
                    pooledObjects.Add(pickup);
                    return pickup;
                }
            }
        }

        return null;
    }
}
