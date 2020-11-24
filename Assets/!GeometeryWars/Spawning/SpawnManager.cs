using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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
        bool isSpawn = false;
        Coroutine c = null;

        private void Start()
        {
            //create pools
            pools = new ObjectPool[levelPatterns.enemyPrefabs.Length];
            for (int i = 0; i < pools.Length; i++)
            {
                if (levelPatterns.enemyPrefabs[i].GetComponent<Poolable>() != null)
                {
                    pools[i] = new ObjectPool(levelPatterns.enemyPrefabs[i].GetComponent<Poolable>());
                    //create a number of enemies at start to prevent mass creations...
                    pools[i].SetupInitial(100);
                }
            }

            //start
            levelIndex = 0;
            isSpawn = false;
            c = CoroutineEX.Delay(this, () => isSpawn = true, levelPatterns.startDelay); ;
            isPlay = true;
        }


        private void Update()
        {
            //for now... once levelManager created it will execute this every frame
            //or devise system using corotine to wait amount of time for next spawn to happen...
            if(isPlay)
            {
                isPlay = Run();
            }
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
                    //CallSpawnTask(levelIndex);
                    StartCoroutine(SpawnUnits(levelIndex));

                    //prime next pattern, activate delay
                    levelIndex++;
                    if (levelIndex == levelPatterns.patterns.Length)
                    {
                        levelIndex = 0;
                        spawnCount = 0f;
                        isSpawn = false;
                        c = CoroutineEX.Delay(this, () => isSpawn = true, levelPatterns.startDelay);
                        c = CoroutineEX.OnNextFrame(this, () => Debug.Log("This executed next frame"));
                    }
                }
            }     

            return levelTimer > 0f;
        }

        //spawn units on different thread so no lags...
        IEnumerator SpawnUnits(int levelIndex)
        {
            //spawn pattern
            for (int i = 0; i < levelPatterns.patterns[levelIndex].points.Length; i++)
            {
                GameObject temp = pools[levelPatterns.enemyTypeIndex[levelIndex]].Get();
                //get position
                temp.transform.position = levelPatterns.patterns[levelIndex].points[i];
                //point transform down towards map
                temp.transform.rotation = Quaternion.FromToRotation(temp.transform.up, temp.transform.position - map.position) * temp.transform.rotation;
                yield return null;
            }
        }



        //****************************
        //try to async spawn enemies...
        //won't work as you cannot instantiate nor change active state of gameobjects from outside main thread...
        private async void CallSpawnTask(int levelIndex)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Debug.Log("<color=green>Task is starting</color>");
            var spawnTask = SpawnTask(levelIndex, levelPatterns, pools);
            await spawnTask;
            sw.Stop();
            Debug.Log($"<color=blue>Task is finished</color>: {sw.ElapsedMilliseconds} milliseconds");
        }
        private Task SpawnTask(int levelIndex, SO_LevelPattern levelPatterns, ObjectPool[] pools)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < levelPatterns.patterns[levelIndex].points.Length; i++)
                {
                    UnityEngine.Debug.Log("Loop");
                    GameObject temp = pools[levelPatterns.enemyTypeIndex[levelIndex]].Get();
                    if (temp == null) { Debug.Log("Couldn't get object from pool in task"); }
                    temp.transform.position = levelPatterns.patterns[levelIndex].points[i];

                    //point transform down towards map
                    temp.transform.rotation = Quaternion.FromToRotation(temp.transform.up, temp.transform.position - map.position) * temp.transform.rotation;
                }
            });
        }
    }
}