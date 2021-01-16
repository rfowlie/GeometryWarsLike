using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "SO_Drops", menuName = "ScriptableObjects/Game/Drops")]
public class SO_Drops : ScriptableObject
{
    [SerializeField] private DropInfo[] values;
    
    public Drop GetRandomPickUp()
    {
        //randomly select a drop

        //instantiate

        //return
        return null;
    }
}


//need to allow and array of these to expose properly 
[System.Serializable]
public struct DropInfo
{
    public Drop drop;
    public float percentage;
}
