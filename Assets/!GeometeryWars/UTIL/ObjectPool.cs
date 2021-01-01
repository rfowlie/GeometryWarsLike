using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Object pool for gameobjects
public class ObjectPool<T> where T : Poolable
{
    //Constructor
    public ObjectPool(T poolObject, int initialAmount = 1, string poolName = "")
    {
        active = new Dictionary<int, T>();
        deactive = new Queue<T>();

        this.poolObject = poolObject.gameObject;
        this.poolName = poolName;
        int amount = initialAmount < 0 ? 1 : initialAmount;
        for (int i = 0; i < amount; i++)
        {
            T temp = Create();
            temp.gameObject.SetActive(false);
            deactive.Enqueue(temp);
        }
    }

    //use later for getting different pools through code...
    private string poolName = string.Empty;
    public string Name() { return poolName; }
    private GameObject poolObject = null;
    private Queue<T> deactive;
    public int DeactiveCount() { return deactive.Count; }
    private Dictionary<int, T> active;
    public List<T> GetActive() { return new List<T>(active.Values); }

    //return available deactive objs else create a new one
    public T Get()
    {
        T obj;
        if(deactive.Count > 0)
        {
            obj = deactive.Dequeue();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Create();
        }        
        
        return obj;
    }
    public void Return(GameObject obj)
    {
        T temp;
        if(active.TryGetValue(obj.GetInstanceID(), out temp))
        {
            deactive.Enqueue(temp);
            temp.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log($"{obj.name} does not belong in object pool {poolName}!!");
        }
    }

    //keep track of total objs created in pool
    private int objectCount = 0;
    public int CreatedCount() { return objectCount; }
    private T Create()
    {
        GameObject obj = GameObject.Instantiate(poolObject);
        //set scene
        obj.name = poolObject.name + " " + objectCount++.ToString();
        //obj.GetComponent<Poolable>().SetPool(this);
        return obj.GetComponent<T>();
    }
}
