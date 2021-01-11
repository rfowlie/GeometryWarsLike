using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//common transform operations
public class TransformEX
{
    //find the foot parent transform
    public static Transform GetRoot(Transform child)
    {
        Transform temp = child;
        while (child.parent)
        {
            temp = child.parent;
        }

        return temp;
    }

    //gets all componenet references in hierarchy it belongs too
    public static T[] GetAllOfComponentInHierarchy<T>(Transform transform)
    {
        return GetAllOfComponentInChidren<T>(GetRoot(transform));
    }

    
    //gets all component references in children
    public static T[] GetAllOfComponentInChidren<T>(Transform transform)
    {
        List<T> masterList = new List<T>();

        masterList = DepthFirstSearch(transform, masterList);

        return masterList.ToArray();
    }

    private static List<T> DepthFirstSearch<T>(Transform parent, List<T> list)
    {
        //check if has children
        for (int i = 0; i < parent.childCount; i++)
        {
            //call this on all children
            Transform child = parent.GetChild(i);
            if (child.childCount > 0)
            {
                DepthFirstSearch(child, list);
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
