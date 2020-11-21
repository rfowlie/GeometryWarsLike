using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Object pool for gameobjects
public class ObjectPool
{
    //Constructor
    public ObjectPool(Poolable poolObject, string poolName = "")
    {
        this.poolObject = poolObject.gameObject;
        this.poolName = poolName;
    }

    //use later for getting different pools through code...
    private string poolName = string.Empty;
    public string Name() { return poolName; }
    private GameObject poolObject = null;
    private Queue<GameObject> deactive = new Queue<GameObject>();
    public int DeactiveCount() { return deactive.Count; }

    //return available deactive objs else create a new one
    public GameObject Get()
    {
        GameObject obj = deactive.Count > 0 ? deactive.Dequeue() : Create();
        obj.SetActive(true);
        return obj;
    }
    public void Return(GameObject obj)
    {        
        deactive.Enqueue(obj);
        obj.SetActive(false);
    }

    //keep track of total objs created in pool
    public int objectCount = 0;
    private GameObject Create()
    {
        GameObject obj = GameObject.Instantiate(poolObject);
        obj.name = poolObject.name + " " + objectCount++.ToString();
        obj.GetComponent<Poolable>().SetPool(this);
        return obj;
    }
}
