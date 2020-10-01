using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APatternContinuous : MonoBehaviour
{
    public float speedThrust = 2f;
    public float speedSteer = 50f;

    private PatternSet[] set;
    public int setIndex = 0;

    private void Start()
    {
        set = new PatternSet[4];
        set[0] = new PatternSet(new Pattern[] { new ThrustRelativeForTime(transform, 2f, speedThrust) });
        set[1] = new PatternSet(new Pattern[] { new SteerForTime(transform, transform.TransformPoint(-Vector3.forward), speedSteer, speedSteer) });
        set[2] = new PatternSet(new Pattern[] { new ThrustRelativeForTime(transform, 2f, speedThrust) });
        set[3] = new PatternSet(new Pattern[] { new SteerForTime(transform, transform.TransformPoint(Vector3.forward), speedSteer, speedSteer) });



        //set = new PatternSet[4];
        //Pattern[] pTemp;
        //pTemp = new Pattern[]
        //        {
        //            new ThrustTime(transform, 1.5f, speedThrust)
        //        };
        //set[0] = new PatternSet(pTemp);
        //set[2] = new PatternSet(pTemp);
        //pTemp = new Pattern[]
        //        {
        //            new ThrustTime(transform, 1f, speedThrust),
        //            new Steer(transform, 0f, 60f, speedSteer, speedSteer)
        //        };
        //set[1] = new PatternSet(pTemp);

        //pTemp = new Pattern[]
        //        {
        //            new ThrustTime(transform, 2f, speedThrust),
        //            new Steer(transform, 0f, 180f, speedSteer, speedSteer)
        //        };
        //set[3] = new PatternSet(pTemp);


        //call enter on current set
        set[setIndex].Enter();
    }

    private void Update()
    {
        //check if need to switch pattern set
        bool next = set[setIndex].Finished();
        
        //THIS CAN BE CHANGED TO A DESICION TYPE SYSTEM
        //EACH PATTERN CAN HAVE A WEIGHT TO IT???
        //if next, adjust index and call all enters
        if(next)
        {
            setIndex = (setIndex + 1) % set.Length;
            set[setIndex].Enter();
        }

        //only run patterns that aren't complete
        set[setIndex].Calculate();
    }

    private void FixedUpdate()
    {
        set[setIndex].Execute();    
    }
}