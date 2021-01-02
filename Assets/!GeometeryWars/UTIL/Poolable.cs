using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//object pool will set itself as pool for this object
public class Poolable : MonoBehaviour
{
    public event Action<Poolable> RECYCLE;

    //allow outside things to notify poolable Object to return to its pool
    public void ReturnToPool(Poolable self)
    {
        RECYCLE(this);
    }
}
