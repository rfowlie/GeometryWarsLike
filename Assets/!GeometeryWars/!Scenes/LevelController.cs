using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Contains and keeps track of all levels
public class LevelController : MonoBehaviour
{
    [SerializeField] private string boot;
    public string GetBoot() { return boot; }
    [SerializeField] private string baseLevel;
    public string GetBaseLevel() { return baseLevel; }
    [SerializeField] private int currentlevelIndex = 0;
    public int GetCurrentLevelIndex() { return currentlevelIndex; }
    [SerializeField] private string[] alllevels;
}
