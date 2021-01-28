using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TestEditorWindow : EditorWindow
{
    public static TestEditorWindow window;

    [MenuItem("Window/Tools/Test")]
    public static void OpenWindow()
    {
        window = GetWindow<TestEditorWindow>("Test Window");
        window.minSize = new Vector2(320, 150);
        window.maxSize = new Vector2(510, 300);
    }

    private void OnEnable()
    {
        action = new List<System.Action>();
        action.Add(EditMenu);
        action.Add(ButtonMenu);

        //Grab any resources like textures needed for the window in on enable
        tex = EditorGUIUtility.FindTexture("Assets/!!Test/PlayingWithEditorWindows/Textures/TexturesCom_Brick_StoneCladding8_512_albedo.tif");
        altTex = EditorGUIUtility.FindTexture("Assets/!!Test/PlayingWithEditorWindows/Textures/TexturesCom_Pavement_CastleFloor_512_albedo.tif");
        altTex2 = EditorGUIUtility.FindTexture("Assets/!!Test/PlayingWithEditorWindows/Textures/TexturesCom_Brick_StoneCladding8_512_ao.tif");
    }

    static Texture2D tex;
    static Texture2D altTex;
    static Texture2D altTex2;
    //GUIStyle allows you to alter the default look of elements
    GUIStyle flexibleStyle = new GUIStyle();


    //Use rects to box off specific areas easily
    Rect rect = new Rect(0, 0, 200, 150);
    //needed for scroll view
    Vector2 scrollPosition = Vector2.zero;

    private void OnGUI()
    {
        ////alter the enter colour of the GUI
        //GUI.color = Color.cyan;
        ////alter the colour of elements
        //GUI.backgroundColor = Color.green;

        //label absolute distance, 0 resets to default
        //EditorGUIUtility.labelWidth = 150;
        //alter field min distance
        //EditorGUIUtility.fieldWidth = 50;


        //setup Styles
        flexibleStyle.padding = new RectOffset(25, 25, 0, 0);

        //info = ButtonMenuSection(info, new Rect(0f, 0f, 0.75f, 1f), new string[] { "Edit", "Button" }, action, tex);

        DrawTextureSection(new Rect(0, 0, 0.5f, 1f), tex);
        DrawTextureSection(new Rect(0.5f, 0f, 1f, 1f), altTex);
        DrawTextureSection(new Rect(0.25f, 0.25f, 0.75f, 0.75f), altTex2);


        //Interesting animated rect thing...
        //info = ButtonMenu(info, new Rect(0f, 0f, 0.3f, 1f), new string[] { "Edit", "Button" });

        //slider = EditorGUILayout.Slider("Slider", slider, 0f, 1f);        
        //if(EditorGUILayout.BeginFadeGroup(slider))
        //{
        //    EditorGUI.indentLevel++;
        //    EditorGUILayout.PrefixLabel("Color");
        //    m_Color = EditorGUILayout.ColorField(m_Color);
        //    EditorGUILayout.PrefixLabel("Text");
        //    m_String = EditorGUILayout.TextField(m_String);
        //    EditorGUILayout.PrefixLabel("Number");
        //    m_Number = EditorGUILayout.IntSlider(m_Number, 0, 10);
        //    EditorGUI.indentLevel--;
        //}   
        //EditorGUILayout.EndFadeGroup();



        //scrollPosition = ScrollBarSection(scrollPosition, new Rect(0.5f, 0.05f, 0.95f, 0.95f), 
        //    () =>
        //    {
        //        for (int i = 0; i < 50; i++)
        //        {
        //            EditorGUILayout.FloatField($"{i}", i);
        //        }
        //    }, 
        //    tex
        //);
    }
    Color m_Color;
    string m_String;
    int m_Number;
    float slider;

    bool dropDown;
    ButtonMenuInfo info;
    List<System.Action> action;
    int selected = 0;
    //bodies
    //edit menu
    private SO_Test obj;

    private void EditMenu()
    {
        obj = (SO_Test)EditorGUILayout.ObjectField("Object", obj, typeof(SO_Test));
        if(obj != null)
        {
            DrawProperties(new SerializedObject(obj).FindProperty("ids"), true);
        }         
    }

    //default property drawing method???
    protected void DrawProperties(SerializedProperty prop, bool drawChildren)
    {
        //SerializedProperty prop = new SerializedObject(obj).FindProperty("ids");
        foreach (SerializedProperty p in prop)
        {
            if (p.isArray)
            {
                if (p.isExpanded)
                {
                    EditorGUI.indentLevel += 10;
                    DrawProperties(p, drawChildren);
                    EditorGUI.indentLevel -= 10;
                }
            }
            else
            {
                EditorGUILayout.PropertyField(p, drawChildren);
            }
        }
    }

    private void ButtonMenu()
    {
        if(GUILayout.Button("Debug"))
        {
            Debug.Log("Button Pressed");
        }
        if(GUILayout.Button("Red"))
        {
            GUI.color = Color.red;
        }
    }


    //******Start building Library of easy to use designs*****
    
    //Draw a texture at relative location
    private void DrawTextureSection(Rect relativeRect, Texture2D tex)
    {
        //get the correct dimensions based off the current window size
        relativeRect = EditorEX.AdjustRect(relativeRect, window.position);
        GUI.DrawTexture(new Rect(relativeRect), tex, ScaleMode.ScaleAndCrop);
    }

    //Draw a scroll bar section at relative location
    private Vector2 ScrollBarSection(Vector2 scrollPosition, Rect relativeRect, System.Action toDraw, Texture2D tex = null)
    {
        //get the correct dimensions based off the current window size
        relativeRect = EditorEX.AdjustRect(relativeRect, window.position);

        GUIStyle s = new GUIStyle();
        //have bars slightly away from edge
        s.padding = new RectOffset(10, 5, 10, 5);

        //place texture in backgroud if texture given
        if (tex != null)
        {
            GUI.DrawTexture(new Rect(relativeRect), tex, ScaleMode.ScaleAndCrop);
        }
        GUIContent content = new GUIContent("This is a content", tex, "This is a tooltip");

        GUILayout.BeginArea(relativeRect, content, s);
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        EditorGUILayout.BeginVertical("box");
        //what to draw inside here
        toDraw();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();

        return scrollPosition;
    }


    //DEPRECIATED??? GUILayout.SelectionGrid does this already... Though wrapping in scroll view is nice...
    //create relative section that lists a number of buttons and returns the value of the button most recently pressed
    private ButtonMenuInfo ButtonMenu(ButtonMenuInfo info, Rect relativeRect, string[] buttonLabels, Texture2D tex = null)
    {
        //get the correct dimensions based off the current window size
        relativeRect = EditorEX.AdjustRect(relativeRect, window.position);

        GUIStyle s = new GUIStyle();
        //have bars slightly away from edge
        s.padding = new RectOffset(5, 5, 5, 5);
        //place texture in backgroud if texture given
        if (tex != null)
        {
            GUI.DrawTexture(new Rect(relativeRect.x, relativeRect.y, relativeRect.width, relativeRect.height), tex, ScaleMode.ScaleAndCrop);
        }
        GUILayout.BeginArea(relativeRect, s);
        EditorGUILayout.BeginVertical("box", GUILayout.MinWidth(100), GUILayout.MaxWidth(100), GUILayout.ExpandWidth(false));
        info.scrollPosition = EditorGUILayout.BeginScrollView(info.scrollPosition);
        for (int i = 0; i < buttonLabels.Length; i++)
        {
            if (GUILayout.Button(buttonLabels[i]))
            {
                //set selected value to this
                info.selectedElement = i;
            }
        }
        //close
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();

        return info;
    }  
    private struct ButtonMenuInfo
    {
        public int selectedElement;
        public Vector2 scrollPosition;
    }
}





//*********
//Methods to help with editor building
public static class EditorEX
{
    public static Rect AdjustRect(Rect relativeRect, Rect windowRect)
    {
        relativeRect.x *= windowRect.width;
        relativeRect.y *= windowRect.height;
        relativeRect.width = (relativeRect.width * windowRect.width) - relativeRect.x;
        relativeRect.height = (relativeRect.height * windowRect.height) - relativeRect.y;

        return relativeRect;
    }
}
