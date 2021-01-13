using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Burst;
using Unity.Jobs;
using UnityEngine.Jobs;


namespace GeometeryWars
{
    //run enemy behaviour/movement using JOB system
    public class EnemyManager
    {
        public EnemyManager(SpawnManager s)
        {
            spawn = s;
        }

        private SpawnManager spawn;
                
        public void Move()
        {
            ObjectPool<AEnemy>[] pools = spawn.GetPools();
            Debug.Log("<color=blue>Move Enemies</color>");
            for (int i = 0; i < pools.Length; i++)
            {
                List<AEnemy> temp = pools[i].GetActiveObjects();
                for (int j = 0; j < temp.Count; j++)
                {
                    temp[j].Move();
                }
            }
        }

        public void MoveIJob()
        {
            //create all native containers and populate 
            List<AEnemy> active = spawn.GetActiveFromPools();
            NativeArray<Vector3> pos = new NativeArray<Vector3>(active.Count, Allocator.TempJob);
            NativeArray<Quaternion> rot = new NativeArray<Quaternion>(active.Count, Allocator.TempJob);
            NativeArray<EnemyStruct> enemies = new NativeArray<EnemyStruct>(active.Count, Allocator.TempJob);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new EnemyStruct(active[i]);
            }

            //create job, pass in native containers
            EnemyMovement em = new EnemyMovement
            {
                e = enemies,
                finalPos = pos,
                finalRot = rot,
                fixedDeltaTime = Time.fixedDeltaTime
            };

            //schedule, sending amount to each thread...
            JobHandle handle = em.Schedule(active.Count, 1);
            handle.Complete();

            //update original values
            for (int i = 0; i < active.Count; i++)
            {
                active[i].transform.position = pos[i];
                active[i].transform.rotation = rot[i];
            }

            //dispose of native containers
            pos.Dispose();
            rot.Dispose();
            enemies.Dispose();
        }

        public struct EnemyStruct
        {
            public EnemyStruct(AEnemy e)
            {
                position = e.transform.position;
                forward = e.transform.forward;
                up = e.transform.up;
                movement = e.Movement?.Invoke() ?? Vector3.zero;
                obstacleDistance = e.obstacleDistance;
                obstacleLayer = e.obstacleLayer;
                mapLayer = e.mapLayer;
                speedThrust = e.speedThrust;
                rot = e.transform.rotation;
                rotation = e.Rotation?.Invoke() ?? e.transform.rotation;
            }

            public Vector3 position;
            public Vector3 forward;
            public Vector3 up;
            public Vector3 movement;
            public float obstacleDistance;
            public float speedThrust;
            public Quaternion rot;
            public Quaternion rotation;

            //could filter out...
            public LayerMask obstacleLayer;
            public LayerMask mapLayer;
        }

        //Try to make above IJOB
        public struct EnemyMovement : IJobParallelFor
        {
            public NativeArray<EnemyStruct> e;
            public float fixedDeltaTime;

            public NativeArray<Vector3> finalPos;
            public NativeArray<Quaternion> finalRot;

            public void Execute(int index)
            {
                RaycastHit hitNext;
                Vector3 nextPos = e[index].position + e[index].movement;
                if (Physics.SphereCast(e[index].position, 1f, e[index].forward, out hitNext, e[index].obstacleDistance, e[index].obstacleLayer))
                {
                    Vector3 p = Vector3.Project(e[index].position - hitNext.point, hitNext.normal);
                    p = ((hitNext.point - e[index].position) + p).normalized;
                    nextPos = e[index].position + p * e[index].speedThrust * fixedDeltaTime;
                }

                RaycastHit hit;
                if (Physics.Raycast(nextPos, -e[index].up, out hit, float.PositiveInfinity, e[index].mapLayer))
                {
                    finalPos[index] = hit.point + hit.normal;
                }

                finalRot[index] = e[index].rotation;
            }
        }
    }
}


