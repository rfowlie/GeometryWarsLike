using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace GeometeryWars
{
    //base class
    //controls all high level interactions
    //think water barrel at water park...
    public class GameController : Singleton<GameController>
    {
        //non singleton version of the scene conroller
        SceneController sceneControl;
        //holds ref to all levels
        LevelController levelControl;

        //contains all the information for the current game
        private GameStateInfo info;
        public GameStateInfo GetStateInfo() { return info; }

        //enum to track position of game
        public enum GamePosition { NONE, GAMEOVER, MAINMENU, STATS, LEVEL }
        public GamePosition position = GamePosition.NONE;


        protected override void Awake()
        {
            base.Awake();

            //attached to gameObject
            sceneControl = GetComponent<SceneController>();
            levelControl = GetComponent<LevelController>();
        }        

        private void OnEnable()
        {
            //listen for SceneChange events
            SceneChanger.TRIGGER += AdjustScene;

            //listen for LevelController events
            LevelManager.START += SetupLevel;
            LevelManager.END += AdjustLevel;
            LevelManager.GAMEOVER += GameOver;

            //statsManager
            StatsManager.START += SetupStats;
        }

        private void Start()
        {
            AdjustScene();
        }

        //set all values to default
        private void Restart()
        {
            level = null;
            stats = null;

            //setup game info
            info = new GameStateInfo();
            info.points = 0;
            info.levelPlayer = 0;
            info.levelMovementSpeed = 0;
            info.levelFireRate = 0;            
        }

        private void GameOver()
        {
            position = GamePosition.GAMEOVER;
            AdjustScene();
        }

        //STATSMANAGER
        StatsManager stats = null;
        private void SetupStats(StatsManager s)
        {
            //set this as current stats manager
            stats = s;
        }
        private void AdjustStats()
        {
            //grab updated info from statsManager, and set to game state
            info = stats.GetStats();
        }


        //LEVEL MANAGER
        //when new level loads, the active level manager will fire event when ready
        //game controller takes in the level manager and sets it as the active level manager, as well as 
        //its scene as the active scene
        //listens for levelmanager to fire off event indicating it is setup and active
        LevelManager level = null;
        private void SetupLevel(LevelManager nextLevel)
        {
            level = nextLevel;
            SceneManager.SetActiveScene(level.gameObject.scene);            
        }
        //when timer on level manager finishes, update points, remove levelmanager and update scene
        private void AdjustLevel()
        {
            //Debug.Log("<color=red>AdjustLevel</color>");
            if(level == null) { Debug.LogError("Level Manager is Null!!"); return; }

            //add points from level to total points
            info.points += level.GetPoints();

            //remove level manager
            level = null;

            //change to relevant scene
            AdjustScene();
        }

        //When a scene changer event fires, if scene can change move to correct scene using switch...
        bool canAdjust = true;
        private void AdjustScene()
        {
            if(canAdjust)
            {
                switch (position)
                {
                    case GamePosition.MAINMENU:
                        position = GamePosition.STATS;
                        //load next level from level controller, unload main menu...
                        sceneControl.SceneChange(new string[] { levelControl.GetStatsMenu() }, sceneControl.GetLoadedScenes());
                        break;
                    case GamePosition.STATS:
                        AdjustStats();
                        position = GamePosition.LEVEL;
                        sceneControl.SceneChange(new string[] { levelControl.NextLevel() }, sceneControl.GetLoadedScenes());
                        break;
                    case GamePosition.LEVEL:
                        position = GamePosition.STATS;
                        //load next level from level controller, unload main menu...
                        sceneControl.SceneChange(new string[] { levelControl.GetStatsMenu() }, sceneControl.GetLoadedScenes());
                        break;
                    case GamePosition.GAMEOVER:
                        position = GamePosition.NONE;
                        sceneControl.SceneChange(new string[] { levelControl.GetGameOverMenu() }, sceneControl.GetLoadedScenes());
                        break;
                    case GamePosition.NONE:
                        //ensure all values are default...
                        Restart();                      
                        position = GamePosition.MAINMENU;
                        sceneControl.SceneChange(new string[] { levelControl.GetMainMenu() }, sceneControl.GetLoadedScenes());
                        break;
                    default:
                        break;
                }
            }            
        }
    }
}