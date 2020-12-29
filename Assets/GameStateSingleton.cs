using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//hold game info for current session
public class GameStateSingleton : Singleton<GameStateSingleton>
{
    [SerializeField] private int startingPoints = 100;   
    private static GameStateInfo info;

    private void Start()
    {
        info = new GameStateInfo();
        info.points = startingPoints;
        info.levelMovementSpeed = 0;
        info.levelFireRate = 0;
    }

    public GameStateInfo GetGameStateInfo()
    {
        return info;
    }
    public void UpdateGameStateInfo(GameStateInfo newInfo) 
    {
        info = newInfo;
        Debug.Log($"Points: {info.points}, Movement: {info.levelMovementSpeed}, Bullet: {info.levelFireRate}");
    }
}

