using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


namespace GeometeryWars
{
    public class StatsManager : MonoBehaviour
    {
        private UpgradeCosts costs;

        //UI vars
        [SerializeField] private TextMeshProUGUI points;
        [SerializeField] private TextMeshProUGUI movement;
        [SerializeField] private TextMeshProUGUI bullet;
        private GameStateInfo info;
        public GameStateInfo GetStats() { return info; }

        
        //*UPDATE THE UI, then on scene change, call event to notify GameController to grab updates
        public static event Action<StatsManager> START;

        private void Start()
        {
            costs = GetComponent<UpgradeCosts>();
        }
        private void OnEnable()
        {
            //notify game controller that active and that the game controller needs to set the game stats
            START(this);

            //get game stats from game controller
            info = GameController.Instance.GetState();
            points.text = info.points.ToString();
            movement.text = info.levelMovementSpeed.ToString();
            //bullet.text = info.levelBullet.ToString();    
        }

        //movement
        public void IncrementMovement()
        {
            //ensure that movement can be upgraded
            if(info.levelMovementSpeed < costs.GetMovementCount())
            {
                //check if enough points to increment...
                int amount = costs.GetMovementUpgrade(info.levelMovementSpeed);
                if (amount <= info.points)
                {
                    info.points -= amount;
                    points.text = info.points.ToString();
                    info.levelMovementSpeed++;
                    movement.text = info.levelMovementSpeed.ToString();
                }
            }
        }
    }
}

