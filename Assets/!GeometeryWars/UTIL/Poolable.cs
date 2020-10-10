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

    //don't allow things deriving from poolable to access any other Object Pool Functions
    public void ReturnToPool(GameObject self)
    {
        pool.Return(self);
    }
}
