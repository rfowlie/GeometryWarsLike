using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//control all aspects of level
namespace GeometeryWars
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TimeManager time;
        [SerializeField] private SpawnManagerALT spawn;
        [SerializeField] private PointsManager points;
        [SerializeField] private PlayerManager player;

        public static event Action<LevelManager> START;
        public static event Action END;

        //get the current points for this level
        public int GetPoints()
        {
            return points.points;
        }

        private void Start()
        {
            time = GetComponent<TimeManager>();
            spawn = GetComponent<SpawnManagerALT>();
            points = GetComponent<PointsManager>();
            player = GetComponent<PlayerManager>();

            START(this);
        }

        private void Update()
        {
            //run timer..check if level finished
            if(!time.AdjustTime(Time.deltaTime))
            {
                //level finished
                END();
            }

            //run spawner
            spawn.Execute(time.GetTimeFromStart());

            //update units

            //update player

        }
    }

    //will have a delta time passed in when active and will spawn units according to the updated time
    //also holds all object pools for enemies
    public class SpawnManagerALT : MonoBehaviour
    {
        [Header("Variables")]
        public SO_LevelPattern levelPatterns;
        private ObjectPool[] pools;
        public int levelIndex = 0;
        public float spawnCount = 0f;
        bool isSpawn = true;
        Coroutine c = null;

        
        private void Start()
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

        public void Execute(float timeFromZero)
        {
            if(isSpawn)
            {
                //check time against current index
                if (levelPatterns.spawnTimes[levelIndex] < timeFromZero)
                {
                    //spawn pattern
                    StartCoroutine(SpawnUnits(levelIndex));

                    //LOOPS, won't need later on...
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
                GameObject temp = pools[levelPatterns.enemyTypeIndex[levelIndex]].Get();
                //get position
                temp.transform.position = levelPatterns.patterns[levelIndex].points[i];
                //point transform down towards map
                temp.transform.rotation = Quaternion.FromToRotation(temp.transform.up, temp.transform.position - map.position) * temp.transform.rotation;
                yield return null;
            }
        }
    }


    //keeps track of the level time
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private int levelTime = 30;
        private float currentTime;
        public float GetCurrentTime() { return currentTime; }
        public float GetTimeFromStart() { return levelTime - currentTime; }

        private void Start()
        {
            currentTime = levelTime;
        }

        public bool AdjustTime(float deltaTime)
        {
            currentTime -= deltaTime;
            return currentTime < 0f;
        }
    }

    //new player script...
    public class PlayerManager : MonoBehaviour
    {

    }
}


