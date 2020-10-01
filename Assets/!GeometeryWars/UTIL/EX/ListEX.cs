using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Common list operations
public class ListEX
{
    //takes in any list and swaps the position of two values
    public static void Swap<T>(IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }
}
