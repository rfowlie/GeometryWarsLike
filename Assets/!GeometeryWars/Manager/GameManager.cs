using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GeometeryWars
{
    public class GameManager : MonoBehaviour
    {
        //for now reset the scene...
        private void OnEnable()
        {
            SurfacePlayer.DEATH += () => SceneManager.LoadScene(0);
        }

        //private void OnDisable()
        //{
        //    SurfacePlayer.DEATH -= () => SceneManager.LoadScene(0);
        //}
    }
}


