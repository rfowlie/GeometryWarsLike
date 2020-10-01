using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//container for a group of patterns
//allows more complex behaviour to emerge

public class PatternSet
{
    //CONSTUCTORS  
    public PatternSet(Pattern[] patterns)
    {
        this.patterns = patterns;
        //List<IFinitePattern> f = new List<IFinitePattern>();
        for (int i = 0; i < patterns.Length; i++)
        {

        }
    }

    //VARIABLES
    private Pattern[] patterns;
    //private IFinitePattern[] finitePatterns;
    private Pattern[] infinitePatterns;

    //BREAK UP THE ARRAY INTO USING IFINITEPATTERN VS NOT

    //METHODS
    //call enter on all patters in set
    public void Enter()
    {
        for(int i = 0; i < patterns.Length; i++)
        {
            patterns[i].Enter();
        }
    }

    //checks if all patterns in arr finished
    public bool Finished()
    {
        bool allDone = true;
        for(int i = 0; i < patterns.Length; i++)
        {
            if(!patterns[i].Finished())
            {
                allDone = false;
                break;
            }
        }

        return allDone;
    }

    //call all calculates, but only if Exit false
    public void Calculate()
    {
        for (int i = 0; i < patterns.Length; i++)
        {
            if(!patterns[i].Finished())
            {
                patterns[i].Calculate();
            }
        }
    }

    //call all executes, but only if Exit is false
    public void Execute()
    {
        for (int i = 0; i < patterns.Length; i++)
        {
            if (!patterns[i].Finished())
            {
                patterns[i].Execute();
            }
        }
    }

    //add pattern to set
    public void AddPattern(Pattern newPattern)
    {
        List<Pattern> temp = new List<Pattern>(patterns);
        temp.Add(newPattern);
        patterns = temp.ToArray();
    }
    //remove a pattern from the arr
    public void RemovePattern(int index)
    {
        //validate index
        if(index < 0 || index >= patterns.Length)
        {
            Debug.Log("Not a valid index for pattern array");
            return;
        }

        List<Pattern> temp = new List<Pattern>(patterns);
        temp.RemoveAt(index);
        patterns = temp.ToArray();
    }
}
