﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Wander_Jump_Surface : AEnemyJump
    {
        protected override void SetMovement()
        {
            Movement = () => EMovement.Direction.Forward(transform, speedThrust);
        }

        protected override void SetRotation()
        {
            Rotation = () => EMovement.Rotation.Forward(transform, hit);
        }
    }   
}

