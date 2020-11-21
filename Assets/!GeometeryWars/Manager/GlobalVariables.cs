using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Store global variables for game...
public class GlobalVariables : Singleton<GlobalVariables>
{
    public Transform map;
    public LayerMask mapLayer;
    public LayerMask obstacleLayer;
}
