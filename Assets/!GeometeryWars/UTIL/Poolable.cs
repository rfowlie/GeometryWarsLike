using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//object pool will set itself as pool for this object
public class Poolable : MonoBehaviour
{
    public ObjectPool pool { get; set; }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        pool.Return(gameObject);
    }
}
