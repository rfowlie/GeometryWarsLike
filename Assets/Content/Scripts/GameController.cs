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
        //pass to levelManager on start, which then gives to DropManager
        [SerializeField] private SO_Drops drops;
        


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
                //fire the event of current registerd interaction
                InteractController.Instance.ExecuteCurrent();
                //interactControl.ExecuteCurrent();
            };
            input.Menu.Cursor.performed += (ctx) =>
            {
                //set the rate of change to input value
                cursorDelta = ctx.ReadValue<Vector2>();                
            };
            input.Menu.Cursor.canceled += (ctx) =>
            {
                //ensure cursor doesn't do any weird movement
                cursorDelta = Vector2.zero;
            };

            
            //get screen dimensions for clamping
            screenHeight = Screen.height;
            screenWidth = Screen.width;
            //make 10% of hypo
            screenSpeed = Mathf.Sqrt((screenHeight * screenHeight) + (screenWidth * screenWidth));
        }

        Vector2 cursorDelta;
        Vector2 cursorCurrentPos;
        float screenHeight;
        float screenWidth;
        float screenSpeed;
        [SerializeField] private float cursorSpeed = 2;

        private void Update()
        {
            //MOVE MOUSE WITH NEW INPUT SYSTEM            
            if (cursorDelta != Vector2.zero)
            {
                cursorCurrentPos += cursorDelta * cursorSpeed * screenSpeed * Time.deltaTime;
                cursorCurrentPos = new Vector2(Mathf.Clamp(cursorCurrentPos.x, 0f, screenWidth), Mathf.Clamp(cursorCurrentPos.y, 0f, screenHeight));
                Mouse.current.WarpCursorPosition(cursorCurrentPos);
            }
        }

        private void OnEnable()
        {
            //listen for SceneChange events
            SceneChanger.TRIGGER += (ctx) =>
            {
                if(ctx) { AdjustScene(); }
                else
                {
                    position = GamePosition.NONE;
                    AdjustScene();
                }
            };

            //listen for LevelController events
            LevelManager.START += SetupLevel;
            LevelManager.END += AdjustLevel;
            LevelManager.GAMEOVER += AdjustLevel;

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

            //pass all relevant info to new levelManager
            level.Setup(GetCurrentLevelPattern(), GetMap().transform, drops, GetStateInfo());
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
                        //hide cursor in level
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        levelControl.IncrementLevel();
                        //setup map...
                        map = Instantiate(maps.GetMapAtIndex(levelControl.GetCurrentLevelIndex()));
                        map.transform.position = Vector3.zero;
                        sceneControl.SceneChange(new string[] { levelControl.GetGameLevel() }, sceneControl.GetLoadedScenes());
                        break;
                    case GamePosition.LEVEL:
                        position = GamePosition.STATS;
                        //ensure cursor stays in game window
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                        
                        //load next level from level controller, unload main menu...
                        sceneControl.SceneChange(new string[] { levelControl.GetStatsMenu() }, sceneControl.GetLoadedScenes());
                        break;
                    case GamePosition.GAMEOVER:
                        position = GamePosition.NONE;
                        //ensure cursor stays in game window
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                        sceneControl.SceneChange(new string[] { levelControl.GetGameOverMenu() }, sceneControl.GetLoadedScenes());
                        break;
                    case GamePosition.NONE:
                        //ensure all values are default...
                        Restart();
                        //ensure cursor stays in game window
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
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