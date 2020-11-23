using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CoroutineEX
{
    //wait a passed in time in real seconds then execute function
    public static IEnumerator Delay(Action func, float delay)
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
}
