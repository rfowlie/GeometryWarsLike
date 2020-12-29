using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeometeryWars
{
    //Game Controller will be listening for this
    public class SceneChanger : MonoBehaviour
    {
        public static event Action TRIGGER;
        public void Change()
        {
            TRIGGER();
        }
    }
}