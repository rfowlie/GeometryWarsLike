using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Common Array operations
public class ArrayEX : ArrayList
{
    //take in any type of array and resize, removeing or adding elements at the end
    public static T[] ResizeArray<T>(int count, T[] arr)
    {
        T[] temp = new T[count];

        int length = temp.Length > arr.Length ? arr.Length : temp.Length;

        for (int i = 0; i < length; i++)
        {
            temp[i] = arr[i];
        }

        return temp;
    }

    //custom classes new to be instantiated
    public static T[] ResizeArrayCustomClass<T>(int count, T[] arr) where T : new()
    {
        T[] temp = new T[count];
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = new T();
        }

        int length = temp.Length > arr.Length ? arr.Length : temp.Length;

        for (int i = 0; i < length; i++)
        {
            temp[i] = arr[i];
        }

        return temp;
    }


    //takes an array, removes elements at specific spots, shifts other elements and resizes the array
    public static T[] RemoveAndShift<T>(T[] arr, params int[] indexToRemove)
    {
        //create new array of length original - elements to remove
        T[] newArr = new T[arr.Length - indexToRemove.Length];

        //replace all elements into smaller array, skipping indexs listed in remove list
        for (int i = 0, o = 0; i < arr.Length; i++)
        {
            //check that index value isn't the same as one to remove
            for (int check = 0; check < indexToRemove.Length; check++)
            {
                if(i == indexToRemove[check])
                {
                    //move to next element and restart check
                    i++;
                    check = -1;
                }
            }

            //if this index isn't one to remove, add to new arr
            newArr[o] = arr[i];
            o++;
        }

        return newArr;
    }
}
