using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hold the scriptable object and act as the facade for the information so that it remains private
public class UpgradesController : Singleton<UpgradesController>
{
    [SerializeField] private SO_Upgrades upgrades;

    //seems silly...
    public int GetMovementLevels() { return upgrades.movementCosts.Length; }
    public int GetMovementCost(int level) { return upgrades.movementCosts[level]; }
    public float GetMovementValue(int level) { return upgrades.movementValues[level]; }

    public int GetFireRateLevels() { return upgrades.fireRateCosts.Length; }
    public int GetFireRateCost(int level) { return upgrades.fireRateCosts[level]; }
    public float GetFireRateValue(int level) { return upgrades.fireRateValues[level]; }

    public int GetHealthLevels() { return upgrades.healthCosts.Length; }
    public int GetHealthCost(int level) { return upgrades.healthCosts[level]; }
    public int GetHealthValue(int level) { return upgrades.healthValues[level]; }
}
