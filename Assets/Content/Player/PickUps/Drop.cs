using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drop : MonoBehaviour
{
    public DropType type = DropType.NONE;

    public static event Action<DropType> TRIGGER;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("<color=red>DROP COLLISION</color>");
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player collected this!!");
            TRIGGER?.Invoke(type);
        }
    }
}
