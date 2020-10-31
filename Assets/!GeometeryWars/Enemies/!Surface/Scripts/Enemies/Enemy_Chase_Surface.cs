using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Chase_Surface : AEnemy
    {
        protected override void SetMovement()
        {
            currentMovement = new Chase(this);
        }
    }
}