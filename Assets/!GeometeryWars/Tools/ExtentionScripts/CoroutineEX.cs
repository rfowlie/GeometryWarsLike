using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CoroutineEX
{
    public static Coroutine Delay(MonoBehaviour inst, Action func, float delay)
    {
        return inst.StartCoroutine(OnDelay(func, delay));
    }
    //wait a passed in time in real seconds then execute function
    private static IEnumerator OnDelay(Action func, float delay)
    {
        if(delay < 0f) { delay = 0f; }

        yield return new WaitForSeconds(delay);
        func();
    }
    //execute a func after a random time between two values
    public static IEnumerator RandomDelayFunc(Action func, float min, float max)
    {
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(min, max));
        func();
    }

    //will execute passed method in next frame
    //*also allows you to tie the coroutine to other monobehaviours, not just self...
    public static Coroutine OnNextFrame(MonoBehaviour inst, Action func)
    {
        return inst.StartCoroutine(CoroutineOnNextFrame(func));
    }
    private static IEnumerator CoroutineOnNextFrame(Action func)
    {
        yield return null;
        func();
    }
}
