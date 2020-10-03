using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class SpawnManager : MonoBehaviour
    {
        public float count = 0f;

        //hold array of patterns for spawning enemies
        public Transform map;
        [SerializeField] private Grid grid;
        [SerializeField] private Poolable[] enemies;
        private ObjectPool[] pools;
        [SerializeField] private SO_SpawnPattern pattern;

        private void Start()
        {
            //create pools
            pools = new ObjectPool[enemies.Length];
            for (int i = 0; i < pools.Length; i++)
            {
                pools[i] = new ObjectPool(enemies[i]);
            }

            //spawnTimes.Enqueue(1f);
            spawnTimes.Enqueue(3f);
            //spawnTimes.Enqueue(5f);
            //spawnTimes.Enqueue(8f);
        }

        Queue<float> spawnTimes = new Queue<float>();
        private void Update()
        {
            count += Time.deltaTime;
            if(spawnTimes.Count > 0 && count > spawnTimes.Peek())
            {
                spawnTimes.Dequeue();

                for (int i = 0; i < pattern.points.Length; i++)
                {
                    GameObject temp = pools[0].Get();
                    temp.transform.position = pattern.points[i];
                    //point transform down towards map
                    temp.transform.rotation = Quaternion.FromToRotation(temp.transform.up, temp.transform.position - map.position) * temp.transform.rotation;
                }

                ////decided object order
                //int[] keys = { 0, 1, 0, 1, 0, 1, 0 };
                        
                //Vector3[] c = grid.GetCircleOfPoints(new Vector2(0.2f, 0.2f), keys.Length, 3f);
                //Vector3[] l = grid.GetLineOfPoints(new Vector2(0.3f, 0.8f), new Vector2(0.9f, 0.1f), keys.Length);

                //Spawn(keys, c);
                //Spawn(keys, l);
            }
        }

        //SPAWN PATTERNS
        public void Spawn(int[] indexs, Vector3[] points)
        {
            //ensure array sizes
            int count = indexs.Length < points.Length ? indexs.Length : points.Length;
            for (int i = 0; i < count; i++)
            {
                GameObject temp = pools[indexs[i]].Get();
                temp.transform.position = points[i];
            }
        }
    }
}


