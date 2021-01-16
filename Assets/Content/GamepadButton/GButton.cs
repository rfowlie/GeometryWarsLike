using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


public abstract class Interactable : MonoBehaviour, IInteract
{
    public static event Action<IInteract> ADD;
    public static event Action REMOVE;
    protected void NewInteraction() { ADD?.Invoke(this); }
    protected void RemoveInteraction() { REMOVE?.Invoke(); }

    public abstract void Execute();
}

//only expose a method that invokes the Unity event to the InteractController
public interface IInteract
{
    void Execute();
}


[RequireComponent(typeof(Image))]
public class GButton : Interactable, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    [SerializeField] Color colour = Color.white;
    [SerializeField] Color highlight = Color.white;

    //public static event Action<IInteract> INTERACT;
    public UnityEvent OnClick;

    public override void Execute()
    {
        OnClick.Invoke();
    }

    //call out event to notify interactController that this is the current interaction element
    public void OnPointerEnter(PointerEventData eventData)
    {
        NewInteraction();
        image.color = highlight;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        RemoveInteraction();
        image.color = colour;
    }


    private void Start()
    {
        image = GetComponent<Image>();
        image.color = colour;
    }    
}
