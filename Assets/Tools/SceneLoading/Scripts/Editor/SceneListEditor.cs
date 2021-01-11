using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(SceneList))]
public class SceneListEditor : Editor
{
    SceneList sl;

    private void OnEnable()
    {
        sl = (SceneList)target;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        if (sl.loadScenes == null || sl.unloadScenes == null)
        {
            sl.loadScenes = new Object[0];
            sl.loadNames = new string[0];
            sl.unloadScenes = new Object[0];
            sl.unloadNames = new string[0];
        }

        EditorGUILayout.LabelField("SCENE LIST", EditorStyles.boldLabel);
        EditorGUILayout.Space(15);
        EditorGUILayout.BeginVertical();
        sl.needsLoadScreen = EditorGUILayout.Toggle("Needs Load Screen", sl.needsLoadScreen);
        EditorGUILayout.Space(10);
        

        Load();
        EditorGUILayout.Space(30);
        
        EditorGUILayout.LabelField("Unload Scenes", EditorStyles.boldLabel);
        sl.unloadAll = EditorGUILayout.Toggle(new GUIContent("Unload All", "Will unload all scenes except BOOT"), sl.unloadAll);

        if(sl.unloadAll == false)
        {
            Unload();
        }        

        EditorGUILayout.EndVertical();
        EditorUtility.SetDirty(sl);
    }

    private void Load()
    {
        EditorGUILayout.LabelField("Load Scenes", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add", GUILayout.MaxWidth(250)))
        {
            sl.loadCount++;
            sl.loadScenes = ArrayEX.ResizeArray(sl.loadCount, sl.loadScenes);
            sl.loadNames = ArrayEX.ResizeArray(sl.loadCount, sl.loadNames);
        }
        if (GUILayout.Button("Remove", GUILayout.MaxWidth(350)))
        {
            if (sl.loadCount > 0)
            {
                sl.loadCount--;
                sl.loadScenes = ArrayEX.ResizeArray(sl.loadCount, sl.loadScenes);
                sl.loadNames = ArrayEX.ResizeArray(sl.loadCount, sl.loadNames);
            }
        }
        if (GUILayout.Button("Reset", GUILayout.MaxWidth(250)))
        {
            sl.loadCount = 0;
            sl.loadScenes = ArrayEX.ResizeArray(sl.loadCount, sl.loadScenes);
            sl.loadNames = ArrayEX.ResizeArray(sl.loadCount, sl.loadNames);
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(16);

        if (sl.loadCount > 0)
        {
            for (int i = 0; i < sl.loadCount; i++)
            {
                sl.loadScenes[i] = EditorGUILayout.ObjectField(sl.loadScenes[i], typeof(Object), true);
                if (sl.loadScenes[i] != null)
                {
                    sl.loadNames[i] = sl.loadScenes[i].name;
                    //EditorGUILayout.TextField(sl.names[i]);
                }
            }
        }
    }

    private void Unload()
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add"))
        {
            sl.unloadCount++;
            sl.unloadScenes = ArrayEX.ResizeArray(sl.unloadCount, sl.unloadScenes);
            sl.unloadNames = ArrayEX.ResizeArray(sl.unloadCount, sl.unloadNames);
        }
        if (GUILayout.Button("Remove"))
        {
            if (sl.loadCount > 0)
            {
                sl.unloadCount--;
                sl.unloadScenes = ArrayEX.ResizeArray(sl.unloadCount, sl.unloadScenes);
                sl.unloadNames = ArrayEX.ResizeArray(sl.unloadCount, sl.unloadNames);
            }
        }
        if (GUILayout.Button("Reset"))
        {
            sl.unloadCount = 0;
            sl.unloadScenes = ArrayEX.ResizeArray(sl.unloadCount, sl.unloadScenes);
            sl.unloadNames = ArrayEX.ResizeArray(sl.unloadCount, sl.unloadNames);
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(16);

        if (sl.unloadCount > 0)
        {
            for (int i = 0; i < sl.unloadCount; i++)
            {
                sl.unloadScenes[i] = EditorGUILayout.ObjectField(sl.unloadScenes[i], typeof(Object), true);
                if (sl.unloadScenes[i] != null)
                {
                    sl.unloadNames[i] = sl.unloadScenes[i].name;
                    //EditorGUILayout.TextField(sl.names[i]);
                }
            }
        }
    }
}
