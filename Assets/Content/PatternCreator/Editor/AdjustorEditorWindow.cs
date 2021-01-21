using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class AdjustorEditorWindow : EditorWindow
{
    //vars
    private static GameObject controller;
    [SerializeField] private float scale = 1f;

    [MenuItem("Window/Tools/Adjustor")]
    public static void OpenWindow()
    {
        AdjustorEditorWindow window = GetWindow<AdjustorEditorWindow>("Adjustor Window");
        window.minSize = new Vector2(300, 200);
        window.maxSize = new Vector2(300, 200);


        controller = new GameObject("Controller");
        controller.transform.position = Vector3.zero;
    }

    private void OnFocus()
    {
        if (controller != null) { controller.SetActive(true); }
    }

    private void OnLostFocus()
    {
        if (controller != null) { controller.SetActive(false); }
    }

    private void OnDestroy()
    {
        DestroyImmediate(controller); 
    }
    

    //handles rendering
    private void OnGUI()
    {
        //setup style...
        GUIStyle style = new GUIStyle();
        style.padding = new RectOffset(0, 0, 4, 4);
        style.stretchWidth = true;

        //guiLayout holds lots of options for formatting
        GUILayout.Label("Welcome to my editor");

        //buttons return bools
        if (GUILayout.Button("Random Rotation"))
        {
            ApplyToSelectedGameObjects((GameObject i) => i.transform.rotation = Random.rotation);
        }

        EditorGUILayout.BeginHorizontal(style);
        if(GUILayout.Button("Random Scale"))
        {
            ApplyToSelectedGameObjects((GameObject i) => i.transform.localScale = scale * new Vector3(Random.value, Random.value, Random.value));
        }
        scale = EditorGUILayout.FloatField("Scale", scale);
        EditorGUILayout.EndHorizontal();
    }

    //apply something to all selected gameObjects...
    private void ApplyToSelectedGameObjects(System.Action<GameObject> action)
    {
        foreach(GameObject o in Selection.gameObjects)
        {
            action(o);
        }
    }
}


