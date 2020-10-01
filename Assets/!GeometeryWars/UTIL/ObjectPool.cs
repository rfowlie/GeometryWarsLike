using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Object pool for gameobjects
public class ObjectPool
{
    //Constructor
    public ObjectPool(Poolable poolObject)
    {
        this.poolObject = poolObject.gameObject;
    }

    //use later for getting different pools through code...
    [SerializeField] private string poolName = string.Empty;
    [SerializeField] private GameObject poolObject = null;
    //private List<GameObject> active = new List<GameObject>();
    //public int ActiveCount() { return active.Count; }
    private Queue<GameObject> deactive = new Queue<GameObject>();
    public int DeactiveCount() { return deactive.Count; }

    public GameObject Get()
    {
        GameObject obj = deactive.Count > 0 ? deactive.Dequeue() : Create();
        //active.Add(obj);
        obj.SetActive(true);
        return obj;
    }
    public void Return(GameObject obj)
    {
        //if (!active.Contains(obj))
        //{
        //    Debug.LogError("Object to be removed not in active pool");
        //    return;
        //}
        //active.Remove(obj);
        deactive.Enqueue(obj);
        obj.SetActive(false);
    }
    
    //public void ReturnAll()
    //{
    //    foreach(var obj in active)
    //    {
    //        deactive.Enqueue(obj);
    //        obj.SetActive(false);
    //    }

    //    active.Clear();
    //}

    public int objectCount = 0;
    private GameObject Create()
    {
        GameObject obj = GameObject.Instantiate(poolObject);
        obj.name = poolObject.name + " " + objectCount++.ToString();
        obj.GetComponent<Poolable>().pool = this;
        return obj;
    }
}
