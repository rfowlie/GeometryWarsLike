using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//place on triggers to notify outside objects
public class ChildTrigger : MonoBehaviour
{
    public event Action TRIGGER;

    private void OnTriggerEnter(Collider other)
    {
        TRIGGER();
    }
}
