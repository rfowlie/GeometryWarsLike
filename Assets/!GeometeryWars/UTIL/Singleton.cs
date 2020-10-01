using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base SingltonClass
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    //property
    public static T Instance { get; private set; }

    //check if not null
    public static bool IsInitialized
    {
        get { return Instance != null; }
    }

    //set self to be instance on awake
    protected virtual void Awake()
    {
        if(IsInitialized)
        {
            Debug.Log("[Singleton] Trying to instantiate a second instance ");
        }
        else
        {
            Instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
}
