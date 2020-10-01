using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Poolable
{
    [SerializeField] private ParticleSystem particle;
    public ObjectPool prefabPool;
    //[SerializeField] private GameObject prefab;
    public int numberToSpawn = 1;
    public bool isSpawn = false;
    public float timeIntro = 1f;

    private void OnEnable()
    {        
        particle.Clear();
        particle.time = 0f;
        particle.Play();
    }

    private void Update()
    {
        //Debug.Log("Particle Time: " + particle.time);
        if (particle.time > timeIntro)
        {
            isSpawn = true;
        }
        if (isSpawn)
        {
            if (numberToSpawn > 0)
            {
                numberToSpawn--;
                GameObject obj = prefabPool.Get();
                //be sure to set position to here
                obj.transform.position = transform.position;
            }
            else
            {
                //temp
                isSpawn = false;
                particle.Stop();
                gameObject.SetActive(false);

                //send back to pool which will deactivate
                //pool.Return(gameObject);
            }
        }
    }
}
