using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//common transform operations
public class TransformEX
{
    //find the root parent transform
    public static Transform FindRootParent(Transform child)
    {
        Transform temp = child;
        if(child.parent)
        {
            temp = FindRootParent(child.parent);
        }

        return temp;
    }


    //create list and then send it to the recursive statement
    public static T[] FindComponentsInHierarchy<T>(Transform parent)
    {
        List<T> masterList = new List<T>();

        masterList = InsidePart<T>(parent, masterList);

        return masterList.ToArray();
    }

    private static List<T> InsidePart<T>(Transform parent, List<T> list)
    {
        //check if has children
        for (int i = 0; i < parent.childCount; i++)
        {
            //call this on all children
            Transform child = parent.GetChild(i);
            if (child.childCount > 0)
            {
                InsidePart<T>(child, list);
            }
        }

        //parent doesn't have children, check for component
        if (parent.GetComponent<T>() != null)
        {
            //add to some list... or pass upwards???
            list.Add(parent.GetComponent<T>());
        }

        return list;
    }
}
