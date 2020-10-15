using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Common Array operations
public class ArrayEX : ArrayList
{
    //take in any type of array and resize, removeing or adding elements at the end
    public static T[] ResizeArray<T>(int count, T[] arr)
    {
        T[] temp = new T[count];
        int length = temp.Length > arr.Length ? arr.Length : temp.Length;
        Array.Copy(arr, temp, length);        
        return temp;
    }


    //doesn't leave any values in the array null, unlike array above which changes size but leaves extra slots null
    public static T[] ResizeArrayNoNull<T>(int count, T[] arr) where T : new()
    {
        T[] temp = new T[count];
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = new T();
        }

        int length = temp.Length > arr.Length ? arr.Length : temp.Length;
        Array.Copy(arr, temp, length);
        return temp;
    }


    //takes an array, removes elements at specific spots, shifts other elements and resizes the array
    public static T[] RemoveAndShift<T>(T[] arr, params int[] indexToRemove)
    {
        //create new array of length original - elements to remove
        T[] newArr = new T[arr.Length - indexToRemove.Length];

        //replace all elements into smaller array, skipping indexs listed in remove list
        for (int i = 0, o = 0; o < arr.Length; i++, o++)
        {
            //check that index value isn't the same as one to remove
            for (int j = 0; j < indexToRemove.Length; j++)
            {
                if(o == indexToRemove[j])
                {
                    //move to next element and restart check
                    o++;
                    j = 0;
                }
            }

            //if this index isn't one to remove, add to new arr
            Array.Copy(arr, o, newArr, i, 1);            
        }

        return newArr;
    }
}
