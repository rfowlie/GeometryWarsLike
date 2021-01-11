using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameTime : Singleton<GameTime>
{
    public static void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public static void Play()
    {
        Time.timeScale = 1.0f;
    }

    private Coroutine c = null;
    //adjust time scale of game to desire speed in a desired amount of time using a x^2 curve
    public void TimeWarp(float targetTimeScale, float speed, float exp = 1)
    {
        //prevent this being created more than once...
        if(c == null)
        {
            c = StartCoroutine(AdjustTime(targetTimeScale, speed, exp));
        }
    }
    private static IEnumerator AdjustTime(float targetTimeScale, float speed, float exp)
    {
        if(exp <= 0)
        {
            exp = 0.01f;
        }

        float count = 0f;
        float start = Time.timeScale;
        while(start != targetTimeScale)
        {
            count += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(start, targetTimeScale, Mathf.Pow(count / speed, exp));
            yield return null;
        }
    }
}
