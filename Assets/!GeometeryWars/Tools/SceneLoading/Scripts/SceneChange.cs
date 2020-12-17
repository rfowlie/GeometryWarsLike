﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private SceneList sceneChanges = null;

    public virtual void Change()
    {
        SceneController.Instance.SceneChange(sceneChanges);
    }
}
