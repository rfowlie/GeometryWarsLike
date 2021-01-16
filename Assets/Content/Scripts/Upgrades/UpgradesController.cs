using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum UpgradeType { NONE, HEALTH, MOVEMENT, FIRERATE }
//hold the scriptable object and act as the facade for the information so that it remains private
public class UpgradesController : Singleton<UpgradesController>
{
    [SerializeField] private SO_Upgrades upgrades;

    private int IndexInRange(int level, int compare)
    {
        if(level < 0) { return 0; }
        if(level >= compare) { return compare - 1; }
        return level;
    }


    //seems silly...
    public int GetMovementLevels() { return upgrades.movementCosts.Length; }
    public int GetMovementCost(int level) { return upgrades.movementCosts[IndexInRange(level, GetMovementLevels())]; }
    public float GetMovementValue(int level) { return upgrades.movementValues[IndexInRange(level, GetMovementLevels())]; }

    public int GetFireRateLevels() { return upgrades.fireRateCosts.Length; }
    public int GetFireRateCost(int level) { return upgrades.fireRateCosts[IndexInRange(level, GetFireRateLevels())]; }
    public float GetFireRateValue(int level) { return upgrades.fireRateValues[IndexInRange(level, GetFireRateLevels())]; }

    public int GetHealthLevels() { return upgrades.healthCosts.Length; }
    public int GetHealthCost(int level) { return upgrades.healthCosts[IndexInRange(level, GetHealthLevels())]; }
    public int GetHealthValue(int level) { return upgrades.healthValues[IndexInRange(level, GetHealthLevels())]; }
}
