using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//object pool will set itself as pool for this object
public class Poolable : MonoBehaviour
{
    public ObjectPool pool { get; set; }

    private bool isPool = true;
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(isPool)
        {
            isPool = false;
            pool.Return(gameObject);
        }
    }
}
