using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Manage all interactable objects in game...
public class InteractController : Singleton<InteractController>
{
    private IInteract currentInteraction;
    public void ExecuteCurrent()
    {
        if (currentInteraction != null)
        {
            currentInteraction.Execute();
        }
    }

    private void OnEnable()
    {
        Interactable.ADD += (x) =>
        {
            currentInteraction = x;
        };
        Interactable.REMOVE += () =>
        {
            currentInteraction = null;
        };
    }

    private void Start()
    {
        Debug.Log("<color=pink>I LIVE!!</color>");
    }
}
