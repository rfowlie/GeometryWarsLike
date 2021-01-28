using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class PracticeEditorWindow : EditorWindow
{
    Texture2D headerTexture;
    Texture2D bodyTexture;

    Color headerSectionColour = new Color(128, 128, 128, 1f);
    Rect header;
    Rect body;


    [MenuItem("Window/Tools/PracticeMenu")]
    public static void OpenWindow()
    {
        PracticeEditorWindow window = GetWindow<PracticeEditorWindow>("Practice Window", true);
        //Vector2 size = new Vector2(200, 300);
        //window.minSize = size;
        //window.maxSize = size;
    }

    //same as in monobehaviours
    private void OnEnable()
    {
        InitTextures();
    }

    //load/create all our textures when this window is enabled
    private void InitTextures()
    {
        headerTexture = new Texture2D(1, 1);
        headerTexture.SetPixel(0, 0, headerSectionColour);
        headerTexture.Apply();

        bodyTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Sprites/PlusSign.jpg");
        bodyTexture.Apply();
    }



    //similiar to update 
    //not called once per frame but 1 or more time per interaction

    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
    }

    //we will define rect values in here
    private void DrawLayouts()
    {
        //set size of rects in window
        header.x = 0;
        header.y = 0;
        header.width = position.width;
        header.height = 40;

        body.x = 0;
        body.y = header.height;
        body.width = position.width;
        body.height = position.height - header.height;

        
        GUI.DrawTexture(header, headerTexture);
        GUI.DrawTexture(body, bodyTexture);
    }

    private string textField;
    //draw contents of header
    private void DrawHeader()
    {
        GUILayout.BeginArea(body);
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        if(GUILayout.Button("Rotate Selected Transform"))
        {
            Transform temp = Selection.activeTransform;
            if(temp != null) { temp.rotation = Random.rotation; }
        }
        GUILayout.Label("Label");
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        textField = GUILayout.TextField(textField);
        if(GUILayout.Button("Write Field to Console"))
        {
            Debug.Log(textField);
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

}
