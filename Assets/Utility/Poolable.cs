using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//object pool will set itself as pool for this object
public class Poolable : MonoBehaviour
{
    //public ObjectPoolALT pool;
    public event Action<GameObject> RETURN;

    //?could be a sneaky solve, gameobject doesn't even have to know it has a poolable component...
    private void OnDisable()
    {
        RETURN(gameObject);
        //pool.Return(gameObject);
    }
}
