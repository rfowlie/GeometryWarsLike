using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TransformationsWindow : EditorWindow
{
    //vars
    [SerializeField] private float scale = 1f;
    [SerializeField] private float rotation = 1f;
    

    [MenuItem("Window/Tools/RandomTransformations")]
    public static void OpenWindow()
    {
        TransformationsWindow window = GetWindow<TransformationsWindow>("Adjustor Window");
        window.minSize = new Vector2(300, 200);
        window.maxSize = new Vector2(300, 200);
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
            ApplyToSelectedGameObjects((GameObject i) =>
            {
                i.transform.rotation = Quaternion.Euler(new Vector3((Random.value - 0.5f) * rotation, (Random.value - 0.5f) * rotation, (Random.value - 0.5f) * rotation));
            });            
        }
        rotation = EditorGUILayout.Slider("Rotation Amount", rotation, 0, 360);

        EditorGUILayout.BeginHorizontal(style);
        if(GUILayout.Button("Random Scale"))
        {
            ApplyToSelectedGameObjects((GameObject i) => i.transform.localScale = scale * new Vector3(Random.value, Random.value, Random.value));
        }
        scale = EditorGUILayout.FloatField("Scale Multiplier", scale);
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


