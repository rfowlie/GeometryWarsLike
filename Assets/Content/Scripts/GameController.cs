using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.InputSystem;


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

        //Scriptable Objects
        [Header("Scriptable Objects")]
        public SO_Maps maps;
        public SO_Upgrades upgrades;
        [SerializeField] private SO_GameLevelPatterns levelPatterns;
        public SO_LevelPattern GetCurrentLevelPattern()
        {
            return levelPatterns.GetPatternAtIndex(levelControl.GetCurrentLevelIndex());
        }

        [Space]
        [Header("Global Variables")]
        [SerializeField] private LayerMask mapLayer;
        public LayerMask GetMapLayer() { return mapLayer; }
        [SerializeField] private LayerMask obstacleLayer;
        public LayerMask GetObstacleLayer() { return obstacleLayer; }
        [SerializeField] private GameObject map;
        public GameObject GetMap() { return map; }


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


            //NEW INPUT TEST
            input = new InputAction_01();
            input.Menu.Enable();
            input.Menu.Select.performed += (ctx) =>
            {
                //fire normal left mouse button
                //Mouse.current.IsPressed();
            };
            input.Menu.Cursor.performed += (ctx) =>
            {
                Debug.Log($"<color=blue>Current: {Mouse.current.position.ReadValue()}</color>");
                Debug.Log($"<color=red>Input: {ctx.ReadValue<Vector2>()}</color>");
                Debug.Log($"<color=green>Input: {Mouse.current.position.ReadValue() + ctx.ReadValue<Vector2>()}</color>");

                Vector2 pos = Mouse.current.position.ReadValue() + ctx.ReadValue<Vector2>() * 10f;
                Mouse.current.WarpCursorPosition(pos);

                currentCursorChange = ctx.ReadValue<Vector2>();                
            };
            input.Menu.Cursor.canceled += (ctx) =>
            {
                currentCursorChange = Vector2.zero;
            };
            
            Cursor.lockState = CursorLockMode.Confined;
            cursorCurrentPos = Mouse.current.position.ReadValue();
            screenHeight = Screen.height;
            screenWidth = Screen.width;
        }

        Vector2 currentCursorChange;
        Vector2 cursorCurrentPos;
        float screenHeight;
        float screenWidth;

        private void Update()
        {
            //MOVE MOUSE WITH NEW INPUT SYSTEM
            Debug.Log($"<color=purple>Mouse: {Mouse.current.position.ReadValue()}</color>");
            Mouse.current.WarpCursorPosition(cursorCurrentPos);

            if (currentCursorChange != Vector2.zero)
            {
                cursorCurrentPos += currentCursorChange * 4f;
                cursorCurrentPos = new Vector2(Mathf.Clamp(cursorCurrentPos.x, 0f, screenWidth), Mathf.Clamp(cursorCurrentPos.y, 0f, screenHeight));
                Mouse.current.WarpCursorPosition(cursorCurrentPos);
            }
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

        InputAction_01 input;
        private void Start()
        {
            AdjustScene();            
        }

        //set all values to default
        private void Restart()
        {
            levelControl.Restart();

            level = null;
            stats = null;

            //setup game info
            info = new GameStateInfo();
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
            if(level != null)
            {
                Debug.LogError("There is more then one levelManager in the scene!!");
            }
            level = nextLevel;
            SceneManager.SetActiveScene(level.gameObject.scene);
            //move map to active scene
            SceneManager.MoveGameObjectToScene(map, SceneManager.GetActiveScene()); 
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

            //stop all coroutines
            StopAllCoroutines();

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
                        levelControl.IncrementLevel();
                        //setup map...
                        map = Instantiate(maps.GetMapAtIndex(levelControl.GetCurrentLevelIndex()));
                        map.transform.position = Vector3.zero;
                        sceneControl.SceneChange(new string[] { levelControl.GetGameLevel() }, sceneControl.GetLoadedScenes());
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