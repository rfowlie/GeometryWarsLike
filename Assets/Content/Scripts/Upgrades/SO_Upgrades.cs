using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrades", menuName = "ScriptableObjects/Game/Upgrades")]
public class SO_Upgrades : ScriptableObject
{
    //[SerializeField] public CostValue[] movement;

    [SerializeField] public int[] movementCosts;
    [SerializeField] public float[] movementValues;

    [SerializeField] public int[] fireRateCosts;
    [SerializeField] public float[] fireRateValues;

    [SerializeField] public int[] healthCosts;
    [SerializeField] public int[] healthValues;
}



[System.Serializable]
public struct CostValue
{
    public int cost;
    public int value;
}


