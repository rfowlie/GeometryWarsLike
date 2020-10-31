using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    private void OnEnable()
    {
        SurfacePlayer.DEATH += () => SceneManager.LoadScene(0);
    }

    //private void OnDisable()
    //{
    //    SurfacePlayer.DEATH -= () => SceneManager.LoadScene(0);
    //}

}
