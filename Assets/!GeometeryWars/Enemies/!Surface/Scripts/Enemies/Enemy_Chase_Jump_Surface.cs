using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GeometeryWars
{
    public class Enemy_Chase_Jump_Surface : AEnemyJump
    {
        protected override void SetMovement()
        {
            defaultMovement = new Chase(this);
            jumpMovement = new wander(this);
            currentMovement = defaultMovement;
        }
    }
}

