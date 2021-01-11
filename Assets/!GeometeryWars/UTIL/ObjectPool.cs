using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Adds a poolable component to any Gameobject passed in if doesn't have one already
//sets event on poolable to the return method of the object pool
//fires the event when gameobject is disable/set inactive

//Object pool for gameobjects
public class ObjectPool<T> where T : MonoBehaviour
{
    //Constructor
    public ObjectPool(T obj, int initialAmount = 1)
    {
        active = new List<T>();
        deactive = new Queue<T>();

        poolObject = obj;
        int amount = initialAmount < 0 ? 1 : initialAmount;
        for (int i = 0; i < amount; i++)
        {
            T temp = Create();
            active.Add(temp);
            temp.gameObject.SetActive(false);
        }
    }

    private T poolObject;
    private Queue<T> deactive;
    public int DeactiveCount() { return deactive.Count; }
    private List<T> active;
    public List<T> GetActive() { return active; }

    //return available deactive objs else create a new one
    public T Retrieve()
    {
        T obj;
        if (deactive.Count > 0)
        {
            obj = deactive.Dequeue();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Create();
        }

        active.Add(obj);
        return obj;
    }
    public void Return(GameObject obj)
    {
        T temp = obj.GetComponent<T>();
        if (active.Contains(temp))
        {
            active.Remove(temp);
            deactive.Enqueue(temp);
            //need this???
            obj.SetActive(false);
        }
        else
        {
            Debug.Log($"{obj.name} does not belong to this object pool!!");
        }
    }

    //keep track of total objs created in pool
    private int objectCount = 0;
    public int CreatedCount() { return objectCount; }
    private T Create()
    {
        GameObject obj = GameObject.Instantiate(poolObject.gameObject);
        
        //check if Poolable componet, set pool, add if not
        Poolable temp = obj.GetComponent<Poolable>();
        if (temp != null)
        {
            temp.RETURN += Return;
            //obj.GetComponent<PoolableALT>().pool = this;
        }
        else
        {

            temp = obj.AddComponent<Poolable>();
            temp.RETURN += Return;
        }

        //set scene
        obj.name = poolObject.name + " " + objectCount++.ToString();
        return obj.GetComponent<T>();
    }
}
