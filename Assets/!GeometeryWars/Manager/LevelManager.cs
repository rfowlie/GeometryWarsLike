using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Unity.Burst;
using Unity.Jobs;


//control all aspects of level
namespace GeometeryWars
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TimeManager time;
        [SerializeField] private SpawnManager spawn;
        [SerializeField] private PointsManager points;
        [SerializeField] private PlayerManager player;
        private EnemyManager enemy;

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
            spawn = GetComponent<SpawnManager>();
            points = GetComponent<PointsManager>();
            player = GetComponent<PlayerManager>();

            enemy = new EnemyManager();

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

    //run enemy behaviour/movement using JOB system
    public class EnemyManager
    {
        public void Move(List<AEnemy> list)
        {
            foreach(var i in list)
            {
                //passing in list of Aenemy so run movement
                i.Move();
            }
        }

        public struct EnemyMovement : IJobParallelFor
        {
            public void Execute(int index)
            {
                
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


