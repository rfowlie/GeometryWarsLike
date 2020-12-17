using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GeometeryWars
{
    public class LevelManager : MonoBehaviour
    {       
        //for now reset the scene...
        private void OnEnable()
        {
            SurfacePlayer.DEATH += () => SceneManager.LoadScene(0);
            Timer.FINISH += () => LevelComplete();
        }

        [SerializeField] private SpawnManager spawnManager;
        [SerializeField] private PointsManager pointsManager;

        //setup pieces from Persistent
        private void Start()
        {
            SceneManager.SetActiveScene(gameObject.scene);
            spawnManager.Setup();
            pointsManager.Adjust(GameState.Instance.GetGameStateInfo().points);
        }

        private void LevelComplete()
        {
            //update score
            GameStateInfo info = GameState.Instance.GetGameStateInfo();
            info.points = pointsManager.points;
            GameState.Instance.UpdateGameStateInfo(info);
            //scenes
            SceneController.Instance?.SceneChange(new string[] { "Stats" }, new string[] { gameObject.scene.name });
        }
    }
}


