using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Wander_Jump_Surface : AEnemyJump
    {
        protected override void SetMovement()
        {
            defaultMovement = new wander(this);
            jumpMovement = new Wobble(this, Vector3.right, 10f, 3f);
            currentMovement = defaultMovement;
        }
    }   
}

