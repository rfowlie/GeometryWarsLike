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

        [SerializeField] private PlayerManager player;
        private TimeManager timer;
        private PointsManager points;
        private SpawnManager spawn;
        private EnemyManager enemy;

        private bool isActive = false;

        //events
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
                
        private void Start()
        {
            //notify GameController of active levelManager
            START(this);

            //setup components
            timer = new TimeManager(20, timerUI);
            points = new PointsManager(pointsUI);
            spawn = new SpawnManager(GameController.Instance.GetCurrentLevelPattern(),
                                     GameController.Instance.GetMap().transform);
            enemy = new EnemyManager(spawn);

            //setup player
            player.Setup(playerHealthUI,
                         UpgradesController.Instance.GetHealtheValue(GameController.Instance.GetStateInfo().levelHealth),
                         UpgradesController.Instance.GetMovementValue(GameController.Instance.GetStateInfo().levelMovementSpeed),
                         UpgradesController.Instance.GetFireRateValue(GameController.Instance.GetStateInfo().levelFireRate));

            isActive = true;
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


