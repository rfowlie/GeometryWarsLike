using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPPrefabTest : MonoBehaviour
{
    Coroutine c = null;

    private void OnEnable()
    {
        c = CoroutineEX.Delay(this, () => gameObject.SetActive(false), 3f);
    }

    private void OnDisable()
    {        
        c = null;
    }
}
