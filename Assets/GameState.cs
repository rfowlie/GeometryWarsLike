using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//hold game info for current session
public class GameState : Singleton<GameState>
{
    [SerializeField] private int startingPoints = 100;   
    private static GameStateInfo info;

    private void Start()
    {
        info = new GameStateInfo();
        info.points = startingPoints;
        info.levelMovement = 0;
        info.levelBullet = 0;
    }

    public GameStateInfo GetGameStateInfo()
    {
        return info;
    }
    public void UpdateGameStateInfo(GameStateInfo newInfo) 
    {
        info = newInfo;
        Debug.Log($"Points: {info.points}, Movement: {info.levelMovement}, Bullet: {info.levelBullet}");
    }
}

public struct GameStateInfo
{
    public int points;
    public int levelMovement;
    public int levelBullet;
}

