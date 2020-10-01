using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{ 
    public class SpawnController : MonoBehaviour
    {
        public float count = 0f;

        //hold array of patterns for spawning enemies
        [SerializeField] private Grid grid;
        [SerializeField] private ObjectPool chase;
        [SerializeField] private ObjectPool wander;

        //SPAWN PATTERNS
        public void Spawn(GameObject[] objsToSpawn, Vector3[] points)
        {
            //ensure array sizes
            int count = objsToSpawn.Length < points.Length ? objsToSpawn.Length : points.Length;
            for (int i = 0; i < count; i++)
            {
                objsToSpawn[i].transform.position = points[i];
            }
        }
   
        private void Start()
        {
            spawnTimes.Enqueue(1f);
            spawnTimes.Enqueue(3f);
            spawnTimes.Enqueue(6f);
            spawnTimes.Enqueue(10f);
            spawnTimes.Enqueue(12f);
        }

        Queue<float> spawnTimes = new Queue<float>();
        private void Update()
        {
            count += Time.deltaTime;
            if(count > spawnTimes.Peek())
            {
                spawnTimes.Dequeue();
                //decided object order
                GameObject[] objs = { chase.Get(), wander.Get(), chase.Get(), wander.Get(), chase.Get(), wander.Get() };
                //get points
                Vector3[] points = grid.GetCircleOfPoints(new Vector2(0.2f, 0.2f), 6, 3f);
                //spawn
                Spawn(objs, points);
            }
        }
    }
}


