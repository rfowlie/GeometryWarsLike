using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Costs", menuName = "ScriptableObjects/Game/Upgrades")]
public class SO_UpgradeCosts : ScriptableObject
{
    [SerializeField] public int[] movementUpgradeCost = { 100, 200, 400, 800 };
}
