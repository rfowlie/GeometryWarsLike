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
        [SerializeField] private TextMeshProUGUI points;
        [SerializeField] private TextMeshProUGUI movement;
        [SerializeField] private TextMeshProUGUI bullet;
        private GameStateInfo info;
        public GameStateInfo GetStats() { return info; }

        
        //*UPDATE THE UI, then on scene change, call event to notify GameController to grab updates
        public static event Action<StatsManager> START;

        public void SetStats(GameStateInfo i)
        {
            info = i;
            points.text = info.points.ToString();
            movement.text = info.levelMovementSpeed.ToString();
            //bullet.text = info.levelBullet.ToString();    
        }

        //movement
        public void IncrementMovement()
        {
            //check if enough points to increment...
            int amount = UpgradeCosts.Instance.GetMovementUpgrade(info.levelMovementSpeed);
            if (amount <= info.points)
            {
                info.points -= amount;
                points.text = info.points.ToString();
                info.levelMovementSpeed++;
                movement.text = info.levelMovementSpeed.ToString();

                //could just do at the end??? before loading new scene???
                GameStateSingleton.Instance.UpdateGameStateInfo(info);
            }
        }
    }
}

