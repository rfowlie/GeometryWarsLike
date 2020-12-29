using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private int startingPoints = 100;
    public GameStateInfo info;

    private void Start()
    {
        info = new GameStateInfo();
        info.points = startingPoints;
        info.levelPlayer = 0;
        info.levelMovementSpeed = 0;
        info.levelFireRate = 0;
    }

    public void UpdateGameStateInfo(GameStateInfo newInfo)
    {
        info = newInfo;
        Debug.Log($"Points: {info.points}, Movement: {info.levelMovementSpeed}, Bullet: {info.levelFireRate}");
    }
}
