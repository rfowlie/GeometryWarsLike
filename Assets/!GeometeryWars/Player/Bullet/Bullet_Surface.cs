using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Bullet_Surface : Poolable
    {
        [Range(0.1f, 10f)] public float distanceFromSurface = 1f;
        public float speed = 5f;
        public float lifetime = 3f;
        private float count = 0f;

        private void Update()
        {
            count += Time.deltaTime;
            if (lifetime < count)
            {
                count = 0f;
                ReturnToPool(gameObject);
            }
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            //calc next position for bullet
            Vector3 nextPos = transform.position + transform.forward * speed * Time.fixedDeltaTime;
            //determine if colliding with something
            if (Physics.Raycast(transform.position, transform.forward * speed * Time.fixedDeltaTime))
            {
                //colliding with something
                transform.position = nextPos;
            }
            else if (Physics.Raycast(nextPos, -transform.up, out hit, float.PositiveInfinity))
            {
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                transform.position = hit.point + hit.normal * distanceFromSurface;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            ReturnToPool(gameObject);
        }
    }
}


