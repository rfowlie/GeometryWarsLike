using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Pattern
{
    //update
    public abstract void Calculate();
    //fixed update
    public abstract void Execute();
    //important setup for pattern
    public abstract void Enter();
    //tells whether this pattern is finished 
    public abstract bool Finished();
}

//this pattern should take in a target transform and continuously chase it
public class ChaseForTime : Pattern
{
    //CONSTRUCTOR
    public ChaseForTime(Transform actor, Transform target)
    {
        
    }    

    //METHODS
    public override void Calculate()
    {
        throw new System.NotImplementedException();
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override bool Finished()
    {
        throw new System.NotImplementedException();
    }
}