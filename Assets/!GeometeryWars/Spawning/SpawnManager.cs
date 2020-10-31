﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeometeryWars
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Components")]
        public Transform map;
        [Space]
        
        [Header("Variables")]
        public SO_LevelPattern levelPatterns;
        private ObjectPool[] pools;
        public int levelIndex = 0;
        public float levelTimer = 60f;
        public float spawnCount = 0f;

        public bool isPlay = false;

        private void Start()
        {
            //create pools
            pools = new ObjectPool[levelPatterns.enemyPrefabs.Length];
            for (int i = 0; i < pools.Length; i++)
            {
                if (levelPatterns.enemyPrefabs[i].GetComponent<Poolable>() != null)
                {
                    pools[i] = new ObjectPool(levelPatterns.enemyPrefabs[i].GetComponent<Poolable>());
                }
            }

            //start
            levelIndex = 0;
            c = StartCoroutine(SpawnDelay(levelPatterns.startDelay));
            isPlay = true;
        }


        private void Update()
        {
            if(isPlay)
            {
                isPlay = Run();
            }
        }

        bool isSpawn = false;
        Coroutine c = null;
        IEnumerator SpawnDelay(float delay)
        {
            //Debug.Log("<color=green>Start Delay</color>");
            isSpawn = false;
            yield return new WaitForSeconds(delay);
            isSpawn = true;
            //Debug.Log("<color=red>End Delay</color>");
        }

        private bool Run()
        {
            //update counters
            levelTimer -= Time.deltaTime;

            if(isSpawn)
            {
                spawnCount += Time.deltaTime;

                //check time against current index
                if (levelPatterns.spawnTimes[levelIndex] < spawnCount)
                {
                    //spawn pattern
                    StartCoroutine(SpawnUnits(levelIndex));

                    //prime next pattern, activate delay
                    levelIndex++;
                    if (levelIndex == levelPatterns.patterns.Length)
                    {
                        levelIndex = 0;
                        spawnCount = 0f;
                        c = StartCoroutine(SpawnDelay(levelPatterns.endDelay));
                    }
                }
            }     

            return levelTimer > 0f;
        }

        //spawn units on different thread so no lags
        IEnumerator SpawnUnits(int levelIndex)
        {
            //spawn pattern
            for (int i = 0; i < levelPatterns.patterns[levelIndex].points.Length; i++)
            {
                GameObject temp = pools[levelPatterns.enemyTypeIndex[levelIndex]].Get();
                temp.transform.position = levelPatterns.patterns[levelIndex].points[i];

                //point transform down towards map
                temp.transform.rotation = Quaternion.FromToRotation(temp.transform.up, temp.transform.position - map.position) * temp.transform.rotation;
                yield return null;
            }
        }
    }
}