using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CoroutineEX
{
    //pass a func to be executed on a monobehaviour after set delay
    public static Coroutine Delay(MonoBehaviour inst, Action func, float delay)
    {
        return inst.StartCoroutine(CoroutineDelay(func, delay));
    }
    private static IEnumerator CoroutineDelay(Action func, float delay)
    {
        if(delay < 0f) { delay = 0f; }

        yield return new WaitForSeconds(delay);
        func();
    }

    public static Coroutine RandomDelay(MonoBehaviour inst, Action func, float min, float max)
    {
        return inst.StartCoroutine(CoroutineRandomDelay(func, min, max));
    }
    //execute a func after a random time between two values
    private static IEnumerator CoroutineRandomDelay(Action func, float min, float max)
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
        //skips current frame
        yield return null;
        func();
    }
}
