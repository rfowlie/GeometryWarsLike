using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnCreator : EditorWindow
{
    int amountOfPoints;
    float radius;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/SpawnCreator")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(SpawnCreator));
        window.ShowAuxWindow();

        // Get existing open window or if none, make a new one:
        //SpawnCreator window = (SpawnCreator)GetWindow(typeof(SpawnCreator));
        //window.Show();
    }

    private void OnEnable()
    {
        SceneView.beforeSceneGui += OnSceneGUI;
    }
    private void OnDisable()
    {
        SceneView.beforeSceneGui -= OnSceneGUI;
    }

    Vector3 worldPosition;
    void OnSceneGUI(SceneView sceneView)
    {
        // Do your drawing here using Handles.
        worldPosition = Handles.PositionHandle(worldPosition, Quaternion.identity);
        Vector3[] points = ShapeCreator.Circle(amountOfPoints, radius, Vector3.up, Vector3.forward);
        for (int i = 0; i < points.Length; i++)
        {
            Handles.DrawSphere(1, worldPosition + points[i], Quaternion.identity, 1f);
        }

        Handles.BeginGUI();
        // Do your drawing here using GUI.        
        
        Handles.EndGUI();
    }

    private void OnGUI()
    {
        worldPosition = EditorGUILayout.Vector3Field("World Position", worldPosition);
        amountOfPoints = EditorGUILayout.IntField("Amount Of Points", amountOfPoints);
        radius = EditorGUILayout.FloatField("Radius", radius);
    }
}
