using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Wander_Surface : AEnemy
    {
        public override void SetMovement()
        {
            Movement = () => SurfaceMovement.Direction.Forward(transform, speedThrust);
        }

        public override void SetRotation()
        {
            Rotation = () => SurfaceMovement.Rotation.SlightRotation(transform, hit, Random.Range(0f, 1f));
            //Rotation = () => SurfaceMovement.Rotation.Forward(transform, hit);
        }

        Coroutine c = null;

        protected override void Start()
        {
            base.Start();

            c = CoroutineEX.RandomDelay(this, SetRotation, 0f, 5f);

            
        }
    }
}

