using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//public class SpawnPatternsWindow : EditorWindow
//{
//    [MenuItem("Tools/SpawnPatternCreator")]
//    public static void ShowWindow()
//    {
//        //this creates or gets the window for us
//        GetWindow<SpawnPatternsWindow>("SpawnPatternCreator");
//    }

//    void OnEnable()
//    {
//        SceneView.duringSceneGui += OnSceneGUI;
//    }

//    void OnDisable()
//    {
//        SceneView.duringSceneGui -= OnSceneGUI;
//    }

//    public Vector3 handlesPosition;
//    void OnSceneGUI(SceneView sceneView)
//    {
//        Quaternion temp = Quaternion.FromToRotation(handlesPosition, (handlesPosition - spawnPattern.target.position));
//        handlesPosition = Handles.PositionHandle(handlesPosition, temp);       
//    }

//    SpawnPattern spawnPattern = new SpawnPattern();
//    string name;
//    string assetPath;
//    Object folder;

//    private void OnGUI()
//    {
//        //window code here
//        EditorGUILayout.BeginVertical();
//        handlesPosition = EditorGUILayout.Vector3Field("Handle", handlesPosition);
//        spawnPattern.transform = EditorGUILayout.ObjectField("Start", spawnPattern.transform, typeof(Transform), true) as Transform;
//        spawnPattern.target = EditorGUILayout.ObjectField("Target", spawnPattern.target, typeof(Transform), true) as Transform;
//        name = EditorGUILayout.TextField("Name", name);
//        folder = EditorGUILayout.ObjectField("Folder", folder, typeof(Object));
//        assetPath = AssetDatabase.GetAssetPath(folder);
//        if (GUILayout.Button("Create"))
//        {
//            Debug.Log("Create Spawn Pattern");
//            SO_SpawnPattern temp = new SO_SpawnPattern(name, spawnPattern.p.ToArray());
//            AssetDatabase.CreateAsset(temp, assetPath + "/" + name + ".asset");
//        }
//        EditorGUILayout.EndVertical();
//        //EditorUtility.SetDirty(spawnPattern);
//    }
//}
