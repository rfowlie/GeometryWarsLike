﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Threading.Tasks;


namespace GeometeryWars
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Variables")]
        public SO_LevelPattern levelPatterns;
        private ObjectPool<AEnemy>[] pools;
        public int levelIndex = 0;
        public float spawnCount = 0f;
        bool isSpawn = true;
        Coroutine c = null;


        private void Start()
        {
            //create pools
            pools = new ObjectPool<AEnemy>[levelPatterns.enemyPrefabs.Length];
            for (int i = 0; i < pools.Length; i++)
            {
                if (levelPatterns.enemyPrefabs[i].GetComponent<Poolable>() != null)
                {
                    pools[i] = new ObjectPool<AEnemy>(levelPatterns.enemyPrefabs[i].GetComponent<AEnemy>(), 100);
                }
            }

            levelIndex = 0;
        }

        public void Execute(float timeFromZero)
        {
            if (isSpawn)
            {
                //check time against current index
                if (levelPatterns.spawnTimes[levelIndex] < timeFromZero)
                {
                    //spawn pattern
                    StartCoroutine(SpawnUnits(levelIndex));

                    //prime next pattern, activate delay
                    levelIndex++;

                    if (levelIndex == levelPatterns.patterns.Length)
                    {
                        isSpawn = false;
                    }
                }
            }

        }

        //spawn units one on each frame... 
        IEnumerator SpawnUnits(int levelIndex)
        {
            Transform map = GlobalVariables.Instance.map;

            //spawn pattern
            for (int i = 0; i < levelPatterns.patterns[levelIndex].points.Length; i++)
            {
                AEnemy temp = pools[levelPatterns.enemyTypeIndex[levelIndex]].Get();
                //get position
                temp.transform.position = levelPatterns.patterns[levelIndex].points[i];
                //point transform down towards map
                temp.transform.rotation = Quaternion.FromToRotation(temp.transform.up, temp.transform.position - map.position) * temp.transform.rotation;
                yield return null;
            }
        }
    }
}