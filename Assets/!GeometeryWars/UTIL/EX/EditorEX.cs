using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Common Editor script operations
public static class EditorEX 
{
    //???
    public static void RelativeHandles(Transform transform, Vector3[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            //draw position handle?
            Vector3 temp = Handles.PositionHandle(transform.position + points[i], transform.rotation);
            //update handle array position (local)?
            if (temp != points[i])
            {
                points[i] += (temp - points[i]) - transform.position;
            }
        }
    }    
}
