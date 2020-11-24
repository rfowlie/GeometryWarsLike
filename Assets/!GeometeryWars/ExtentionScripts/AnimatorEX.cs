using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorEX
{
    //sets all bools in anim to passed in value
    public static void SetBools(Animator anim, bool b)
    {
        for (int i = 0; i < anim.parameterCount; i++)
        {
            if (anim.parameters[i].type.ToString() == "Bool")
            {
                anim.SetBool(anim.parameters[i].name, b);
            }
        }
    }

    //set all bools to false
    public static void ResetBools(Animator anim)
    {
        SetBools(anim, false);
    }

    //set animator according to passed in array of names and bools (correspond to each other)
    public static void SetBools(Animator anim, string[] names, bool[] bools)
    {
        for (int i = 0; i < names.Length; i++)
        {
            for (int j = 0; j < anim.parameterCount; j++)
            {
                //Debug.Log("<color=green>Param Name: </color>" + anim.parameters[i].name);
                if (anim.parameters[j].name == names[i])
                {
                    //Debug.Log("<color=purple>Changed bool</color>");    
                    anim.SetBool(anim.parameters[j].name, bools[i]);
                    break;
                }
            }
        }
    }


    //???
    //activate given trigger on given anim
    public static void Trigger(Animator anim, string triggerName)
    {
        anim.SetTrigger(triggerName);
    }
}
