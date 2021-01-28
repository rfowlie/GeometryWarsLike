using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;


public class GameDataObjectEditorWindow : EasyEditorWindow
{
    //EPIC
    //callback that will allow your SO to open its editor window
    //Doesn't even need its own custom editor
    [OnOpenAsset(0)]
    public static bool OpenAsset(int instanceID, int line)
    {
        //try casting to correct type
        SO_Test obj = EditorUtility.InstanceIDToObject(instanceID) as SO_Test;
        if (obj != null)
        {
            GameDataObjectEditorWindow.OpenWindow(obj);
            //Notify Unity that we are handling the opening of this asset by returning true
            return true;
        }

        //if we return false from this callback, Unity will handle opening the asset another way
        return false;
    }

    //needed to open the window
    public static void OpenWindow(SO_Test o)
    {
        GameDataObjectEditorWindow window = GetWindow<GameDataObjectEditorWindow>("Game Data Editor");
        window.serializedObject = new SerializedObject(o);
    }



    protected override SerializedProperty FirstProperty()
    {
        return serializedObject.FindProperty("ids");
    }

    protected override void OnGUI()
    {
        //set this value to the first property in your SO
        currentProperty = FirstProperty();

        //draws a sidebar for the selected array, and lists elements to the right
        DrawArrayAsSideBar("ids", DrawID);

        GUILayout.BeginArea(new Rect(250, 100, 200, 200));
        DrawArray("goblins");
        GUILayout.EndArea();



        //add and delete elements...
        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(200));
        if (GUILayout.Button("Add Element"))
        {
            //insert at front...
            serializedObject.FindProperty("ids").InsertArrayElementAtIndex(0);
            serializedObject.FindProperty("goblins").InsertArrayElementAtIndex(0);
        }
        if (GUILayout.Button("Remove Element", GUILayout.Width(150)))
        {
            serializedObject.FindProperty("ids").DeleteArrayElementAtIndex(0);
            serializedObject.FindProperty("goblins").DeleteArrayElementAtIndex(0);
        }
        EditorGUILayout.EndHorizontal();

        Apply();
    }

    //custom definition for property...
    protected override void DrawSelectedProperyPanel()
    {
        if(selectedProperty != null)
        {
            currentProperty = selectedProperty;

            DrawField("luckyNumber", true);
            DrawField("nameFirst", true);           
        }        
    }

    //testing
    private void DrawID()
    {
        if (selectedProperty != null)
        {
            currentProperty = selectedProperty;
            DrawField("luckyNumber", true);
            DrawField("nameFirst", true);
            //DrawArray("nickames", true);
        }

    }
    private void DrawGoblin()
    {
        if (selectedProperty != null)
        {
            currentProperty = selectedProperty;
            DrawField("health", true);
            DrawField("mana", true);
        }            
    }
}
