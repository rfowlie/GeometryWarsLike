using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private Poolable bullet;
    private ObjectPool bulletPool;

    private void Start()
    {
        //create bullet pool
        bulletPool = new ObjectPool(bullet);
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
                GameObject bullet = bulletPool.Get();
                bullet.transform.position = body.position + body.forward;
                bullet.transform.rotation = body.rotation;
            }

            yield return null;
        }
    }
}
