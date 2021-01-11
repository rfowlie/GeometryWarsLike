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
        private ObjectPool<AEnemy>[] pools;
        public ObjectPool<AEnemy>[] GetPools()
        {
            return pools;
        }
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
                pools[i] = new ObjectPool<AEnemy>(levelPatterns.enemyPrefabs[i], 100);
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

                    //if we've reached the end of the spawn list turn spawning off
                    if (levelIndex == levelPatterns.spawnTimes.Length)
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
                AEnemy temp = pools[levelPatterns.enemyTypeIndex[levelIndex]].Retrieve();
                //get position
                temp.transform.position = levelPatterns.patterns[levelIndex].points[i];
                //point transform down towards map
                temp.transform.rotation = Quaternion.FromToRotation(temp.transform.up, temp.transform.position - map.position) * temp.transform.rotation;
                yield return null;
            }
        }
    }
}