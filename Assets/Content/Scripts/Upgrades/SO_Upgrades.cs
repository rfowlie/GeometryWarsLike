using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrades", menuName = "ScriptableObjects/Game/Upgrades")]
public class SO_Upgrades : ScriptableObject
{
    [SerializeField] public int[] movementCosts = { 100, 200, 400, 800 };
    [SerializeField] public float[] movementValues = { 1, 2, 3, 4 };

    [SerializeField] public int[] fireRateCosts = { 100, 200, 300, 400 };
    [SerializeField] public float[] fireRateValues = { 1f, 0.85f, 0.55f, 0.3f };

    [SerializeField] public int[] healthCosts = { 100, 200, 400, 800 };
    [SerializeField] public int[] healthValues = { 100, 200, 300, 400 };
}
