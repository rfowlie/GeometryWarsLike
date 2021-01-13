using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    //hold an array of all level patterns for the game
    [CreateAssetMenu(fileName = "GameLevelPatterns", menuName = "ScriptableObjects/Game/LevelPatterns")]
    public class SO_GameLevelPatterns : ScriptableObject
    {
        [SerializeField] private SO_LevelPattern[] allPatterns;
        public SO_LevelPattern GetPatternAtIndex(int index)
        {
            if(index <0 || index > allPatterns.Length)
            {
                index = 0;
            }

            return allPatterns[index];
        }
    }
}