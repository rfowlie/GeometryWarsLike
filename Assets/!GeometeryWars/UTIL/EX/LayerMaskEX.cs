using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LayerMaskEX : MonoBehaviour
{
    //check if objects layer is within a layermask
    public static bool IsInLayerMask(GameObject otherObj, LayerMask layerMask)
    {
        //Logical OR, if otherObj layer not within layermask, will produce a different number than the layermask, returning false
        return layerMask == (layerMask | (1 << otherObj.layer));
    }
}
