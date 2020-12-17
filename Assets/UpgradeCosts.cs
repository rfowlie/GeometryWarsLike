using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCosts : Singleton<UpgradeCosts>
{
    [SerializeField] private int[] movementUpgradeCost = { 100, 200, 400, 800 };
    public int GetMovementUpgrade(int level)
    {
        return movementUpgradeCost[level];
    }
}
