using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private Grid grid;
        [SerializeField] private Poolable pickUp;
        private ObjectPool pickUpPool;

        private void Start()
        {
            pickUpPool = new ObjectPool(pickUp);

            //temp spawn pickups
            for (int i = 0; i < 2; i++)
            {
                Vector3 temp = grid.GetRandomPointInGrid();
                GameObject obj = pickUpPool.Get();
                obj.transform.position = temp;
            }            
        }

    }
}

