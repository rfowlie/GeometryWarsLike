﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Bullet_Surface : Poolable
    {
        private float distanceFromSurface = 1f;
        public float speed = 5f;
        public float lifetime = 3f;
        private float count = 0f;
        private LayerMask mapLayer;

        
        private void Start()
        {
            mapLayer = GameController.Instance.GetMapLayer();
            distanceFromSurface = GameController.Instance.GetDistanceFromSurface();
        }

        private void Update()
        {
            count += Time.deltaTime;
            if (lifetime < count)
            {
                count = 0f;
                gameObject.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            //calc next position for bullet
            Vector3 nextPos = transform.position + transform.forward * speed * Time.fixedDeltaTime;            
            if (Physics.Raycast(nextPos, -transform.up, out hit, distanceFromSurface + 2f, mapLayer))
            {
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                transform.position = hit.point + hit.normal * distanceFromSurface;
            }
            //if bullet glitches remove it...
            else
            {
                gameObject.SetActive(false);
            }
        }

        
        private void OnTriggerEnter(Collider other)
        {            
            if(other.gameObject.tag == "Enemy")
            {
                other.GetComponent<AEnemy>().CallEvent();
            }

            gameObject.SetActive(false);
        }
    }
}


