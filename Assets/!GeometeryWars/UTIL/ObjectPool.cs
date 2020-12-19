using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Object pool for gameobjects
public class ObjectPool
{
    //Constructor
    public ObjectPool(Poolable poolObject, int initialAmount = 1, string poolName = "")
    {
        this.poolObject = poolObject.gameObject;
        this.poolName = poolName;
        SetupInitial(initialAmount < 0 ? 1 : initialAmount);
    }

    //use later for getting different pools through code...
    private string poolName = string.Empty;
    public string Name() { return poolName; }
    private GameObject poolObject = null;
    private Queue<GameObject> deactive = new Queue<GameObject>();
    public int DeactiveCount() { return deactive.Count; }

    public void SetupInitial(int number)
    {
        for(int i = 0; i < number; i++)
        {
            GameObject temp = Create();
            temp.SetActive(false);
            deactive.Enqueue(temp);
        }
    }

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
    private int objectCount = 0;
    public int CreatedCount() { return objectCount; }
    private GameObject Create()
    {
        GameObject obj = GameObject.Instantiate(poolObject);
        //set scene
        obj.name = poolObject.name + " " + objectCount++.ToString();
        obj.GetComponent<Poolable>().SetPool(this);
        return obj;
    }
}
