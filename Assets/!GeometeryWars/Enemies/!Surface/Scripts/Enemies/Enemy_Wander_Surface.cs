using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Wander_Surface : AEnemy
    {
        protected override void SetMovement()
        {
            currentMovement = new MoveForward(this);
        }
    }
}

