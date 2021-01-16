using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GeometeryWars
{
    public class Enemy_Chase_Jump_Surface : AEnemyJump
    {
        public override void SetMovement()
        {
            Movement = () => SurfaceMovement.Direction.Forward(transform, speedThrust);
        }

        public override void SetRotation()
        {
            Rotation = () => SurfaceMovement.Rotation.FaceTarget(transform, target, hit);
        }
    }
}

