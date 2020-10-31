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
        public Rigidbody rb;
        public LayerMask reflectOff;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            //start in random direction with a little movement in any axis
            float x = Random.Range(0.1f, 1f) * 2f - 1f;
            float y = Random.Range(0.1f, 1f) * 2f - 1f;
            rb.velocity = new Vector3(x, y, 0f).normalized;
            Debug.Log("Ready");
        }

        private void Update()
        {
            //RaycastHit hit;
            //if (Physics.SphereCast(transform.position, 1f, velocity, out hit, 0.3f, reflectOff))
            //{
            //    if (hit.collider.gameObject.tag == "Barrier")
            //    {
            //        velocity = Vector3.Reflect(velocity, hit.normal);
            //    }
            //    else
            //    {
            //        velocity = hit.normal;
            //    }

            //    Debug.DrawLine(hit.point, hit.point + hit.normal, Color.cyan, 5f);
            //    //transform.position = hit.point + velocity * Time.fixedDeltaTime;
            //    Debug.DrawLine(hit.point, hit.point + velocity, Color.red, 5f);

            //    //get it away from barrier...
            //    transform.position += velocity * speedThrust * Time.deltaTime;
            //}
        }

        private void FixedUpdate()
        {
            //if(isActive)
            //{      
            //    transform.position += velocity * speedThrust * Time.fixedDeltaTime;
            //}
        }

        private void OnTriggerEnter(Collider other)
        {
            //get direction of object to this
            Vector3 normal = transform.position - other.transform.position;
            rb.velocity = Vector3.Reflect(velocity, normal);
            Debug.DrawLine(other.transform.position, transform.position, Color.cyan, 5f);
            Debug.DrawLine(transform.position, transform.position + rb.velocity, Color.red, 5f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"Collision Name {collision.gameObject.name}");
            if (LayerMaskEX.IsInLayerMask(collision.gameObject.layer, reflectOff))
            {
                Debug.Log("Reflect");
                //shoot raycast in velocity direction from transform
                RaycastHit hit;
                if (Physics.Raycast(transform.position - velocity, velocity, out hit, 5f, reflectOff))
                {
                    rb.velocity = Vector3.Reflect(velocity, hit.normal) * speedThrust * Time.fixedDeltaTime;
                    Debug.DrawLine(hit.point, hit.point + hit.normal, Color.cyan, 5f);
                    Debug.DrawLine(hit.point, hit.point + rb.velocity.normalized, Color.red, 5f);
                }
            }
        }

        protected override void SetMovement()
        {
            throw new System.NotImplementedException();
        }
    }
}


