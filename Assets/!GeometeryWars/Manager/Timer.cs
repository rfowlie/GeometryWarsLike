using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public bool isPlay = false;
    public float levelTime = 60f;
    public float timer;

    public static event Action BEGIN;
    public static event Action FINISH;

    private void Start()
    {
        timer = levelTime;
        isPlay = true;
        BEGIN();
    }

    private void Update()
    {       
        if (isPlay)
        {
            timer -= Time.deltaTime;
            if(timer < 0f)
            {
                //end level...
                isPlay = false;
                FINISH();
            }
        }
    }
}
