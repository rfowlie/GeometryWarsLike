using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum DropType { NONE, HEALTH, FIRERATE, MOVEMENTSPEED, ARMOUR }
public class Drop : MonoBehaviour
{
    [SerializeField] private DropType type = DropType.NONE;


    public static event Action TRIGGER;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("<color=red>DROP COLLISION</color>");
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player collected this!!");
            TRIGGER?.Invoke();
        }
    }
}
