using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Wander_Surface : AEnemy
    {
        public override void SetMovement()
        {
            Movement = () => EMovement.Direction.Forward(transform, speedThrust);
        }

        public override void SetRotation()
        {
            Rotation = () => EMovement.Rotation.Forward(transform, hit);
        }
    }
}

