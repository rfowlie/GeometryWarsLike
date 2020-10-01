using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    //enemy that begins moving in random direction
    //try to avoid pure up down or left right
    //change direction upon collision with barrier
    public class Enemy_Wander : AEnemy
    {
        public float speed = 1f;
        public Vector3 velocity = Vector3.zero;
        public LayerMask barrier;

        // Start is called before the first frame update
        void Start()
        {
            //start in random direction with a little movement in any axis
            float x = Random.Range(0.1f, 1f) * 2f - 1f;
            float y = Random.Range(0.1f, 1f) * 2f - 1f;
            velocity = new Vector3(x, y, 0f);
            velocity = velocity.normalized;
        }

        private void FixedUpdate()
        {
            if(isActive)
            {
                transform.position += velocity * speed * Time.fixedDeltaTime;
            }
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            if (collision.gameObject.tag == "Barrier")
            {
                //change velocity
                //shoot raycast in velocity direction from transform
                RaycastHit hit;
                if (Physics.Raycast(transform.position - velocity, velocity, out hit, 5f, barrier))
                {
                    velocity = Vector3.Reflect(velocity, hit.normal);

                    //get it away from barrier...
                    transform.position += velocity * speed * Time.deltaTime;
                }
            }
        }
    }
}


