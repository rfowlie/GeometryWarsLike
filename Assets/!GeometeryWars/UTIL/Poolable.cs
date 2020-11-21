using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//object pool will set itself as pool for this object
public class Poolable : MonoBehaviour
{
    private ObjectPool pool;

    //ensure only ObjectPool can call this method
    public void SetPool<T>(T pool) where T : ObjectPool
    {
        this.pool = pool;
    }

    //allow outside things to notify poolable Object to return to its pool
    public void ReturnToPool(GameObject self)
    {
        pool.Return(self);
    }
}
