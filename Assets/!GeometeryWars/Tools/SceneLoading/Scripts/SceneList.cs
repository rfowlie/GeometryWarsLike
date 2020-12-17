using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//component to place on game objects or colliders in the world to load/unload scenes in game
[CreateAssetMenu(fileName = "SceneList", menuName = "SO/Scene/SceneList")]
public class SceneList : ScriptableObject
{
    [SerializeField] public bool needsLoadScreen = false;

    [SerializeField] public int loadCount = 0;
    [SerializeField] public Object[] loadScenes;
    [SerializeField] public string[] loadNames;

    [SerializeField] public bool unloadAll = false;
    [SerializeField] public int unloadCount = 0;
    [SerializeField] public Object[] unloadScenes;
    [SerializeField] public string[] unloadNames;
}
