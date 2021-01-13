using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//control all aspects of in game level
namespace GeometeryWars
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TimeManager time;
        [SerializeField] private PointsManager points;
        [SerializeField] private SpawnManager spawn;
        [SerializeField] private PlayerManager player;
        private EnemyManager enemy;


        private bool isActive = false;

        public static event Action<LevelManager> START;
        public static event Action END;
        public static event Action GAMEOVER;

        //get the current points for this level
        public int GetPoints()
        {
            return points.points;
        }

        //Closing steps for when timer is up and level ends
        public void PrepareLevelOver()
        {
            //stop updating units and player
            isActive = false;
            GAMEOVER();
        }

        private void OnEnable()
        {
            player.DEATH += PrepareLevelOver;
        }

        private void Awake()
        {
            time = GetComponent<TimeManager>();
            points = GetComponent<PointsManager>();
            spawn = GetComponent<SpawnManager>();
        }

        private void Start()
        {
            START(this);
            player.Setup(UpgradesController.Instance.GetMovementValue(GameController.Instance.GetStateInfo().levelMovementSpeed),
                         UpgradesController.Instance.GetFireRateValue(GameController.Instance.GetStateInfo().levelFireRate));
            enemy = new EnemyManager(spawn);
            isActive = true;
        }

        private void Update()
        {
            //if level is active...
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
                player.UpdatePlayer();
            }
        }

        public bool IJob = false;
        private void FixedUpdate()
        {
            if(isActive)
            {
                if(IJob)
                {
                    enemy.MoveIJob();
                }
                else
                {
                    //move enemies
                    enemy.Move();
                }

                //move player
                player.Move();
            }
        }
    }

    

    
}


