    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



//handles as many incoming scene changes as possible while maintaining the BOOT scene
public class SceneController : Singleton<SceneController>
{
    [SerializeField] private string boot = "BOOT";
    [SerializeField] private string firstlevel = "FirstLevel";
    string activeScene;
    List<string> loadedScenes;
    List<string> scenesToBeLoaded;
    List<string> scenesToBeUnloaded;
    bool needsLoadScreen = false;

    protected static Action LoadingBegin;
    protected static Action LoadingFinished;

    protected override void Awake()
    {
        base.Awake();

        loadedScenes = new List<string>();
        scenesToBeLoaded = new List<string>();
        scenesToBeUnloaded = new List<string>();
    }

    private void Start()
    {
        loadedScenes.Add(boot);
        QueueLoad(new string[] { firstlevel });
        Load();        
    }

    #region Methods
    //not using sceneList
    public void SceneChange(string[] load, string[] unload, bool isLoadScreen = false)
    {
        if(unload != null) { QueueUnload(unload); }        
        if(unload != null) { QueueLoad(load); }        
        if(isLoadScreen) { ActivateLoadScreen(); }
        Activate();
    }
    //public accessor with order of operations
    public void SceneChange(SceneList sceneChange)
    {
        //SETUP
        //check if unloading all scenes
        if (sceneChange.unloadAll)
        {
            UnloadAll();
        }
        //add scenes to be unloaded
        if (sceneChange.unloadNames != null)
        {
            QueueUnload(sceneChange.unloadNames);
        }
        //add scenes to be loaded
        if (sceneChange.loadNames != null)
        {
            QueueLoad(sceneChange.loadNames);
        }

        //determine loadscreen
        if (sceneChange.needsLoadScreen)
        {
            ActivateLoadScreen();
        }

        Activate();
    }
    private void Activate()
    {
        //fire off event to notify loading started
        if (LoadingBegin != null)
        {
            LoadingBegin();
        }

        //begin loading/unloading
        if (scenesToBeLoaded.Count > 0)
        {
            Load();
        }
        if (scenesToBeUnloaded.Count > 0)
        {
            Unload();
        }
    }
    //add scenes to load list
    private void QueueLoad(string[] newScenes)
    {
        //vet all passed in scenes and assign them to scenes to be loaded
        for (int i = 0; i < newScenes.Length; i++)
        {
            //check if scene already loaded or lined up to be loaded
            if (loadedScenes.Contains(newScenes[i]) == false &&
                scenesToBeLoaded.Contains(newScenes[i]) == false)
            {
                scenesToBeLoaded.Add(newScenes[i]);
            }
        }
    }
    //add scenes to unload list
    private void QueueUnload(string[] unload)
    {
        //vet all passed in scenes and assign them to scenes to be loaded
        for (int i = 0; i < unload.Length; i++)
        {
            //check if scene being loaded already loaded
            if (loadedScenes.Contains(unload[i]) == true &&
                scenesToBeUnloaded.Contains(unload[i]) == false)
            {
                scenesToBeUnloaded.Add(unload[i]);
            }
        }
    }
    //unload all except BOOT
    private void UnloadAll()
    {
        scenesToBeUnloaded = new List<string>(loadedScenes);
        //prevent boot from being unloaded...
        scenesToBeUnloaded.Remove(boot);
    }
    
    
    private void Load()
    {
        //always additive as we always want BOOT and no weird glitches
        AsyncOperation ao = SceneManager.LoadSceneAsync(scenesToBeLoaded[0], LoadSceneMode.Additive);
        //check if failed, in which case remove from list
        if (ao == null)
        {
            Debug.Log(ao.ToString() + "didn't load/ doesn't exist");
            scenesToBeLoaded.RemoveAt(0);
            if(scenesToBeLoaded.Count > 0)
            {
                Load();
            }
        }
        //else add to loaded scenes, remove from to be loaded, and attach method to event
        else
        {
            loadedScenes.Add(scenesToBeLoaded[0]);
            scenesToBeLoaded.RemoveAt(0);
            ao.completed += LoadComplete;
        }        
    }

    private void LoadComplete(AsyncOperation ao)
    {
        Debug.Log(ao.ToString() + "is loaded");

        if (scenesToBeLoaded.Count > 0)
        {
            Load();
        }
        else
        {
            Finished();
        }
    }
    
    private void Unload()
    {
        //always additive as we always want BOOT and no weird glitches
        AsyncOperation ao = SceneManager.UnloadSceneAsync(scenesToBeUnloaded[0]);
        //check if failed 
        if (ao == null)
        {
            Debug.Log(ao.ToString() + "didn't unload/ doesn't exist");
            scenesToBeUnloaded.RemoveAt(0);
            if (scenesToBeUnloaded.Count > 0)
            {
                Unload();
            }
        }
        //adjust lists and attach method to event
        else
        {
            loadedScenes.Remove(scenesToBeUnloaded[0]);
            scenesToBeUnloaded.RemoveAt(0);
            ao.completed += UnloadComplete;
        }   
    }

    private void UnloadComplete(AsyncOperation ao)
    {
        Debug.Log(ao.ToString() + " is unloaded");

        if (scenesToBeUnloaded.Count > 0)
        {
            Unload();
        }
        else
        {
            Debug.Log("All scenes finished Unloading");

            Finished();
        }
    }

    private void Finished()
    {
        //this will get fired twice if unloading and loading
        //second time will guarentee trigger the load screen
        if (scenesToBeLoaded.Count == 0 && scenesToBeUnloaded.Count == 0)
        {
            //Debug
            //Debug.LogError($"Count:{SceneManager.sceneCount}");
            //for (int i = 0; i < SceneManager.sceneCount; i++)
            //{
            //    Debug.LogError($"{i}:{SceneManager.GetSceneAt(i).name}");
            //}

            if (needsLoadScreen)
            {
                DeactivateLoadScreen();
            }

            //fire off event to notify outside parties that all loading is finished
            if(LoadingFinished != null)
            {
                LoadingFinished();                
            }
        }
    }

    //do all the loading screen shit
    private void ActivateLoadScreen()
    {
        //turn on load screen

        //pause game time
    }
    private void DeactivateLoadScreen()
    {

    }

    #endregion
}
