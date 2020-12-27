using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneChangeEvent : MonoBehaviour
{
    public static event Action TRIGGERED;

    public void Activate()
    {
        TRIGGERED();
    }
}
