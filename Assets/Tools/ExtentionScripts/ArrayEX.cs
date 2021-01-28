using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//NEEDS A LOOK OVER AND A CLEAN
public class ArrayEX : ArrayList
{
    //take in any type of array and resize, removeing or adding elements at the end
    public static T[] ResizeArray<T>(int newSize, T[] arr)
    {
        T[] temp = new T[newSize];
        int length = temp.Length > arr.Length ? arr.Length : temp.Length;
        Array.Copy(arr, temp, length);
        return temp;
    }

    //doesn't leave any values in the array null, unlike array above which changes size but leaves extra slots null
    public static T[] ResizeArrayNoNull<T>(int newSize, T[] arr) where T : new()
    {
        //not effecient...
        T[] temp = new T[newSize];
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = new T();
        }

        int length = temp.Length > arr.Length ? arr.Length : temp.Length;
        Array.Copy(arr, temp, length);
        return temp;
    }

    //remove indexes in array, return new resized array
    public static T[] RemoveAndAdjust<T>(T[] arr, params int[] indexesToRemove)
    {
        List<T> list = new List<T>(arr);
        List<int> indexes = new List<int>(indexesToRemove);
        indexes.Sort();
        //eliminate indexes from hightest to lowest to not mess things up
        indexes.Reverse();
        for (int i = 0; i < indexes.Count; i++)
        {
            //ensure index is within bounds
            if (indexes[i] < 0 || indexes[i] > list.Count) { continue; }
            list.RemoveAt(indexes[i]);
        }

        return list.ToArray();
    }
      

    //take in any type of array and resize, removeing or adding elements at the end
    public static T[] ResizeAndTrim<T>(T[] arr, int newSize)
    {
        T[] temp = new T[newSize];
        int length = temp.Length > arr.Length ? arr.Length : temp.Length;
        Array.Copy(arr, temp, length);
        return temp;
    }

    //doesn't leave any values in the array null, unlike array above which changes size but leaves extra slots null
    public static T[] ResizeArrayNoNull<T>(T[] arr, int newSize) where T : new()
    {
        //not effecient...
        T[] temp = new T[newSize];
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = new T();
        }

        int length = temp.Length > arr.Length ? arr.Length : temp.Length;
        Array.Copy(arr, temp, length);
        return temp;
    }



    //KEEP
    //remove indexes in array, return new resized array
    public static T[] RemoveAndResize<T>(T[] arr, params int[] indexesToRemove)
    {
        List<T> list = new List<T>(arr);
        List<int> indexes = new List<int>(indexesToRemove);
        //eliminate indexes from hightest to lowest to not mess things up
        indexes.Sort();
        indexes.Reverse();
        for (int i = 0; i < indexes.Count; i++)
        {
            //ensure index is within bounds
            if (indexes[i] < 0 || indexes[i] > list.Count) { continue; }
            list.RemoveAt(indexes[i]);
        }

        return list.ToArray();
    }    

    //KEEP
    //ALT to system.Array.Resize because this will instantiate the array if not instantiated...
    public static T[] Grow<T>(T[] arr, int newSize, T defaultValue = default)
    {
        T[] temp = new T[newSize];
        for (int i = 0; i < temp.Length; i++)
        {
            //resizing to a greater length, set values to default for type
            if (i > arr.Length - 1)
            {
                temp[i] = defaultValue;
            }
            //otherwise just swap over value so no data lost
            else
            {
                temp[i] = arr[i];
            }
        }

        return temp;
    } 
}
