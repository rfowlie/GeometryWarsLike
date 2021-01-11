using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPTest : MonoBehaviour
{
    [SerializeField] private OPPrefabTest prefab;
    private ObjectPool<OPPrefabTest> pool;

    private void Start()
    {
        pool = new ObjectPool<OPPrefabTest>(prefab);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            OPPrefabTest temp = pool.Retrieve();
            temp.transform.position = new Vector3(randomFloat(), randomFloat(), randomFloat());
        }
    }

    private float randomFloat()
    {
        return Random.Range(-10f, 10f);
    }
}
