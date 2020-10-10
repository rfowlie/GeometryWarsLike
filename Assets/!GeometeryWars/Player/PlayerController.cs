using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeometeryWars
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        public Poolable bullet;
        private ObjectPool bulletPool;
        [Space]
        public bool canMove = true;
        public float speed = 1.0f;
        private Vector3 velocity;
        private float angle = 0f;

        [Tooltip("Number of bullets per second")]
        public int fireRate = 1;
        private float fireSpeed = 0;
       
        private void Start()
        {
            healthCurrent = healthMax;
            fireSpeed = 1f / fireRate;
            //set default shoot
            bulletPool = new ObjectPool(bullet);
            fire = SingleBullet;
        }

        private void Update()
        {
            //movement            
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

            //rotation
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;

            //shoot
            if (Input.GetMouseButtonUp(1) && !isShooting)
            {
                isShooting = true;
                c = null;
                c = StartCoroutine(Shooting());
            }
            else if (Input.GetMouseButtonUp(1) && isShooting)
            {
                isShooting = false;
            }
        }

        private void FixedUpdate()
        {
            //move
            if (canMove)
            {
                if (velocity != Vector3.zero)
                {
                    transform.position += velocity * speed * Time.fixedDeltaTime;
                }
            }

            //rotate
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private bool isShooting = false;
        Coroutine c = null;
        private float count = 0f;
        IEnumerator Shooting()
        {
            while (isShooting)
            {
                count -= Time.deltaTime;
                if (count < 0f)
                {
                    //shoot current fire type
                    fire();
                    count += fireSpeed;
                }

                yield return null;
            }
        }

        private delegate void Fire();
        private Fire fire;
        private void SingleBullet()
        {
            GameObject temp = bulletPool.Get();
            temp.transform.position = transform.position + transform.up;
            temp.transform.rotation = transform.rotation;
        }

        private void TripleBullet()
        {
            GameObject temp;
            temp = bulletPool.Get();
            temp.transform.position = transform.position + transform.up;
            temp.transform.rotation = transform.rotation;

            temp = bulletPool.Get();
            Quaternion rot = Quaternion.AngleAxis(8f, Vector3.forward);
            temp.transform.position = transform.position + rot * transform.up;
            temp.transform.rotation = transform.rotation * rot;

            temp = bulletPool.Get();
            rot = Quaternion.AngleAxis(-8f, Vector3.forward);
            temp.transform.position = transform.position + rot * transform.up;
            temp.transform.rotation = transform.rotation * rot;
        }

        //Stats
        public int healthMax = 3;
        public int healthCurrent;
        public LayerMask canDamage;
        public static event Action playerDead;
        public void Damage()
        {
            healthCurrent--;
            if (healthCurrent <= 0)
            {
                if (playerDead != null)
                {
                    playerDead();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (LayerMaskEX.IsInLayerMask(collision.gameObject.layer, canDamage))
            {
                Damage();
            }
            else if(collision.gameObject.tag == "PickUp")
            {                
                pickUpCount += 3f;                
                if(cPickUp == null)
                {
                    Debug.Log("<color=red>TripleBullet</color>");
                    fire = TripleBullet;
                    cPickUp = StartCoroutine(PickUpCountDown());
                }
            }
        }

        //FOR NOW
        //INSTEAD SHOULD JUST USE OBSERVER PATTERN
        public float pickUpTime = 10f;
        private float pickUpCount = 0f;
        Coroutine cPickUp = null;
        private IEnumerator PickUpCountDown()
        {
            while(pickUpCount > 0)
            {
                pickUpCount -= Time.deltaTime;
                yield return null;
            }

            Debug.Log("<color=blue>SingleBullet</color>");
            fire = SingleBullet;
            cPickUp = null;
        }
    }
}

