using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Common layermask operations
public class LayerMaskEX : MonoBehaviour
{
    //uses inclusive or, if either bit from layermask or layer are 1 keeps it
    //so if the layer is on a bit that isn't on the layermask it will cause the result
    //to differ from the layermask, returning false
    public static bool IsInLayerMask(int otherLayer, LayerMask yourLayer)
    {
        return yourLayer == (yourLayer | (1 << otherLayer));
    }
}
