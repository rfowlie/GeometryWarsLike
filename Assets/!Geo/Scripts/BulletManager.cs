using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private Bullet_Surface bullet;
        private ObjectPool<Bullet_Surface> bulletPool;

        public void Setup()
        {
            //create bullet pool
            bulletPool = new ObjectPool<Bullet_Surface>(bullet);
        }

        Coroutine c = null;
        [SerializeField] public bool isFire = false;
        [Range(0.01f, 1f)]
        [SerializeField] private float fireRate = 0.1f;

        public void BeginFire(Transform t)
        {
            isFire = true;
            c = StartCoroutine(Fire(t));
        }
        public void StopFire()
        {
            isFire = false;
            c = null;
        }

        IEnumerator Fire(Transform body)
        {
            float count = 0f;
            while (isFire)
            {
                count += Time.deltaTime;
                if (count > fireRate)
                {
                    count -= fireRate;
                    Bullet_Surface bullet = bulletPool.Retrieve();
                    bullet.transform.position = body.position + body.forward;
                    bullet.transform.rotation = body.rotation;
                }

                yield return null;
            }
        }
    }
}


