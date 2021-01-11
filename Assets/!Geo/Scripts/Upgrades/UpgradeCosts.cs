using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hold the scriptable object and act as the facade for the information so that it remains private
public class UpgradeCosts : MonoBehaviour
{
    [SerializeField] SO_UpgradeCosts upgradeCosts;

    public int GetMovementCount() { return upgradeCosts.movementUpgradeCost.Length; }
    public int GetMovementUpgrade(int level)
    {
        return upgradeCosts.movementUpgradeCost[level];
    }
}
