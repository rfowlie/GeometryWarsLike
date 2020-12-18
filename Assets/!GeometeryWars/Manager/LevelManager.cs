using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


namespace GeometeryWars
{
    public class LevelManager : MonoBehaviour
    {       
        //for now reset the scene...
        private void OnEnable()
        {
            SurfacePlayer.DEATH += () => SceneManager.LoadScene(0);

            //listen for timer to finish, complete current level
            Timer.FINISH += () => LevelComplete();
        }
               

        [SerializeField] private SpawnManager spawnManager;
        [SerializeField] private PointsManager pointsManager;

        //setup pieces from Persistent
        private void Start()
        {
            //Better spot to do this??? to much access...
            SceneManager.SetActiveScene(gameObject.scene);
            spawnManager.Setup();
            pointsManager.Adjust(GameState.Instance.GetGameStateInfo().points);
        }

        //notify that this level is finished, and clean up done...
        public static event Action LEVELFINISHED;
        private void LevelComplete()
        {
            //update score
            GameStateInfo info = GameState.Instance.GetGameStateInfo();
            info.points = pointsManager.points;
            GameState.Instance.UpdateGameStateInfo(info);

            //NO BUSINESS CHANGING SCENES
            //Tools.SceneControllerSingleton.Instance?.SceneChange(new string[] { "Stats" }, new string[] { gameObject.scene.name });

            LEVELFINISHED();
        }
    }
}


