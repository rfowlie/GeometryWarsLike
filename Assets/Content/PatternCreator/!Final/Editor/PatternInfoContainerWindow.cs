using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using PatternCreator;


public class PatternInfoContainerWindow : EditorWindow
{
    //allows us to control what occurs when asset of this type is opened
    [OnOpenAsset(0)]
    public static bool OpenAsset(int instanceID, int line)
    {
        //try casting to correct type
        SO_PatternInfoContainer o = EditorUtility.InstanceIDToObject(instanceID) as SO_PatternInfoContainer;
        if (o != null)
        {
            OpenWindow(o);
            //Notify Unity that we are handling the opening of this asset by returning true
            return true;
        }

        //if we return false from this callback, Unity will handle opening the asset another way
        return false;
    }


    private void OnDisable()
    {
        //ensure all changes are grabbed
        if (adj != null && selection_Buttons >= 0)
        {
            //retrieve adjustor changes
            o.values[selection_Buttons] = adj.currentInfo;
        }

        //remove pointer when inactive
        if (pointer != null)
        {
            DestroyImmediate(pointer);
        }
    }

    public static void OpenWindow(SO_PatternInfoContainer so)
    {
        o = so;
        window = GetWindow<PatternInfoContainerWindow>("Edit Window");
        //window.maxSize = new Vector2(450, 300);      
        //window.minSize = new Vector2(450, 300);      
    }

    //window vars
    private static PatternInfoContainerWindow window;
    private static SO_PatternInfoContainer o;
    private static Transform map = null;
    private GameObject pointer;
    private Pointer adj;

    //control vars
    private Vector2 scrollPosition_Buttons = Vector2.zero;
    private int selection_Buttons = -1;
    private Vector2 scrollPosition_Info = Vector2.zero;

    private void OnGUI()
    {
        //map at top in header...
        GUILayout.BeginArea(new Rect(0, 0, window.position.width, 30f));
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal("box");
        map = (Transform)EditorGUILayout.ObjectField("Map", map, typeof(Transform));
        GUILayout.Space(15);       
        
        if(map != null && pointer != null)
        {
            if (GUILayout.Button("Create"))
            {
                //Add element to array, ensure to give default name
                if (o.values == null)
                {
                    o.values = new PatternInfo[0];
                }

                o.AddPatternInfo(new PatternInfo($"Pattern {o.values.Length}", map.InverseTransformDirection(pointer.transform.position)));

                if (adj != null)
                {
                    if (selection_Buttons >= 0)
                    {
                        //retrieve adjustor changes
                        o.values[selection_Buttons] = adj.currentInfo;
                    }

                    selection_Buttons = o.values.Length - 1;
                    //update adjustor selection
                    adj.AddPattern(o.values[selection_Buttons]);
                    adj.SetSelection(o.values[selection_Buttons], selection_Buttons);
                }
                else
                {
                    selection_Buttons = o.values.Length - 1;
                }
            }
        }
        else
        {
            if (GUILayout.Button("Display"))
            {
                //display initial values in SO
                if (map != null && pointer == null)
                {
                    pointer = new GameObject("Pointer");
                    adj = pointer.AddComponent<Pointer>();
                    adj.Setup(map);
                    adj.CalculateAll(o);
                    //set pointer as selected gameobject
                    Selection.activeGameObject = pointer;

                    if (selection_Buttons >= 0)
                    {
                        adj.SetSelection(o.values[selection_Buttons], selection_Buttons);
                    }

                    Debug.Log("<color=red>Display</color>");
                }
            }
        }

        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();

        if(o.values != null)
        {
            //create side button position for all the elements within current SO
            GUILayout.BeginArea(new Rect(0, 30f, 175, 300f));
            scrollPosition_Buttons = EditorGUILayout.BeginScrollView(scrollPosition_Buttons);
            EditorGUILayout.BeginVertical("box");
            for (int i = 0; i < o.GetLength(); i++)
            {
                //select element to see/edit
                if (GUILayout.Button(o.values[i].name))
                {
                    if (adj != null && selection_Buttons == -1)
                    {
                        //set selected editor values value to this
                        selection_Buttons = i;
                        //update adjustor selection
                        adj.SetSelection(o.values[selection_Buttons], selection_Buttons);
                    }
                    else if (adj != null && selection_Buttons >= 0)
                    {
                        //retrieve adjustor changes
                        o.values[selection_Buttons] = adj.currentInfo;
                        //set selected editor values value to this
                        selection_Buttons = i;
                        //update adjustor selection
                        adj.SetSelection(o.values[selection_Buttons], selection_Buttons);
                    }
                    else
                    {
                        //set selected editor values value to this
                        selection_Buttons = i;
                    }

                    //move pointer to relative position or element selected
                    if (map != null && pointer != null)
                    {
                        pointer.transform.position = map.TransformDirection(o.values[selection_Buttons].relativePosition);
                    }
                }
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
            GUILayout.EndArea();

            //save changes
        }


        if (selection_Buttons >= 0)
        {
            //info of elements
            GUILayout.BeginArea(new Rect(200, 30f, 320, 300));
            scrollPosition_Info = EditorGUILayout.BeginScrollView(scrollPosition_Info);
            EditorGUILayout.BeginVertical("box");
            o = DrawInfo(o, selection_Buttons);
            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            if(adj != null)
            {
                //take current changes to element from adjustor and apply to SO
                if (GUILayout.Button("Update", GUILayout.MaxWidth(150)))
                {
                    o.values[selection_Buttons] = adj.currentInfo;
                }
            }            
            if (GUILayout.Button("Remove", GUILayout.MaxWidth(150)))
            {
                o.RemovePatternInfo(selection_Buttons);
                if(adj != null)
                {
                    adj.RemovePattern(selection_Buttons);
                }

                selection_Buttons = -1;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
            GUILayout.EndArea();
        } 
    }

    //how the info shoudl be displayed...
    private SO_PatternInfoContainer DrawInfo(SO_PatternInfoContainer o, int i)
    {
        o.values[i].name = EditorGUILayout.TextField("Name", o.values[i].name);
        o.values[i].relativePosition = EditorGUILayout.Vector3Field("Relative Position", o.values[i].relativePosition);
        o.values[i].fillerAmount = EditorGUILayout.IntField("Amount of Points", o.values[i].fillerAmount);
        o.values[i].radius = EditorGUILayout.FloatField("Radius", o.values[i].radius);
        o.values[i].viewPercentage = EditorGUILayout.Slider("View Percentage", o.values[i].viewPercentage, 0f, 100f);
        o.values[i].shape = (SpawnShape)EditorGUILayout.EnumPopup("Shape", o.values[i].shape);
        o.values[i].towardsCenter = EditorGUILayout.Toggle("Aim Towards Center", o.values[i].towardsCenter);
        return o;
    }




















    //**************************************************************************
    //draw only a specific element from an array like default inspector
    private void DefaultDrawerArrayElement(SerializedProperty prop, int index)
    {
        int value = 0;
        foreach (SerializedProperty p in prop)
        {
            if (value == index)
            {
                DrawProperties(p, true);
                break;
            }
            else
            {
                value++;
            }
        }
    }

    //draw all elements in the Serialized property like default inspector
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
}
