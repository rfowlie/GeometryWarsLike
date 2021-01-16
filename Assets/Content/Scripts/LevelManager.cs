using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


//control all aspects of in game level
namespace GeometeryWars
{
    public class LevelManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] public TextMeshProUGUI timerUI;
        [SerializeField] public TextMeshProUGUI pointsUI;
        [SerializeField] public RectTransform playerHealthUI;
        [Header("Player")]
        [SerializeField] private PlayerManager player;

        //components
        private TimeManager timer;
        private PointsManager points;
        private SpawnManager spawn;
        private EnemyManager enemy;
        private DropManager drop;

        private bool isActive = false;

        //events
        public static event Action<LevelManager> START;
        public static event Action END;
        public static event Action GAMEOVER;


        public void Setup(SO_LevelPattern levelPattern, Transform map, SO_Drops allDrops, GameStateInfo info)
        {
            //setup components
            timer = new TimeManager(timerUI, 45);
            points = new PointsManager(pointsUI);

            spawn = new SpawnManager(levelPattern, map);
            enemy = new EnemyManager(spawn);
            drop = new DropManager(allDrops, info.levelDropRate);

            //setup player
            player.Setup(playerHealthUI, info);


            isActive = true;
        }
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
                
        private void Start()
        {
            //notify GameController of active levelManager
            START(this);            
        }

        private void Update()
        {
            //if level is active...
            if(isActive)
            {
                //run timer..check if level finished
                if (timer.AdjustTime(Time.deltaTime))
                {
                    isActive = false;
                    
                    //level finished
                    END();
                }

                //run spawner
                spawn.Execute(timer.GetTimeFromStart());
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


