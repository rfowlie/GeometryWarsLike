using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    [CreateAssetMenu(fileName = "Level Pattern", menuName = "ScriptableObjects/Spawn/Level")]
    public class SO_LevelPattern : ScriptableObject
    {
        //ensure all relevant arrays are of same length
        private void OnValidate()
        {
            if (arraySize < 0) { arraySize = 0; }
            if (patternIndex.Length != arraySize)
            {
                patternIndex = ArrayEX.ResizeArray(arraySize, patternIndex);
            }
            if (spawnTimes.Length != arraySize)
            {
                spawnTimes = ArrayEX.ResizeArray(arraySize, spawnTimes);
            }
            if (enemyTypeIndex.Length != arraySize)
            {
                enemyTypeIndex = ArrayEX.ResizeArray(arraySize, enemyTypeIndex);
            }
        }

        
        [Header("Delay")]
        public float startDelay = 0f;
        public float endDelay = 3f;
        [Space]
        [Space]
        [Header("Info")]
        public AEnemy[] enemyTypes;
        public SO_SpawnPattern[] patterns;
        [Space]
        [Space]
        [Header("Spawns")]
        public int arraySize = 0;
        public int[] enemyTypeIndex;
        public int[] patternIndex;
        public float[] spawnTimes;
    }
}

