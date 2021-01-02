using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GeometeryWars
{
    //rotates to face target, doesn't move
    //then dashs in most recent forward without rotating
    public class Enemy_Dash_Surface : AEnemy
    {
        protected override void SetMovement()
        {
            Movement = null;
        }

        protected override void SetRotation()
        {
            Rotation = () => EMovement.Rotation.FaceTarget(transform, target, hit);
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
                    Movement = () => EMovement.Direction.Forward(transform, speedThrust);
                    Rotation = () => EMovement.Rotation.Forward(transform, hit);
                }
                else
                {
                    Movement = null;
                    Rotation = () => EMovement.Rotation.FaceTarget(transform, target, hit);
                }
            }
        }
    }
}
