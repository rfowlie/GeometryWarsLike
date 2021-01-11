using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public abstract class AEnemyJump : AEnemy
    {        
        private bool isJump = false;
        private float jumpTime = 0f;
        public float jumpForce = 10f;
        public float gravity = 10f;
        public float delayMin = 1f;
        public float delayMax = 3f;
        Coroutine c = null;

        private void SetJump()
        {
            jumpTime = 0f;
            isJump = true;            
        }
        protected override void Start()
        {
            base.Start();            
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            jumpTime = 0f;
            c = CoroutineEX.RandomDelay(this, SetJump, delayMin, delayMax);
        }

        public override void Move()
        {
            base.Move();

            if (isJump)
            {
                jumpTime += Time.fixedDeltaTime;
                float jumpPosition = PhysicsEX.Jump(jumpTime, jumpForce, gravity);
                if (jumpPosition < 0)
                {
                    jumpPosition = 0f;
                    isJump = false;
                    c = null;
                    c = CoroutineEX.RandomDelay(this, SetJump, delayMin, delayMax);
                }

                body.localPosition = Vector3.up * jumpPosition;
            }
        }
    }
}
