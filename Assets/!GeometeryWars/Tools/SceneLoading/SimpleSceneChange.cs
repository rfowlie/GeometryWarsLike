using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneChange : MonoBehaviour
{
    //[SerializeField] private string sceneName;
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
