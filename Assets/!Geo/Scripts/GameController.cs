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
    public class GameController : MonoBehaviour
    {
        SceneController scene;
        GameState state;
        LevelManager level = null;

        private void Awake()
        {
            scene = GetComponent<SceneController>();
            state = GetComponent<GameState>();
        }

        private enum GamePosition { NONE, MAINMENU, STATS, LEVEL }
        private GamePosition position = GamePosition.NONE;

        private void OnEnable()
        {
            //listen for SceneChange events
            SceneChange.TRIGGER += AdjustScene;

            //listen for LevelController events
            LevelManager.ACTIVE += SetupLevel;
            LevelManager.DEACTIVE += () => level = null;
        }

        private void Start()
        {
            //load the main menu
            position = GamePosition.MAINMENU;
            scene.SceneChange(new string[] { "MainMenu" }, null);
        }

        private void SetupLevel(LevelManager nextLevel)
        {
            level = nextLevel;
            SceneManager.SetActiveScene(level.gameObject.scene);
        }

        bool canAdjust = true;
        private void AdjustScene()
        {
            if(canAdjust)
            {
                switch (position)
                {
                    case GamePosition.MAINMENU:
                        position = GamePosition.STATS;
                        //load next level from level manager...
                        scene.SceneChange(new string[] { "DemoScene_WithObstacles" }, new string[] { "MainMenu" });
                        break;
                    case GamePosition.STATS:
                        break;
                    case GamePosition.LEVEL:
                        break;
                    default:
                        break;
                }
            }            
        }
    }
}