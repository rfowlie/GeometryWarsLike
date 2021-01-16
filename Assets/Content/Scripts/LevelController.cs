using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Contains and keeps track of all levels
public class LevelController : MonoBehaviour
{
    public Scene Boot { get; private set; }

    [SerializeField] private string mainMenu;
    public string GetMainMenu() { return mainMenu; }
    [SerializeField] private string statsMenu;
    public string GetStatsMenu() { return statsMenu; }
    [SerializeField] private string gameOverMenu;
    public string GetGameOverMenu() { return gameOverMenu; }

    [SerializeField] private string gameLevel;
    public string GetGameLevel() { return gameLevel; }


    [Space]
    [SerializeField] private int maxLevels = 2;
    [SerializeField] private int currentlevelIndex = -1;
    public int GetCurrentLevelIndex() { return currentlevelIndex; }

    
    private void Start()
    {
        //get persistent
        Boot = gameObject.scene;
    }
            
    public void IncrementLevel()
    {
        currentlevelIndex = (currentlevelIndex + 1) % maxLevels;
    }

    public void Restart()
    {
        currentlevelIndex = -1;
    }
}
