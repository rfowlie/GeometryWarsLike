using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chase_Obstacle : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float speedThrust = 1f;
    public float speedSteer = 45f;

    public Obstacle obstacle;
    public Vector3 addSteer;
    public float checkSize = 10f;
    // Update is called once per frame
    void Update()
    {
        //get obstacles
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkSize);
        List<Obstacle> obs = new List<Obstacle>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].GetComponent<Obstacle>() != null)
            {
                obs.Add(colliders[i].GetComponent<Obstacle>());
            }
        }

        addSteer = Vector3.zero;
        for (int i = 0; i < obs.Count; i++)
        {
            //check obstacle
            Vector3 obToPos = transform.position - obs[i].transform.position;
            addSteer += obToPos.normalized * obs[i].EffectAmount(obToPos.magnitude) * Time.deltaTime;
        }

        transform.localEulerAngles += Steer.TowardTarget(transform, target.position + addSteer) * speedSteer * Time.deltaTime;
        transform.position += transform.forward * speedThrust * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target.position + addSteer, 1f);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, checkSize);
    }
}
