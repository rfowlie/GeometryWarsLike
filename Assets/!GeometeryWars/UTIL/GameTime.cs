using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameTime
{
    public static void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public static void Play()
    {
        Time.timeScale = 1.0f;
    }
}
