using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Chase_Surface : AEnemy
    {
        public override void SetMovement()
        {
            //Debug.Log("Set Movement on Chase");
            Movement = () => SurfaceMovement.Direction.Forward(transform, speedThrust);
        }

        public override void SetRotation()
        {
            //Debug.Log("Set Rotation on Chase");
            Rotation = () => SurfaceMovement.Rotation.FaceTarget(transform, target, hit);
        }
    }
}