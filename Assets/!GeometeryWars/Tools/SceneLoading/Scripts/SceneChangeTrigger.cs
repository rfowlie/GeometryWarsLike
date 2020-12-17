using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scene change that gets activated through trigger
[RequireComponent(typeof(SphereCollider))]
public class SceneChangeTrigger : SceneChange
{
    [SerializeField] private LayerMask triggerLayer;

    private void Start()
    {
        //auto set to trigger
        GetComponent<SphereCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the correct layer interacts with this change scene
        if (LayerMaskEX.IsInLayerMask(other.gameObject, triggerLayer))
        {
            Change();
        }
    }
}
