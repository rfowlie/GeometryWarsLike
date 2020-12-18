using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    //scene controller for GeoWars
    public class SceneController : SceneControllerNonSingleton
    {
        [SerializeField] private LevelController LController;

        //listen for changes...
        private void OnEnable()
        {
            SceneChangeEvent.TRIGGERED += () => Debug.Log("Scene Change Event Triggered");
        }

        private void Start()
        {
            loadedScenes.Add(gameObject.scene.name);
            QueueLoad(new string[] { LController.GetBaseLevel() });
            Load();
        }
    }
}
