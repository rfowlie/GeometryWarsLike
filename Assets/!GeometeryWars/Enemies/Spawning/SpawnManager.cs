using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private ObjectPool chase;
    [SerializeField] private ObjectPool wander;
    [SerializeField] private Spawner s;
    [SerializeField] private GridOLD grid;

    private void Update()
    {
        if (!s.isActiveAndEnabled)
        {
            //get random location from grid...
            Debug.Log("New Point");
            Vector3 spawnPoint = grid.GetRandomPointInGrid();
            s.transform.position = spawnPoint;
            s.prefabPool = chase;
            s.numberToSpawn = 1;
            s.gameObject.SetActive(true);
        }
    }
}
