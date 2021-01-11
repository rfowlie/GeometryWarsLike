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
        //serializable object that holds concrete upgrade costs
        private UpgradesController costs;

        //UI vars
        [SerializeField] private TextMeshProUGUI points;
        [SerializeField] private TextMeshProUGUI movement;
        [SerializeField] private TextMeshProUGUI fireRate;
        private GameStateInfo info;
        public GameStateInfo GetStats() { return info; }

        
        //*UPDATE THE UI, then on scene change, call event to notify GameController to grab updates
        public static event Action<StatsManager> START;

        private void Start()
        {
            costs = UpgradesController.Instance;
        }
        private void OnEnable()
        {
            //notify game controller that active and that the game controller needs to set the game stats
            START(this);

            //get game stats from game controller
            info = GameController.Instance.GetStateInfo();
            points.text = info.points.ToString();
            movement.text = info.levelMovementSpeed.ToString();
            fireRate.text = info.levelFireRate.ToString();    
        }

        //movement
        public void IncrementMovement()
        {
            //ensure that movement can be upgraded
            if(info.levelMovementSpeed < costs.GetMovementLevels() - 1)
            {
                //check if enough points to increment...
                int amount = costs.GetMovementCost(info.levelMovementSpeed);
                if (amount <= info.points)
                {
                    info.points -= amount;
                    points.text = info.points.ToString();
                    info.levelMovementSpeed++;
                    movement.text = info.levelMovementSpeed.ToString();
                }
            }
        }

        public void IncrementFireRate()
        {
            if(info.levelFireRate < costs.GetFireRateLevels() - 1)
            {
                int amount = costs.GetFireRateCost(info.levelFireRate);
                if(amount <= info.points)
                {
                    info.points -= amount;
                    points.text = info.points.ToString();
                    info.levelFireRate++;
                    fireRate.text = info.levelFireRate.ToString();
                }
            }
        }
    }
}

