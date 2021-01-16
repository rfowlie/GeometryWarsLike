using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeometeryWars
{
    //Game Controller will be listening for this
    public class SceneChanger : MonoBehaviour
    {
        public static event Action<bool> TRIGGER;
        public void Change(bool b = true)
        {
            TRIGGER?.Invoke(b);
        }
    }
}