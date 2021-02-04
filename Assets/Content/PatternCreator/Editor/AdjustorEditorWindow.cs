using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PatternCreator;


public class AdjustorEditorWindow : EditorWindow
{
    [MenuItem("Window/Tools/Adjustor")]
    static void OpenWindow()
    {
        window = GetWindow<AdjustorEditorWindow>("Adjustor Window");

        GameObject temp = new GameObject("ADJUSTOR");
        adjustor = temp.AddComponent<PatternCreator.Pointer>();
    }

    static AdjustorEditorWindow window;
    private static PatternCreator.Pointer adjustor;
    private PatternCreator.SO_PatternInfoContainer container;
    private Transform map;
    private Vector3[][] points;

    private void OnGUI()
    {
        map = (Transform)EditorGUILayout.ObjectField("Map", map, typeof(Transform));
        container = (PatternCreator.SO_PatternInfoContainer)EditorGUILayout.ObjectField("PatternInfoContainer", container, typeof(PatternCreator.SO_PatternInfoContainer));
       
        if(GUILayout.Button("Load"))
        {
            //points = adjustor.CalculateAll(map, container);
        }

        if (points != null)
        {
            DrawPoints();
        }
    }

    private void DrawPoints()

    {

    }
    //custom function to assign to scene view delegate
    private void DrawHandles(SceneView s)
    {
        if (container != null && points != null)
        {
            for (int i = 0; i < points.Length; i++)
            {
                for (int j = 0; j < points[i].Length; j++)
                {
                    //can't use gizmos here....
                    //Gizmos.DrawSphere(points[i][j], 0.5f);
                    Handles.DrawSphere(0, points[i][j], Quaternion.identity, 0.5f);
                }
            }
        }
    }

    private void OnFocus()
    {
        //add sceneView delegate
        SceneView.duringSceneGui += this.DrawHandles;
    }
    private void OnLostFocus()
    {
        //remove scenView delegate
        SceneView.duringSceneGui -= this.DrawHandles;
    }
    private void OnDestroy()
    {
        //remove scene view delegate
        SceneView.duringSceneGui -= this.DrawHandles;
        DestroyImmediate(adjustor.gameObject);
    }

}
