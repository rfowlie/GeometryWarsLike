using System.Collections;
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
        private ObjectPool[] pools;
        public int levelIndex = 0;
        public float spawnCount = 0f;
        bool isSpawn = false;
        Coroutine c = null;

        private void OnEnable()
        {
            Timer.BEGIN += () => PauseSpawn(levelPatterns.startDelay);
            Timer.FINISH += () => isSpawn = false;
        }

        private void OnDisable()
        {
            Timer.BEGIN -= () => PauseSpawn(levelPatterns.startDelay);
            Timer.FINISH -= () => isSpawn = false;
        }

        private void Start()
        {
            //Setup();
        }

        public void Setup()
        {
            //create pools
            pools = new ObjectPool[levelPatterns.enemyPrefabs.Length];
            for (int i = 0; i < pools.Length; i++)
            {
                if (levelPatterns.enemyPrefabs[i].GetComponent<Poolable>() != null)
                {
                    pools[i] = new ObjectPool(levelPatterns.enemyPrefabs[i].GetComponent<Poolable>(), 100);
                }
            }

            levelIndex = 0;
        }

        public void PauseSpawn(float delay)
        {
            isSpawn = false;
            c = CoroutineEX.Delay(this, () => isSpawn = true, delay);
        }

        private void Update()
        {            
            if(isSpawn)
            {
               Run();
            }
        }        
        
        private void Run()
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
                    PauseSpawn(levelPatterns.startDelay);
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
                GameObject temp = pools[levelPatterns.enemyTypeIndex[levelIndex]].Get();                
                //get position
                temp.transform.position = levelPatterns.patterns[levelIndex].points[i];
                //point transform down towards map
                temp.transform.rotation = Quaternion.FromToRotation(temp.transform.up, temp.transform.position - map.position) * temp.transform.rotation;
                yield return null;
            }
        }






        //NEED TO USE JOBS SYSTEM
        //****************************
        //try to async spawn enemies...
        //won't work as you cannot instantiate nor change active state of gameobjects from outside main thread...
        //private async void CallSpawnTask(int levelIndex)
        //{
        //    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        //    sw.Start();
        //    Debug.Log("<color=green>Task is starting</color>");
        //    var spawnTask = SpawnTask(levelIndex, levelPatterns, pools);
        //    await spawnTask;
        //    sw.Stop();
        //    Debug.Log($"<color=blue>Task is finished</color>: {sw.ElapsedMilliseconds} milliseconds");
        //}
        //private Task SpawnTask(int levelIndex, SO_LevelPattern levelPatterns, ObjectPool[] pools)
        //{
        //    return Task.Run(() =>
        //    {
        //        for (int i = 0; i < levelPatterns.patterns[levelIndex].points.Length; i++)
        //        {
        //            UnityEngine.Debug.Log("Loop");
        //            GameObject temp = pools[levelPatterns.enemyTypeIndex[levelIndex]].Get();
        //            if (temp == null) { Debug.Log("Couldn't get object from pool in task"); }
        //            temp.transform.position = levelPatterns.patterns[levelIndex].points[i];

        //            //point transform down towards map
        //            temp.transform.rotation = Quaternion.FromToRotation(temp.transform.up, temp.transform.position - map.position) * temp.transform.rotation;
        //        }
        //    });
        //}
    }
}