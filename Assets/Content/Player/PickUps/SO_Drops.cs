using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "SO_Drops", menuName = "ScriptableObjects/Game/Drops")]
public class SO_Drops : ScriptableObject
{

    [SerializeField] private DropInfo[] sets;    
    
    //send drop info depending on random selection based on stored percentages
    public DropInfo GetRandomPickUp()
    {
        //determine which drop info to return from percentages...
        return sets[SelectIndexFromPercentages()];
    }

    public int SelectIndexFromPercentages()
    {
        int index = -1;
        float largest = -1;
        for (int i = 0; i < sets.Length; i++)
        {
            float temp = sets[i].percentage * UnityEngine.Random.Range(0f, 1f);
            if (temp > largest)
            {
                index = i;
                largest = temp;
            }
        }

        return index;
    }
}


//need to allow and array of these to expose properly 
[System.Serializable]
public struct DropInfo
{
    public DropType type;
    //prefab
    public GameObject prefab;
    public float percentage;
}

public enum DropType { NONE, HEAL, HEALTH, FIRERATE, MOVEMENTSPEED, ARMOUR }
