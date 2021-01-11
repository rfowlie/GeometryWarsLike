﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Unity.Burst;
using Unity.Jobs;


//control all aspects of in game level
namespace GeometeryWars
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TimeManager time;
        [SerializeField] private PointsManager points;
        [SerializeField] private SpawnManager spawn;
        //[SerializeField] private PlayerManager player;
        private EnemyManager enemy;

        private bool isActive = false;

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
            points = GetComponent<PointsManager>();
            spawn = GetComponent<SpawnManager>();
            //player = GetComponent<PlayerManager>();

            enemy = new EnemyManager(spawn);

            START(this);
            isActive = true;
        }

        private void Update()
        {
            if(isActive)
            {
                //run timer..check if level finished
                if (time.AdjustTime(Time.deltaTime))
                {
                    isActive = false;
                    
                    //level finished
                    END();
                }

                //run spawner
                spawn.Execute(time.GetTimeFromStart());                

                //update player
            }
        }

        private void FixedUpdate()
        {
            //update units
            enemy.Move();
        }
    }

    

    //run enemy behaviour/movement using JOB system
    public class EnemyManager
    {
        public EnemyManager(SpawnManager s)
        {
            spawn = s;
        }

        private SpawnManager spawn;

        public void Move()
        {
            ObjectPool<AEnemy>[] pools = spawn.GetPools();
            Debug.Log("<color=blue>Move Enemies</color>");
            for (int i = 0; i < pools.Length; i++)
            {
                List<AEnemy> temp = pools[i].GetActive();
                for (int j = 0; j < temp.Count; j++)
                {
                    temp[j].Move();
                }
            }
        }

        //Try to make above IJOB
        public struct EnemyMovement : IJobParallelFor
        {
            public void Execute(int index)
            {

            }
        }
    }


    

    //new player script...
    public class PlayerManager : MonoBehaviour
    {

    }
}


