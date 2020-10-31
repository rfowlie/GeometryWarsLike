using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Chase_Wobble_Surface : AEnemy
    {
        //debug for now
        [SerializeField] Vector3 wobbleDirection = Vector3.right;
        [SerializeField] float wobbleSpeed = 1f;
        [SerializeField] float wobbleStrength = 1f;

        protected override void SetMovement()
        {
            currentMovement = new Wobble(this, wobbleDirection, wobbleSpeed, wobbleStrength);
        }
    }
}

