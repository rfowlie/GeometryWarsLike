using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatsSetup : MonoBehaviour
{
    private GameStateInfo info;

    private void Start()
    {
        info = GameState.Instance.GetGameStateInfo();
        points.text = info.points.ToString();
        movement.text = info.levelMovement.ToString();
        //bullet.text = info.levelBullet.ToString();           
    }

    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private TextMeshProUGUI movement;
    [SerializeField] private TextMeshProUGUI bullet;


    public void IncrementMovement()
    {
        //check if enough points to increment...
        int amount = UpgradeCosts.Instance.GetMovementUpgrade(info.levelMovement);
        if (amount <= info.points)
        {
            info.points -= amount;
            points.text = info.points.ToString();
            info.levelMovement++;
            movement.text = info.levelMovement.ToString();

            //could just do at the end??? before loading new scene???
            GameState.Instance.UpdateGameStateInfo(info);
        }        
    }
}
