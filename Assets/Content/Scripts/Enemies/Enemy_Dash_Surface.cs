using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GeometeryWars
{
    //rotates to face target, doesn't move
    //then dashs in most recent forward without rotating
    public class Enemy_Dash_Surface : AEnemy
    {
        public override void SetMovement()
        {
            Movement = null;
        }

        public override void SetRotation()
        {
            Rotation = () => SurfaceMovement.Rotation.FaceTarget(transform, target, hit);
        }

        //FOR NOWWWWW
        bool isDash = false;
        float count = 0f;
        private void Update()
        {
            count += Time.deltaTime;
            if(count > 5f)
            {
                count -= 5f;
                isDash = !isDash;
                if(isDash)
                {
                    Movement = () => SurfaceMovement.Direction.Forward(transform, speedThrust);
                    Rotation = () => SurfaceMovement.Rotation.Forward(transform, hit);
                }
                else
                {
                    Movement = null;
                    Rotation = () => SurfaceMovement.Rotation.FaceTarget(transform, target, hit);
                }
            }
        }
    }
}
