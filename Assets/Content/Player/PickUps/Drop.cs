using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drop : MonoBehaviour
{
    //player uses this to determine the upgrade, set by DropManager
    [HideInInspector] public DropType type = DropType.NONE;

    public static event Action<DropType> TRIGGER;

    private void OnTriggerEnter(Collider other)
    {        
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player collected this!!");
            TRIGGER?.Invoke(type);

            //for object pool
            gameObject.SetActive(false);
        }
    }
}
