using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Common list operations
public class ListEX
{
    //takes in any list and swaps the position of two values
    public static void Swap<T>(IList<T> list, int indexA, int indexB)
    {
        //ensure valid indexs
        if(indexA < 0 || 
           indexA >= list.Count ||
           indexB < 0 || 
           indexB >= list.Count)
        {            
            Debug.LogError("Index out of range");
        }    

        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;        
    }
}
